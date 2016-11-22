using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CoinManager : NetworkBehaviour {
    [Range(0, 12000)]
    public int totalCoinAmount; // 作り出すコインすべてをあわせた量(枚数ではなく価値)

    //各コインの情報
   // public int[] coinValue; // コインの価値を付加する。

    //public GameObject[] createCoins; // 作り出すコイン一覧。6種類。

    //構造体として1つの配列に2つの情報を格納。
    public Coins[] coins;
    [System.Serializable]
    public struct Coins
    {
        public GameObject model;
        public int value;
    }

    [SerializeField]
    float createCoinRadius; // コインの作り出される範囲の設定。

    public override void OnStartServer()
    {
        base.OnStartServer();
        CreateCoin();
    }
    // Use this for initialization
    void Start () {
        //CreateCoin();

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void CoinGenerate(int coinNo, float createRate)
    {
        int createCount = 0;
        // 出したい金額の合計が今から生成する分の値を持っている間まわす。
        while (totalCoinAmount >= coins[coinNo].value && createCount < 30)
        {
            if (totalCoinAmount / coins[coinNo].value >= createRate)
            {
                //毎回同じポジションにならないようにオブジェクトの±２の範囲内で生み出す。
                Vector3 createRandPos = new Vector3(Random.Range(this.transform.position.x - createCoinRadius, this.transform.position.x + createCoinRadius),
                Random.Range(this.transform.position.y, this.transform.position.y + createCoinRadius), Random.Range(this.transform.position.z - createCoinRadius, this.transform.position.z + createCoinRadius));
                GameObject obj = Instantiate(coins[coinNo].model, createRandPos, Quaternion.Euler(90, 0, 0)) as GameObject;
                obj.transform.parent = transform;
                //生成したコインの価値だけtotalCoinAmountから差し引く。
                totalCoinAmount -= coins[coinNo].value;
                createCount++;
            } else
            {
                break;
            }
        }
        createCount = 0;

    }

    void CreateCoin()
    {
        //0.LGold 1.LSilver 2.SGold 3.LBronze 4.SSilver 5.SBronze
        //金大メダルの確率は1パーセント
        //第一引数はコイン番号。第二引数はコインを生成する際どれぐらいの確率で出るかの数値。
        //金大と銅小はこれで決定…中間の調整をどうするか
        CoinGenerate(0, 10);
        CoinGenerate(1, 15);
        CoinGenerate(2, 10);
        CoinGenerate(3, 5);
        CoinGenerate(4, 3);
        CoinGenerate(5, 1);
    }
}
