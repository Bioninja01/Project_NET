using UnityEngine;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour {

    //Change to the true singleTon Pattern
    static PathRequestManager instance;

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;
    Pathfinding pathFinding;

    bool isProcessingPath;

    void Awake() {
        instance = this;
        pathFinding = GetComponent<Pathfinding>();
    }

	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[],bool> callback) {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext() {
        if (!isProcessingPath && pathRequestQueue.Count > 0) {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
    
    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> _callback) {
            this.pathStart = start;
            this.pathEnd = end;
            this.callback = _callback;
        }
    }

}
