using UnityEngine;
using System.Collections;

public class Lobby_SelectManager : MonoBehaviour {

    private PIG_TYPE type = PIG_TYPE.BALANCE; // 選択中タイプ

    // コンポーネント
    private Lobby_DisplayModel[] models; // キャラクターモデル
    private Lobby_Status statusUI; // ステータスUI
    private Lobby_Player localPlayer=null; // ローカルプレイヤー

    // 選択回転 変数
    private bool isRot = false; // 回転中か
    private float fAddRot = 90.0f; // 回転加算値
    [Range(0.0f,1.0f)]
    public float fRotSpeed = 1.0f; // 回転速度

    /*-----------------------------------------------------------------------------------*/
	void Start()
    {
        // モデルを取得し、モデルの回転を開始する。
        models = GetComponentsInChildren<Lobby_DisplayModel>();
        models[(int)type].StartRotation();
        // ステータスUIを取得し、選択中の貯金箱の情報を設定
        statusUI = GameObject.Find("Status").GetComponent<Lobby_Status>();
        statusUI.SetStarsValue(models[(int)type].GetPigInfo());
    }

    /*-----------------------------------------------------------------------------------*/
    // ローカルプレイヤーを設定
    public void SetLocalPlayer(Lobby_Player _Player)
    {
        localPlayer = _Player;
    }
    // 値の設定処理
    void SetComponentValue()
    {
        // ステータスUIに情報を設定
        statusUI.SetStarsValue(models[(int)type].GetPigInfo());

        // ローカルプレイヤーに選択タイプを渡す
        localPlayer.CmdSetSelectType(type);
    }

    /*-----------------------------------------------------------------------------------*/
    // 選択回転開始前設定
    public void OnSetupRotation(bool isRigth)
    {
        // 回転中は開始しない
        if (isRot) return;
        // 選択中のモデルの回転を停止
        models[(int)type].StopRotation();
        // 次のタイプへ設定 左右フラグで計算変更
        if (isRigth)
        {
            type = (PIG_TYPE)((((int)type + 1)) % (int)PIG_TYPE.TYPE_NUM);
        }else
        {
            type = (PIG_TYPE)((((int)type - 1 + ((int)PIG_TYPE.TYPE_NUM))) % (int)PIG_TYPE.TYPE_NUM);
        }
        // 各コンポーネントの値を設定
        SetComponentValue();

        // 回転コルーチン開始
        StartCoroutine(this.StartRotation(isRigth));
    }
    
    // 選択回転コルーチン
    IEnumerator StartRotation(bool isRight)
    {
        // 左右設定によって回転増加値と回転速度の符号を設定
        float addRot = fAddRot;
        if (!isRight)
        {
            addRot *= -1;
        }
        // 目的回転値を設定
        Vector3 vNextRot = transform.rotation.eulerAngles;
        vNextRot.y += addRot;

        Quaternion qNextRot = Quaternion.Euler(vNextRot);
        Quaternion qStartRot = transform.rotation;
        isRot = true;

        float rotTime = fRotSpeed;
        float startTime = Time.timeSinceLevelLoad;

        while(true)
        {
            // MoveTime秒間処理する。
            float diff = Time.timeSinceLevelLoad - startTime;
           
            if (diff >= rotTime)
            {
                transform.rotation = qNextRot ;
                break;
            }
           
            float rate = diff / rotTime;
            transform.rotation = Quaternion.Lerp(qStartRot, qNextRot, rate);
            

            
            yield return null;
        }
        isRot = false;
        models[(int)type].StartRotation();
    }
    
}
