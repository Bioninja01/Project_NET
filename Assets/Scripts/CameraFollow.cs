using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    Transform player;
    public int xoff, yoff, zoff;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position= new Vector3(player.position.x-xoff, player.position.y+yoff, player.position.z-zoff);
	}
}
