using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    Transform player;
    public int xoff, yoff, zoff;
    private Vector3 offset;
    public float turnSpeed = 4.0f;

    public enum CameraState{
        NORMAL,
        ZOOM
    }
    public CameraState state = CameraState.NORMAL;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        switch(state){
            case CameraState.NORMAL:
                normalCamera();
                break;
            case CameraState.ZOOM:
                //TODO: add real logic to this section
                transform.LookAt(player.position);
                break;
        }
	}

    void normalCamera() {
        transform.position = new Vector3(player.position.x - xoff, player.position.y + yoff, player.position.z - zoff);
    }
}
