﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {

    public GameObject overlay;
    public GameObject dialogBox;
    public GameObject imageCon1;
    public GameObject imageCon2;
    public GameObject choices;


    public Text dialog;
    private Image img_charater1;
    private Image img_charater2;

    // Use this for initialization
    void Start () {
        img_charater1 = imageCon1.GetComponent<Image>();
        img_charater2 = imageCon2.GetComponent<Image>();
        HideDialogUi();
	}
    public void ShowDialogUi() {
        overlay.SetActive(true);
        choices.SetActive(true);
        dialogBox.SetActive(true);
    }
    public void HideDialogUi() {
        overlay.SetActive(false);
        dialogBox.SetActive(false);
        imageCon1.SetActive(false);
        imageCon2.SetActive(false);
        choices.SetActive(false);
    }
    //Delegets
    public void OnEnable() {
        PlayerControllerV2.Talk += StartTalkCoroutine;
    }
    public void OnDisable() {
        PlayerControllerV2.Talk -= StartTalkCoroutine;
    }

/*Main Logic of DialogSystem*/
    private void StartTalkCoroutine(PlayerControllerV2 pc, GameObject go) {
        NPC npc = go.GetComponent<NPC>();
        if(npc != null) {
            StartCoroutine(TalkCoroutine(KeyCode.G, pc, npc));
        }
    }
    IEnumerator TalkCoroutine(KeyCode keyCode, PlayerControllerV2 pc, NPC npc) {
        int i = 0;
        ShowDialogUi();
        imageCon1.SetActive(true);
        imageCon1.GetComponent<Image>().sprite = npc.img;
        imageCon2.SetActive(true);
        imageCon2.GetComponent<Image>().sprite = pc.img;
        dialog.text = npc.printDialog(i);
        i++;
        npc.oldState = npc.state;
        npc.state = NPC.NPC_state.STAY;
        while (true) {
            npc.ChangeState(NPC.NPC_state.STAY);
            if (Input.GetKeyDown(keyCode)) {
                if (npc.printDialog(i) == null) {
                    HideDialogUi();
                    pc.state = PlayerControllerV2.CharState.NORMAL;
                    if (npc != null) {
                        npc.ResetPosition();
                        pc.ResetPosition();
                        npc.RevertState();
                    }
                    break;
                }
                dialog.text = npc.printDialog(i);
                i++;
            }
            if (Input.GetKeyDown(KeyCode.X)) {
                HideDialogUi();
                pc.state = PlayerControllerV2.CharState.NORMAL;
                if (npc != null) {
                    npc.ResetPosition();
                    npc.RevertState();
                }
                break;
            }
            yield return null;
        }
        npc.state = npc.oldState;
        npc.RevertState();
    }

    
}