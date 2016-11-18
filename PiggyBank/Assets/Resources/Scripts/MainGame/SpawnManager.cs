using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
    public GameObject coin;
    GameObject coinManager;
    public Color goald;
    public Color silver;
    public Color copper;

	void Start () {
        coinManager = GameObject.Find("CoinManager");

        for (int i = 0; i < 50; i++) {
            //生成ポジション決定
            float x = Random.Range(-0.5f, 8.5f);
            float y = Random.Range(1.0f, 5.0f);
            float z = Random.Range(-0.5f, 8.5f);
            Vector3 pos = new Vector3(x, y, z);

            //生成
            GameObject obj = (GameObject)Instantiate(coin, pos, Quaternion.identity);
            //回転値
            obj.transform.Rotate(90, Random.Range(0.0f, 360.0f), 0);
            //coinManagerを親にして生成
            obj.transform.parent = coinManager.transform;

            //コインの色を指定
            int j = Random.Range(0, 3);
            if (j == 0) obj.GetComponent<Renderer>().material.color = goald;
            if (j == 1) obj.GetComponent<Renderer>().material.color = silver;
            if (j == 2) obj.GetComponent<Renderer>().material.color = copper;

            //サイズをランダムで設定
            float mag = Random.Range(0.2f, 0.3f);
            Vector3 scale = new Vector3(mag, mag, mag);
            obj.transform.localScale = scale;
        }
	}
	
	void Update () {
	
	}
}
