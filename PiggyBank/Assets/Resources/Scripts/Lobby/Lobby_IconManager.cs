using UnityEngine;
using System.Collections;

public class Lobby_IconManager : MonoBehaviour {

    private Lobby_PlayerIcon[] icons = null;

	// Use this for initialization
	void Start () {
        icons = GetComponentsInChildren<Lobby_PlayerIcon>();
	}
	
    /// <summary>
    /// プレイヤーアイコンの使用状態を設定する
    /// </summary>
    /// <param name="flag">プレイヤーアイコンに設定するフラグ</param>
    /// <param name="idx">設定するアイコンインデックス</param>
    public void SetUsingIcons(bool flag,int idx)
    {
        icons[idx].SetIsUseIcon(flag);
    }
    /// <summary>
    /// プレイヤーアイコンの準備状態を設定する
    /// </summary>
    /// <param name="flag">プレイヤーアイコンに設定するフラグ</param>
    /// <param name="idx">設定するアイコンインデックス</param>
    public void SetReadyIcons(bool flag,int idx)
    {
        icons[idx].SetIsReadyIcon(flag);
    }
    /// <summary>
    /// ホストアイコンの設定
    /// </summary>
    /// <param name="flag">ホストフラグに設定する</param>
    /// <param name="idx">設定するアイコンインデックス</param>
    public void SetIsHost(bool flag,int idx)
    {
        icons[idx].SetIsHostIcon(flag);
    }
}
