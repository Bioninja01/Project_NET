using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChip : MonoBehaviour {

    public int damage = 1;
    public Sprite img;
    public GameObject projectile;

    // Use this for initialization
    void Start () {
		
	}

    public virtual void Atk(BattleMovement hero) {
        //Physics.CheckSphere(hero.transform.position + (hero.transform.forward * hero.mag), 1f);
        GameObject bullet = Instantiate ( projectile, 
                                          hero.transform.position + hero.transform.forward * (hero.mag), 
                                          Quaternion.identity
                                        ) as GameObject;
    }

}
