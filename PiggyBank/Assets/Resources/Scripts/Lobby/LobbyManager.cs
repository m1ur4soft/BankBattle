using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LobbyManager : NetworkBehaviour {

    [SyncVar(hook="GameStart")]
    private bool isGameStart = false; // ゲームスタートフラグ

    private ArrayList players = new ArrayList(); // プレイヤーリスト
    private Lobby_Player localPlayer = null; // ローカルプレイヤー
    [SerializeField]
    private Lobby_IconManager iconManager; // UIのプレイヤーアイコンマネージャー

    /*-----------------------------------------------------------------------------------*/
    void Start()
    {
    }
    
    /*-----------------------------------------------------------------------------------*/
    // プレイヤーリストに追加
    public void AddPlayer(GameObject player)
    {
        // ローカルプレイヤーではないならリストに追加する
       if(! player.GetComponent<Lobby_Player>().isLocalPlayer)
       {
           // 順序を設定しリストに追加
           int order = GetPlayerOrder();
           players.Insert(order,player);
           player.GetComponent<Lobby_Player>().nOrder = order;
           // UIアイコンに適用
           iconManager.SetUsingIcons(true, order);
           iconManager.SetReadyIcons(player.GetComponent<Lobby_Player>().isReady, order);
           iconManager.SetIsHost(player.GetComponent<Lobby_Player>().isHost, order);
           
       }
       else // ローカルプレイヤーならリストには追加せず、保持する。
       {
           localPlayer = player.GetComponent<Lobby_Player>();
       }
    }
    // プレイヤーリストから削除
    public void RemovePlayer(GameObject player)
    {
        if (players.Count == 0) return;
        // UIアイコンに適用
        int order = player.GetComponent<Lobby_Player>().nOrder;
        iconManager.SetUsingIcons(false, order);
        iconManager.SetReadyIcons(false, order);
        
        players.Remove(player);

        Debug.Log(players.Count);
    }
    // 登録するリスト順序を取得する
    int GetPlayerOrder()
    {
        int order = 0;
        for(int i=0;i<players.Count;i++)
        {
            if (((GameObject)players[i]).GetComponent<Lobby_Player>().nOrder != i)
            {
                order = i;
                break;
            }
            // 最後尾の場合
            if(players.Count-1 == i)
            {
                order = i + 1;
                break;
            }
        }
        return order;
    }

    /*-----------------------------------------------------------------------------------*/
    // 準備ボタン処理
    public void OnReadyButton()
    {
        localPlayer.CmdReady();
    }
    // プレイヤーアイコンの準備状態を設定する
    public void SetPlayerIconReady(Lobby_Player player)
    {
        int order = player.gameObject.GetComponent<Lobby_Player>().nOrder;
        iconManager.SetReadyIcons(player.isReady, order);
    }
    // プレイヤーの準備状態をチェックする
    public bool CheckPlayersReady()
    {
        bool isAllReady = false;

        if(localPlayer.isServer)
        {
            foreach(GameObject player in players)
            {
                isAllReady = true;

                if(!player.GetComponent<Lobby_Player>().isReady)
                {
                    isAllReady = false;
                    break;
                }
            }
        }
        return isAllReady;
    }
    // スタートボタン処理
    public void OnGameStartButton()
    {
        if (isGameStart) return;

        //if(!CheckPlayersReady())return;

        isGameStart = true;
    }
    // ゲームスタート処理
    void GameStart(bool start)
    {
        // フラグ切替
        isGameStart = start;

        // プレイヤーのセットアップ
        localPlayer.GetComponent<Game_PlayerControl>().enabled = true;
        foreach(GameObject go in players)
        {
            go.GetComponent<Game_PlayerControl>().enabled = true;
        }
        localPlayer.GetComponent<Lobby_Player>().SetupPlayer();

        // MainGameシーンへ遷移
        GameManager.Instance.ChangeMode(GAME_MODE.MAIN_GAME);
    }
    /*-----------------------------------------------------------------------------------*/
    // 切断ボタン
    public void OnStopHost()
    {
        NetworkManager.singleton.StopHost();
    }
}
