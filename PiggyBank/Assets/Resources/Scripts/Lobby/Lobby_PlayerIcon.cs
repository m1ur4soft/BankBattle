using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lobby_PlayerIcon : MonoBehaviour {

    // 状態管理用フラグ
    public bool isUse = false;  // 使用中
    public bool isReady = false; // 準備完了
    public bool isHost = false; // ホストかどうか

    // コンポーネント
    private Animator _Anim = null;
    [SerializeField]
    private Image _ReadyImg;
    [SerializeField]
    private Image _HostImg;

    void Start()
    {
        // コンポーネント取得
        _Anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 使用中状態(true)か未使用状態(false)に変更する
    /// </summary>
    /// <param name="flag">true=使用状態へ false=未使用状態へ</param>
    public void SetIsUseIcon(bool flag)
    {
        isUse = flag;

        // 変更する使用状態によってアニメーショントリガーを設定する。
        if (isUse)
        {
            _Anim.SetTrigger("trgOpen");
        }
        else
        {
            _Anim.SetTrigger("trgClose");
        }
    }
    /// <summary>
    /// 準備完了状態(true)か準備中状態(false)に変更する。
    /// </summary>
    /// <param name="flag">true=準備完了状態へ false=準備中状態へ</param>
    public void SetIsReadyIcon(bool flag)
    {
        isReady = flag;

        _ReadyImg.enabled = isReady;
    }
    /// <summary>
    /// ホストフラグとアイコンを設定する
    /// </summary>
    /// <param name="flag">true=ホスト false=非ホスト</param>
    public void SetIsHostIcon(bool flag)
    {
        isHost = flag;

        _HostImg.enabled = flag;
    }
}
