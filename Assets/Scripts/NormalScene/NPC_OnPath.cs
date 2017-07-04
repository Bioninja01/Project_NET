using UnityEngine;
using System.Collections;


public class NPC_OnPath : NPC {

    public delegate void fishedPathAction();
    public static event fishedPathAction MoveToNextTarget;

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
        targetIndex = 0;
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
            Vector3 v3;
            v3 = new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z);
            while (true) {
                if (state == NPC.NPC_state.STAY) {
                    yield return null;
                    continue;
                }
                if (transform.position == v3) {
                    targetIndex++;
                    if (targetIndex >= path.Length) {
                        fishedPath = true;
                        MoveToNextTarget();
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                    v3 = new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z);
                }
                transform.position = Vector3.MoveTowards(transform.position, v3, speed * Time.deltaTime);
                transform.LookAt(v3);
                yield return null;
            }
        }
    }

    public override void RevertState() {
        switch (state) {
            case NPC_state.STAY:
                animator.SetFloat("NPC_State", 0);
                break;
            case NPC_state.MOVE:
                animator.SetFloat("NPC_State", 1);
                break;
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
