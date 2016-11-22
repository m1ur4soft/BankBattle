using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*-----------------------------------------------------------------------------------*/
    // ボタン処理
    // ホストとして部屋を作成
    public void OnCreateHost()
    {
        NetworkManager.singleton.StartHost();
    }
    // クライアントとして部屋に参加
    public void OnJoinGame()
    {
        string ip = GameObject.Find("InputIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ip;

        NetworkManager.singleton.StartClient();
    }
}
