using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Charater : MonoBehaviour {

    public delegate void TalkAction(Charater c, GameObject go);
    public static event TalkAction Talk;

    public delegate void WalkAction();
    public static event WalkAction Walk;

    public enum CharState {
        NORMAL,
        TALKING
    }

    /*Fields*/
    public List<Image> portraits;
    public float viewAngle;
    public CharState state = CharState.NORMAL;

    List<GameObject> npcs;
    bool axisInUse = false;
    Rigidbody rd;
    Quaternion oldRotation;

    void Awake() {
        npcs = new List<GameObject>();
        rd = GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other) {
        Vector3 distance2Other = other.gameObject.transform.position - transform.position;
        float angle = Vector3.Angle(distance2Other, transform.TransformDirection(Vector3.forward));
        if (other.gameObject.tag.Equals("NPC")) {
            if(angle < viewAngle) {
                if (!npcs.Contains(other.gameObject)) {
                    npcs.Add(other.gameObject);
                }
            }
            else {
                if (npcs.Contains(other.gameObject)) {
                    npcs.Remove(other.gameObject);
                }
            }
        }
    }
    void OnTriggerExit(Collider other) {
        if (npcs.Contains(other.gameObject)) {
            npcs.Remove(other.gameObject);
        }
    }
    void Update() {
        switch (state) {
            case CharState.NORMAL:
                NormalMove();
                break;
            case CharState.TALKING:
                break;
        }
    }

    public void ResetPosition() {
        transform.rotation = oldRotation;
    }
    private void NormalMove() {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 190.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;
        if(x != 0 || z != 0) {
            Walk();
        }
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        rd.velocity = new Vector3(0, -9.8f, 0);
        if (Input.GetAxis("Submit") != 0) {
            if (axisInUse == false) {
                axisInUse = true;
                if (npcs.Count > 0) {
                    oldRotation = transform.rotation;
                    transform.LookAt(npcs[0].transform.position);
                    npcs[0].transform.LookAt(transform.position);
                    state = CharState.TALKING;
                    Talk(this, npcs[0]);    //start talking
                }
            }
        }
        else {
            axisInUse = false;
        }
    }

}
