using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public delegate void ClickAction();
    public static event ClickAction OnClick;

    public void Test() {
        OnClick();
    }

}