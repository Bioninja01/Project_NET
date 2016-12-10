using UnityEngine;
using System.Collections;


public class NPC_OnPath : NPC {

    public delegate void fishedPathAction();
    public static event fishedPathAction Action;

    public Transform target;
    public float speed = 1;

    bool fishedPath = false;
    public bool FinshedPath {
        get {
            return fishedPath;
        }
    }

    Vector3[] path;
    int targetIndex;



    public void FindTarget() {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccesful) {
        if (pathSuccesful) {
            path = newPath;
            fishedPath = false;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        if (path.Length != 0) {
            Vector3 currentWaypoint = path[0];
            while (true) {
                if (state == NPC.NPC_state.STAY) {
                    yield return null;
                    continue;
                }
                if (transform.position == currentWaypoint) {
                    targetIndex++;
                    if (targetIndex >= path.Length) {
                        fishedPath = true;
                        Action();
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
        if (path != null) {
            Gizmos.color = Color.black;
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
