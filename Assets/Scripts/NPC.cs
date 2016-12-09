using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


public class NPC : MonoBehaviour {
    public Sprite img;
    public TextAsset dialogTXT;
    public GameObject path;
    [HideInInspector]
    public List<string> dialog;

    Quaternion oldPosition;
    StreamWriter writer;
    MatchCollection matches;
    GroupCollection groups;
    Transform[] markers;
    public int targetMaker = 0;



    public enum NPC_state {
        MOVE,
        STAY,
        ACTION
    }

    public NPC_state state;

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

        if (path != null) {
            int num = path.transform.childCount;
            markers = new Transform[num];
            for (int i = 0; i < num; i++) {
                markers[i] = path.transform.GetChild(i);
            }
        }
    }
    void Update() {
        switch (state) {
            case NPC_state.ACTION:
                break;
            case NPC_state.MOVE:
                if (path == null) return;
                transform.LookAt(markers[targetMaker]);
                transform.Translate(0, 0, .080f);
                break;
            case NPC_state.STAY:
                break;

        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("hI");
            state = NPC_state.STAY;
        }
    }
    public void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            if(path != null) {
                state = NPC_state.MOVE;
            }
        }
    }

    public void ChangeMarker() {
        targetMaker++;
        state = NPC_state.ACTION;
        Invoke("Move", 2);
        if (targetMaker == path.transform.childCount) {
            targetMaker = 0;
        }
        
    }

    public string printDialog(int i) {
        if (i >= dialog.Count || i < 0) {
            if (path != null) {
                state = NPC_state.MOVE;
            }
            return null;
        }
        state = NPC_state.STAY;
        return dialog[i];
    }
    public void ResetPosition() {
        transform.rotation = oldPosition;
    }

    void Move() {
        state = NPC_state.MOVE;
    }





}
