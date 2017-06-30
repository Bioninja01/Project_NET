using UnityEngine;
using System.Collections.Generic;

public class BattleGrid : MonoBehaviour {

    [HideInInspector] public List<Transform> battleGrid;
    public int[] size = { 3, 6 };
    // Use this for initialization
    void Start () {
	    for(int i = 0; i < transform.childCount; i++) {
            battleGrid.Add(transform.GetChild(i));
        }
	}
}
