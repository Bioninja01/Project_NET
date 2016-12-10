using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_PathController : MonoBehaviour {

    public List<Transform> targets;
    int index = 0;
    NPC_OnPath npc;

    public void OnEnable() {
        NPC_OnPath.Action += MoveToNextTarget;
    }
    public void OnDisable() {
        NPC_OnPath.Action -= MoveToNextTarget;
    }

    // Use this for initialization
    void Start () {
        npc = GetComponent<NPC_OnPath>();
        if(targets.Count > 0) {
            npc.target = targets[0];
            npc.FindTarget();
        }
	}

    public void MoveToNextTarget() {
        index++;
        if(index >= targets.ToArray().Length) {
            index = 0;
        }
        npc.target = targets[index];
        npc.FindTarget();
    }
}
