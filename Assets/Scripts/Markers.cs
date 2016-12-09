using UnityEngine;
using System.Collections;

public class Markers : MonoBehaviour {

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "NPC") {
            NPC npc = other.GetComponent<NPC>();
            npc.ChangeMarker();
        }
    }

    
}
