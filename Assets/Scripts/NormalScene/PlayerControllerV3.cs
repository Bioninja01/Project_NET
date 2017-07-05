using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV3 : MonoBehaviour {

    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float movementAngleOffset = 45f;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
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

        float animationSpeed = ((running) ? 1f : .5f) * inputDir.magnitude;
     
        animator.SetFloat("SpeedPercent", animationSpeed);
      
    }
}
