using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC_PathController : MonoBehaviour {

    public List<Transform> targets;
    int index = 0;
    NPC_OnPath npc;

    public void OnEnable() {
        NPC_OnPath.MoveToNextTarget += MoveToNextTarget;
    }
    public void OnDisable() {
        NPC_OnPath.MoveToNextTarget -= MoveToNextTarget;
    }

    // Use this for initialization
    void Start () {
        npc = GetComponent<NPC_OnPath>();
        MoveToNextTarget();
	}

    public void MoveToNextTarget() {
        if(index >= targets.Count) {
            index = 0;
        }
        npc.target = targets[index];
        npc.FindTarget();
        index++;
    }
}
