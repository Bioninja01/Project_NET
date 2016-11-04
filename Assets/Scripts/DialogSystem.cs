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



    /*Setters */
    public void SetImages(Image i1, Image i2) {
        SetCharater1(i1);
        SetCharater2(i2);
    }
    public void SetCharater1(Image i1) {
        charater1 = i1;
    }
    public void SetCharater2(Image i2) {
        charater2 = i2;
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
