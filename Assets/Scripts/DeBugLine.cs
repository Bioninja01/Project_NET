using UnityEngine;
using System.Collections;

public class DeBugLine : MonoBehaviour {

    public int debugLineSize = 2;

    // Update is called once per frame
    void Update () {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * debugLineSize;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
}
