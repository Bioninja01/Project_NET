using UnityEngine;
using System.Collections;

public class Projectal : MonoBehaviour {

    public int damage;
    public float lifeTime;
	
	// Update is called once per frame
	void Update () {
	    if(lifeTime <= 0) {
            Destroy(gameObject);
        }
        else {
            lifeTime -= Time.deltaTime;
        }
	}
}
