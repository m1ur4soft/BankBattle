using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayerManager : NetworkBehaviour {

    // プレイヤー順序
    public int nOrder = 0;

    void Start()
    {
        // シーン遷移で削除されないように設定
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public override void OnNetworkDestroy()
    {
        switch(GameManager.Instance._Mode)
        {
            case GAME_MODE.LOBBY:
                this.GetComponent<Lobby_Player>().NetworkDestroy();
                
                break;
            case GAME_MODE.MAIN_GAME:
                this.GetComponent<Game_PlayerControl>().NetworkDestroy();
                break;
            default:
                break;
        }
        
    }
}
