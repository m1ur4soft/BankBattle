using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// プレイヤー判別ID
public enum PLAYER_NUM
{
    NONE = 0,
    PLAYER1,
    PLAYER2,
    PLAYER3,
    PLAYER4,
}

public class Lobby_PlayerUI : MonoBehaviour
{
    public GameObject _Target = null;// ターゲット

    /*-----------------------------------------------------------------------------------*/
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*-----------------------------------------------------------------------------------*/
    // ターゲットを設定する
    public void SetTarget(GameObject obj)
    {
        _Target = obj;
    }
    /*-----------------------------------------------------------------------------------*/
    // ボタン入力処理
    // 右ボタン
    public void OnRightButton()
    {

    }
    // 左ボタン
    public void OnLeftButton()
    {

    }
}
