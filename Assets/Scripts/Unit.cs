using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
    public Transform target;
    public float speed = 1;
    Vector3[] path;
    int targetIndex;

    public virtual void OnEnable() {
        PlayerController.Walk += FindTarget;
    }
    public virtual void OnDisable() {
        PlayerController.Walk -= FindTarget;
    }
    
    public void FindTarget() {
        targetIndex = 0;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesful) {
        if (pathSuccesful) {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        if (path.Length != 0) {
            Vector3 currentWaypoint = path[0];
            while (true) {
                if (transform.position == currentWaypoint) {
                    targetIndex++;
                    if (targetIndex >= path.Length) {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    public void OnDrawGizmos() {
        if(path != null) {
            Gizmos.color = Color.black;
            for(int i= targetIndex; i< path.Length; i++) {
                Gizmos.DrawCube(path[i], Vector3.one);
                if( i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else{
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }
}
