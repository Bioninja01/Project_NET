using UnityEngine;
using System.Collections;

public class MovementTest : MonoBehaviour {

    public Transform target;
    public float speed;
    void Update() {
        float step = speed * Time.deltaTime;
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
