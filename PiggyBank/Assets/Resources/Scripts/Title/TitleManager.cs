using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    float time;
    bool isMat;
    public GameObject titleUi;
    public GameObject clientUi;

    GameObject titleUiObj;
    GameObject clientUiObj;

	void Start () {
        titleUiObj = titleUi;
        clientUiObj = clientUi;
    }

    public void CreareRoom() {
        SceneManager.LoadScene("Select");
    }

    public void MatSet() {
        isMat = true;
        GetComponent<SpriteRenderer>().enabled = false;
        titleUiObj.SetActive(false);
        clientUiObj.SetActive(true);
    }

    public void Back() {
        isMat = false;
        GetComponent<SpriteRenderer>().enabled = true;
        titleUiObj.SetActive(true);
        clientUiObj.SetActive(false);
    }

    public void JoinRoom() {
        if(isMat) SceneManager.LoadScene("Select");
    }

    public void Setting() {
        isMat = true;
        GetComponent<SpriteRenderer>().enabled = false;
        titleUiObj.SetActive(false);
    }
	
	void Update () {
        time += Time.deltaTime;
        if (time >= 3) {
            GetComponent<Animator>().SetTrigger("animTrigger");
            time = 0;
        }
	}
}
