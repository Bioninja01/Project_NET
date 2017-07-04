using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour {
    public Sprite img; // Character Portrait
    public TextAsset dialogTXT;
    [HideInInspector] public List<string> dialog;

    Quaternion oldPosition;
    [HideInInspector] public Animator animator;

    public enum NPC_state {
        MOVE,
        STAY,
        ACTION
    }

    public NPC_state state;
    [HideInInspector] public NPC_state oldState;

    // Use this for initialization
    void Start() {
        dialog = new List<string>();
        if (dialogTXT != null) {
            string[] lines = dialogTXT.text.Split('\n');
            foreach (string s in lines) {
                dialog.Add(s);
            }
        }
        oldPosition = transform.rotation;
        animator = GetComponent<Animator>();
        switch (state) {
            case NPC_state.STAY:
                animator.SetFloat("NPC_State", 0);
                break;
            case NPC_state.MOVE:
                animator.SetFloat("NPC_State", 1);
                break;
        }
    }

    public bool waitForChoise(int i) {
        if(i >= dialog.Count) { return false; }
        string[] choises = dialog[i].Split(' ');
        if( choises[0] == "choises:") { return true; }
        return false;
    }
    public string printDialog(int i) {
        if (i >= dialog.Count || i < 0) {
            return null;
        }
        return dialog[i];
    }
    public void ResetPosition() {
        transform.rotation = oldPosition;
    }

    public void ChangeState(NPC_state s) {
        switch (s) {
            case NPC_state.STAY:
                animator.SetFloat("NPC_State", 0);
                break;
            case NPC_state.MOVE:
                animator.SetFloat("NPC_State", 1);
                break;
        }
    }
    public virtual void RevertState() {
        switch (state) {
            case NPC_state.STAY:
                animator.SetFloat("NPC_State", 0);
                break;
            case NPC_state.MOVE:
                animator.SetFloat("NPC_State", 1);
                break;
        }
    }
}
