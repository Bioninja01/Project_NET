using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour {
    public Sprite img;
    public TextAsset dialogTXT;
    [HideInInspector]
    public List<string> dialog;

    Quaternion oldPosition;

    public enum NPC_state {
        MOVE,
        STAY,
        ACTION
    }
    public NPC_state state;
    NPC_state oldState;

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
    }

    public string printDialog(int i) {
        if (i >= dialog.Count || i < 0) {
            return null;
        }
        state = NPC_state.STAY;
        return dialog[i];
    }
    public void ResetPosition() {
        transform.rotation = oldPosition;
    }

}
