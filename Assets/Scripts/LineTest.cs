using UnityEngine;
using System.Collections;

public class LineTest : MonoBehaviour {

    public Transform obj1;
    public Transform obj2;
	
	// Update is called once per frame
	void Update () {
	    if(obj1 == null || obj2 == null) {
            return;
        }
        Debug.DrawLine(obj1.position, obj2.position, Color.red, 1f);
	}
}
