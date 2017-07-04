using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerControllerV2 : MonoBehaviour {
    public Sprite img;
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float movementAngleOffset = 45f;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public delegate void TalkAction(PlayerControllerV2 pc, GameObject go);
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

    Animator animator;
    
    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
        npcs = new List<GameObject>();
        rd = GetComponent<Rigidbody>();
       
    }

    void OnTriggerStay(Collider other) {
        Vector3 distance2Other = other.gameObject.transform.position - transform.position;
        float angle = Vector3.Angle(distance2Other, transform.TransformDirection(Vector3.forward));
        if (other.gameObject.tag.Equals("NPC")) {
            if (angle < viewAngle) {
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
                Move();
                break;
            case CharState.TALKING:
                break;
        }
    }

    void Move() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero) {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + movementAngleOffset;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            // hard tranformation // transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
        }
        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        if(inputDir.magnitude != 0) {
            Walk();
        }
        float animationSpeed = ((running) ? 1f : .5f) * inputDir.magnitude;
        SetAnimation("SpeedPersent", animationSpeed);
        InitTalkingComand();
        axisInUse = false;
    }
    
    void InitTalkingComand() {
        if (Input.GetAxis("Submit") != 0) {
            if (axisInUse == false) {
                axisInUse = true;
                if (npcs.Count > 0) {
                    oldRotation = transform.rotation;
                    transform.LookAt(npcs[0].transform.position);
                    npcs[0].transform.LookAt(transform.position);
                    state = CharState.TALKING;
                    SetAnimation("SpeedPersent", 0);
                    Talk(this, npcs[0]);    //start talking
                }
            }
        }
    }

    public void SetAnimation(string name,float animationNumber) {
        animator.SetFloat(name, animationNumber);
    }
    
    public void ResetPosition() {
        transform.rotation = oldRotation;
    }
}
