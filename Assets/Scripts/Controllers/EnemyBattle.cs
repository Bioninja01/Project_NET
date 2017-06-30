using UnityEngine;
using System.Collections;

public class EnemyBattle : MonoBehaviour {

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Projectiles")) {
            Destroy(other.gameObject);
        }
    }
}
