using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Game_PlayerControl : NetworkBehaviour {
   
    

    /* プレハブ */
    [SerializeField]
    private GameObject[] _ModelPrefabs; // モデルプレハブ

    /* コンポーネント */
    private GameObject Model = null;

    /* 変数 */
    [SerializeField][SyncVar(hook = "SyncSetup")]
    private PIG_INFO _Info; // 基本ステータス
    [SyncVar]
    private bool isAttack = false; // 攻撃中フラグ

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
    // ゲーム開始前 基本ステータスの設定
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

        // 必要なコンポーネントをアクティブにする
        this.GetComponent<CapsuleCollider>().enabled = true;
        this.GetComponent<MotionSync>().enabled = true;
        this.GetComponent<MotionSync>().Setup();
        this.GetComponent<Rigidbody>().useGravity = true;
        if(isLocalPlayer)
        {
            this.GetComponent<MoveCtr>().enabled = true;
            this.GetComponent<MoveCtr>().Setup();
        }
    }
    // スポーン座標を取得し、その座標に移動する。
    public void SetupSpawnPoint(int idx)
    {
        Transform point = GameObject.Find("SpawnPoint").transform.GetChild(idx);
        Debug.Log(point.name);
         this.transform.position = point.position;

    }
    /*-----------------------------------------------------------------------------------*/
    // 衝突処理
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(isAttack)
            {
                float velScale = this.GetComponent<Rigidbody>().velocity.magnitude;
                Vector3 vForce = this.transform.forward.normalized * velScale * _Info.Power;

            }
        }
    }
    /*-----------------------------------------------------------------------------------*/
    /* セッター */
    [Command]
    public void CmdSetIsAttack(bool flag)
    {
        isAttack = flag;
    }
}
