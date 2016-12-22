using UnityEngine;
using System.Collections;

public class CharaterBattle : MonoBehaviour {
    [HideInInspector] public AudioSource music;
    [HideInInspector] public AudioClip damage;
    public int Hp = 1000;

    public delegate void UpdaeUiAction(CharaterBattle charater);
    public static event UpdaeUiAction notifyUi;

    public virtual void Start() {
        music = GetComponent<AudioSource>();
        damage = Resources.Load("Music/Hit1") as AudioClip;
    }

    public virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Projectiles")) {
            Projectal p = other.GetComponent<Projectal>();
            Hp -= p.damage;
            notifyUi(this);
            Destroy(other.gameObject);
            music.PlayOneShot(damage,.5f);
        }
    }
}
