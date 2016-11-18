using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputText : MonoBehaviour {
    string str;
    public InputField inputField;
    public Text text;

    public void SaveText() {
        str = inputField.text;
        text.text = str;
        inputField.text = "";
    }

	void Start () {
	
	}
	
	void Update () {
	
	}
}
