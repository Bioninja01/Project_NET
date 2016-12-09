using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialog : ScriptableObject {

    public TextAsset dialogTXT;
    List<string> dialog;

    void Start() {
        dialog = new List<string>();
        if (dialogTXT != null) {
            string[] lines = dialogTXT.text.Split('\n');
            foreach (string s in lines) {
                dialog.Add(s);
            }
        }
    }
}
