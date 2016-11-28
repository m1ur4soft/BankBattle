using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Lobby_Player : NetworkBehaviour {

    [SyncVar]
    public bool isHost = false; // ホストフラグ

    [SyncVar(hook="SetIsReady")]
    public bool isReady = false;   // 準備フラグ

    public int nOrder = 0; // プレイヤー順序
    
    [SyncVar]
    private PIG_TYPE SelectType = PIG_TYPE.BALANCE; // 選択中タイプ

    // コンポーネント
    private LobbyManager _lobbyManager = null; // ロビーマネージャー

    /*-----------------------------------------------------------------------------------*/
    void Start()
    {
        // ロビーで不必要なコンポーネントを非アクティブにする
        this.GetComponent<Game_PlayerControl>().enabled = false;
        this.GetComponent<MoveCtr>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<NetworkTransform>().enabled = false;
        this.GetComponent<MotionSync>().enabled = false;

        // ロビーマネージャーの取得と自身をリストに追加
        _lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        _lobbyManager.AddPlayer(this.gameObject);
    }
    public override void OnStartLocalPlayer()
    {
        // 自身をUIのターゲットに設定
        GameObject.Find("LobbyPlayerUI").GetComponent<Lobby_PlayerUI>().SetTarget(this.gameObject);
       
        // SelectManagerに自身を設定
        GameObject.Find("SelectManager").GetComponent<Lobby_SelectManager>().SetLocalPlayer(this);
       
        // ホストフラグの設定
        CmdHost(isServer);

        // ボタン取得
        GameObject sb = GameObject.Find("StartButton"); // スタートボタン
        GameObject rb = GameObject.Find("ReadyButton"); // レディボタン

        // サーバーの場合の設定
        if(isServer)
        {
            //スタートボタンをアクティブにする
            sb.GetComponent<Image>().enabled = true;
            sb.GetComponent<Button>().enabled = true;
            
            rb.GetComponent<Image>().enabled = false;
            rb.GetComponent<Button>().enabled = false;
        }else // クライアントの場合
        {

            // レディボタンをアクティブにする
            sb.GetComponent<Image>().enabled = false;
            sb.GetComponent<Button>().enabled = false;

            rb.GetComponent<Image>().enabled = true;
            rb.GetComponent<Button>().enabled = true;
        }
    }
    // サーバーから切断時
    public void NetworkDestroy()
    {
        // リストから削除
        _lobbyManager.RemovePlayer(this.gameObject);
    }

    /*-----------------------------------------------------------------------------------*/
    // Command Method
    // Host
    [Command]
    public void CmdHost(bool flag)
    {
        isHost = flag;
    }
    // Ready
    [Command]
    public void CmdReady()
    {
        isReady = !isReady;
    }
    // SelectType
    [Command]
    public void CmdSetSelectType(PIG_TYPE type)
    {
        SelectType = type;
    }
    /*-----------------------------------------------------------------------------------*/
    // 準備フラグを設定し、UIアイコンに適用
    void SetIsReady(bool flag)
    {
        isReady = flag;

        if (!isLocalPlayer)
        {
            _lobbyManager.SetPlayerIconReady(this);
        }
    }
    /*-----------------------------------------------------------------------------------*/
    // ゲーム開始前設定
    public void SetupPlayer()
    {
        this.GetComponent<Game_PlayerControl>().CmdSetupInfo(SelectType);

        this.enabled = false;
    }
}
