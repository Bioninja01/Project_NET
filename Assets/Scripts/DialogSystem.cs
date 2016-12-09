using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour {
    public GameObject overlay;
    public GameObject dialogBox;
    public GameObject imageCon1;
    public GameObject imageCon2;

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
        dialogBox.SetActive(true);
    }
    public void HideDialogUi() {
        overlay.SetActive(false);
        dialogBox.SetActive(false);
        imageCon1.SetActive(false);
        imageCon2.SetActive(false);
    }

    public void OnEnable() {
        Charater.Talk += StartTalkCoroutine;
    }
    public void OnDisable() {
        Charater.Talk -= StartTalkCoroutine;
    }

    private void StartTalkCoroutine(Charater c, GameObject go) {
        NPC npc = go.GetComponent<NPC>();
        StartCoroutine(TalkCoroutine(KeyCode.G, c, npc));
    }
    IEnumerator TalkCoroutine(KeyCode keyCode, Charater c, NPC npc) {
        int i = 0;
        ShowDialogUi();
        imageCon1.SetActive(true);
        imageCon1.GetComponent<Image>().sprite = npc.img;
        dialog.text = npc.printDialog(i);
        i++;
        while (true) {
            if (Input.GetKeyDown(keyCode)) {
                if (npc.printDialog(i) == null) {
                    HideDialogUi();
                    c.state = Charater.CharState.NORMAL;
                    if (npc != null) {
                        npc.ResetPosition();
                        c.ResetPosition();
                    }
                    break;
                }
                dialog.text = npc.printDialog(i);
                i++;
            }
            if (Input.GetKeyDown(KeyCode.X)) {
                HideDialogUi();
                c.state = Charater.CharState.NORMAL;
                if (npc != null) {
                    npc.ResetPosition();
                }
                break;
            }
            yield return null;
        }
    }
}