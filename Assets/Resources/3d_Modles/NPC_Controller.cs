using UnityEngine;
using System.Collections;

public class NPC_Controller : MonoBehaviour {

    public enum State {
        Ideal,
        Walk
    }

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }

    public void ChangeState(NPC_Controller.State s) {
        switch (s) {
            case State.Ideal:
                animator.SetFloat("NPC_State", 0);
                break;
            case State.Walk:
                animator.SetFloat("NPC_State", 1);
                break;
        }
    }
}
