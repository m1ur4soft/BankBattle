using UnityEngine;
using System.Collections;

public class OpenEdge : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Animator>().SetTrigger("openTrigger");
        }
	}
}
