using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Charater : MonoBehaviour {
    /*Fields*/
    public List<Image> portraits;
    public int debugLineSize;

    /*SetUP*/
    public void OnEnable() {
        EventManager.OnClick += FrontAction;
    }
    public void OnDisable() {
        EventManager.OnClick -= FrontAction;
    }

    void Update() {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * debugLineSize;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    /*Even tActions*/
    public void MyAction() {
        Debug.Log("test:1");
    }

    public void FrontAction() {
        Debug.Log("test:2");
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        bool b = Physics.Raycast(transform.position, forward, out hit, debugLineSize);
        if (b) {
            string s = hit.transform.gameObject.ToString();
            Debug.Log("The obj I hit: "+s);
        }
    }
}
