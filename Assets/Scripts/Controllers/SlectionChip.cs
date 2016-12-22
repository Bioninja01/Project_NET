using UnityEngine;
using System.Collections.Generic;

public class SlectionChip : MonoBehaviour {

    public Transform parent;
    int index = 0;

    [HideInInspector]
    public AudioSource music;
    AudioClip select;

    void Start() {
        music = GetComponent<AudioSource>();
        select = Resources.Load("Music/Select1") as AudioClip;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.D)) {
            if(index < parent.childCount - 1) {
                index++;
                transform.SetParent(parent.GetChild(index));
                transform.localPosition = new Vector3(0, 0, 0);
                music.PlayOneShot(select);
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            if (index > 0) {
                index--;
                transform.SetParent(parent.GetChild(index));
                transform.localPosition = new Vector3(0, 0, 0);
                music.PlayOneShot(select);
            }
        }
    }
}
