using UnityEngine;
using System.Collections;

// 貯金箱のタイプ識別子
public enum PIG_TYPE
{
    BALANCE=0,
    SPEED,
    ATTACK,
    GUARD,

    TYPE_NUM,
}
[System.Serializable]
public struct PIG_INFO
{
    public PIG_TYPE Type; // タイプ
    public int Speed;  // 速さ
    public int Power;  // パワー
    public int Size;   // サイズ
    public int Weight; // 重量
}
[System.Serializable]
public class PigInfo : MonoBehaviour {

    [SerializeField]
    public PIG_INFO _Info;

}
