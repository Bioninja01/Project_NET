using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class PathCreator : EditorWindow {

    [MenuItem("Window/YellowCar/PathCreater")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(PathCreator));
    }
    void OnGUI() {
        Event current = Event.current;
        if (current.isKey ){
           
            if(current.keyCode == KeyCode.B) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                    Debug.DrawLine(ray.origin, hit.point);
            }
        }
    }

}
