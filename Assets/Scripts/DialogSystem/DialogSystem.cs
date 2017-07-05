using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DialogSystem : MonoBehaviour {

    public GameObject overlay;
    public GameObject dialogBox;
    public GameObject imageCon1;
    public GameObject imageCon2;
    public GameObject choices;

    public SlectionChip slection;

    public Text dialog;
    private Image img_charater1;
    private Image img_charater2;

    bool choiceFlag = false;
    string[] options;

    // Use this for initialization
    void Start () {
        img_charater1 = imageCon1.GetComponent<Image>();
        img_charater2 = imageCon2.GetComponent<Image>();
        HideDialogUi();
	}
    public void ShowDialogUi() {
        overlay.SetActive(true);
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
        PlayerController.Talk += StartTalkCoroutine;
    }
    public void OnDisable() {
        PlayerController.Talk -= StartTalkCoroutine;
    }

/*Main Logic of DialogSystem*/
    private void StartTalkCoroutine(PlayerController pc, GameObject go) {
        NPC npc = go.GetComponent<NPC>();
        if(npc != null) {
            StartCoroutine(TalkCoroutine( pc, npc));
        }
    }
    IEnumerator TalkCoroutine( PlayerController pc, NPC npc) {
        int lineNumber = 0;
        ShowDialogUi();
        imageCon1.SetActive(true);
        imageCon1.GetComponent<Image>().sprite = npc.img;
        imageCon2.SetActive(true);
        imageCon2.GetComponent<Image>().sprite = pc.img;
        dialog.text = npc.printDialog(lineNumber);
        lineNumber++;
        npc.oldState = npc.state;
        npc.state = NPC.NPC_state.STAY;
        while (true) {
            npc.ChangeState(NPC.NPC_state.STAY);
            //Gets "string" reference to choise when curser moves.
            if (npc.waitForChoise(lineNumber)) {
                options = npc.printDialog(lineNumber).Split(' ');
                for (int index = 1; index < options.Length; index++) {
                    string[] opAndlocation = options[index].Split(':');
                    choices.transform.GetChild(index - 1).GetComponentInChildren<Text>().text = opAndlocation[0];
                }
                choices.SetActive(true);
                choiceFlag = true;
                lineNumber++;
            }
//Progress dialog
            if (Input.GetKeyDown(KeyCode.G)) {
                if (npc.printDialog(lineNumber) == null) {
                    HideDialogUi();
                    pc.state = PlayerController.CharState.NORMAL;
                    if (npc != null) {
                        npc.ResetPosition();
                        pc.ResetPosition();
                        npc.RevertState();
                    }
                    break;
                }
//Player choices Action
                if (choiceFlag) {
                    for (int index = 1; index < options.Length; index++) {
                        string[] choiceAndlocation = options[index].Split(':');
                        if (choiceAndlocation[0] == slection.getSelection()) {
                            lineNumber = Int32.Parse(choiceAndlocation[1]);
                            dialog.text = npc.printDialog(lineNumber-1);  // linenumber-1 because .txt file is not zero index. 

                            HideDialogUi();                      
                            Animator animator = pc.GetComponent<Animator>();
                            animator.Play(choiceAndlocation[0], 0);
                            yield return new WaitForEndOfFrame(); // have to wait for the animator to change the animationClip.
                            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // wait for the animation to finish.

                            ShowDialogUi();                            
                        }
                    }
                    choiceFlag = false;
                    choices.SetActive(false);
                }
                else {
                    dialog.text = npc.printDialog(lineNumber);
                    lineNumber++;
                } 
            }
// End dialog 
            if (Input.GetKeyDown(KeyCode.X)) {
                HideDialogUi();
                pc.state = PlayerController.CharState.NORMAL;
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
        pc.GetComponent<Animator>().Play("BackToNormal", 0);
    }

    void ProformAction(PlayerController pc, int clipNumber) {
        Animator animator = pc.GetComponent<Animator>();
        AnimationClip clip = animator.runtimeAnimatorController.animationClips[clipNumber];
        AnimationEvent evt = new AnimationEvent();
        evt.time = clip.length;

        animator.Play("Walk", 0);
    }

    
}