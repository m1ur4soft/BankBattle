using UnityEngine;
using System.Collections;

public class MoveCtr : MonoBehaviour {

    /* ゲームオブジェクト */
    public GameObject _Arrow; // 矢印

    /* コンポーネント */
    private Rigidbody _rigidbody;
    private Game_PlayerControl _Control;

    /* キー入力用変数 */
    private bool isMove = false;

    /* 移動用変数 */
    public float fMoveSpeed     = 5.0f;     // 移動速度
    public float fStopSpeed     = 5.0f;     // 移動停止判定速度
    public float fStartLength   = 20.0f;    // 移動判定を行うドラッグ距離

    private Vector3 vDownpos = new Vector3();   // タッチされた座標保持用
    private float   fLength  = 0;               // ドラッグ距離
    private Vector3 vForward = new Vector3();   // 移動方向ベクトル
    
    /*-----------------------------------------------------------------------------------*/
    public void Setup()
    {
        // Rigidbody取得
        _rigidbody = this.GetComponent<Rigidbody>();
        // Game_PlayerControl取得
        _Control = this.GetComponent<Game_PlayerControl>();
        // 矢印オブジェクト生成
        _Arrow = (GameObject)Instantiate(_Arrow);
        _Arrow.transform.SetParent(this.transform);
        _Arrow.transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
        _Arrow.transform.localScale = new Vector3(1.5f, 0.0f, 1.5f);
    }
    // 初期化
	void Start () {

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
                _Control.CmdSetIsAttack(false);
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
                // 矢印の大きさ変更
                SetArrowScale(fLength/10.0f);
            }
            // 離されたとき
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(fLength);
                // 攻撃中に変更
                _Control.CmdSetIsAttack(true);
                // 移動中に変更
                isMove = true; 
                // ドラッグした距離によって方向ベクトルに飛ばす
                _rigidbody.AddForce(vForward * fLength * fMoveSpeed); 
                // 矢印の大きさ変更
                SetArrowScale(0.0f);
            }
        }
    }

    // 矢印のY軸スケールを指定した値に変更する
    private void SetArrowScale(float scaleY)
    {
        Vector3 arScale = _Arrow.transform.localScale;
        arScale.y = scaleY;
        _Arrow.transform.localScale = arScale;
    }
}
