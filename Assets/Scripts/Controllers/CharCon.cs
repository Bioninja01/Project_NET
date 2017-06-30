using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharCon : MonoBehaviour {

    CharaterBattle c;
    public Text hp;

	// Use this for initialization
	void Start () {
        c = GetComponent<CharaterBattle>();
        hp.text = c.Hp.ToString();
    }

    public void OnEnable() {
        CharaterBattle.notifyUi += UpdateUI;
    }
    public void OnDisable() {
        CharaterBattle.notifyUi -= UpdateUI;
    }

    public void UpdateUI(CharaterBattle character) {
        if (c.Equals(character)) {
            hp.text = c.Hp.ToString();
        }
    }

}
