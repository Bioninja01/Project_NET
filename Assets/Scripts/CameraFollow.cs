using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position= new Vector3(player.position.x-35, player.position.y+50, player.position.z-35);
	}
}
