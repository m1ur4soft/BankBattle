using UnityEngine;
using System.Collections;

public class MoveCtr : MonoBehaviour {

    /* ゲームオブジェクト */
    public GameObject _Arrow; // 矢印

    /* コンポーネント */
    private Rigidbody _rigidbody;

    /* キー入力用変数 */
    private bool isMove = false;

    /* 移動用変数 */
    public float fMoveSpeed     = 5.0f;     // 移動速度
    public float fStopSpeed     = 5.0f;     // 移動停止判定速度
    public float fStartLength   = 20.0f;    // 移動判定を行うドラッグ距離

    private Vector3 vDownpos = new Vector3();   // タッチされた座標保持用
    private float   fLength  = 0;               // ドラッグ距離
    private Vector3 vForward = new Vector3();   // 移動方向ベクトル
    //private Vector3 vAngle   = new Vector3();   // 回転用ベクトル

    /* 矢印用変数 */
    //private float fArrowScaleY = 1.0f; // Y軸ローカルスケール
    /*-----------------------------------------------------------------------------------*/
    
    // 初期化
	void Start () {
        // クライアントの操作対象であるならば、操作可能にする。
        this.enabled = this.GetComponent<NetworkView>().isMine;

        if (!this.enabled) return;
        
        // Rigidbody取得
        _rigidbody = this.GetComponent<Rigidbody>();

        SetArrowScale(0.0f);
	}
	// 更新処理
    void Update()
    {
        Transform _transform = this.transform;
        // 移動中
        if (isMove)
        {
            // 停止判定速度以下になった場合停止し、移動終了
            if(_rigidbody.velocity.magnitude <=fStopSpeed)
            {
                _rigidbody.velocity = Vector3.zero;
                isMove = false;
            }
        }
        else // 移動中ではない
        {
            // タッチされたとき
            if (Input.GetMouseButtonDown(0))
            {
                // タッチされた座標保持
                vDownpos = Input.mousePosition;

                SetArrowScale(0.0f);
            }
            // タッチされている間
            if (Input.GetMouseButton(0))
            {
                // ドラッグされている逆ベクトル取得
                vForward = vDownpos - Input.mousePosition;
                // 長さを取得
                fLength = vForward.magnitude;

                // Y軸とZ軸を入れ替える
                vForward = vForward.normalized;
                vForward.z = vForward.y;
                vForward.y = 0;

                // プレイヤーの回転
                _transform.LookAt(this.transform.position + vForward, Vector3.up);

                SetArrowScale(fLength/10.0f);
            }
            // 離されたとき
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(fLength);
                isMove = true; // 移動中に変更
                _rigidbody.AddForce(vForward * fLength * fMoveSpeed); // ドラッグした距離によって方向ベクトルに飛ばす

                SetArrowScale(0.0f);
            }
        }
    }

    private void SetArrowScale(float scaleY)
    {
        Vector3 arScale = _Arrow.transform.localScale;
        arScale.y = scaleY;
        _Arrow.transform.localScale = arScale;
    }
}
