﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Game_PlayerControl : NetworkBehaviour {
    
    // 基本ステータス
    [SerializeField][SyncVar(hook="SyncSetup")]
    private PIG_INFO _Info;

    // プレイヤーモデルプレハブ
    [SerializeField]
    private GameObject[] _ModelPrefabs;

    // コンポーネント
    private GameObject Model = null;

    /*-----------------------------------------------------------------------------------*/
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // サーバーから切断時
    public void NetworkDestroy()
    {
        Debug.Log("RemoveGame");
    }
    /*-----------------------------------------------------------------------------------*/
    /// <summary>
    /// ゲーム開始前 基本ステータスの設定
    /// </summary>
    /// <param name="type">モデルタイプ</param>
    [Command]
    public void CmdSetupInfo(PIG_TYPE type)
    {
        // 基本ステータス取得
        _Info = _ModelPrefabs[type.GetHashCode()].GetComponent<PigInfo>()._Info;

    }
    void SyncSetup(PIG_INFO info)
    {
        _Info = info;

        // タイプのモデルを子オブジェクトとして生成
        Model = (GameObject)Instantiate(_ModelPrefabs[_Info.Type.GetHashCode()]);
        Model.transform.SetParent(this.transform);
        Model.transform.localPosition = Vector3.zero;

        // 不必要なコンポーネントを非アクティブにする
        this.GetComponent<Lobby_Player>().enabled = false;
    }
    /*-----------------------------------------------------------------------------------*/
    // スポーン座標を取得し、その座標に移動する。
    public void SetupSpawnPoint(int idx)
    {
        Transform point = GameObject.Find("SpawnPoint").transform.GetChild(idx);
        Debug.Log(point.name);
         this.transform.position = point.position;

    }
}
