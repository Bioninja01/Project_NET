using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    public Text dialog;
    public Image charater1;
    public Image charater2;

    // Use this for initialization
    void Start () {
	
	}


    public class DialogInterface {
        public static DialogInterface instance;
        private DialogInterface() { }
        public static DialogInterface GetInstance() {
            if(instance == null) {
                instance = new DialogInterface();
            }
            return instance;
        }
    }
}
