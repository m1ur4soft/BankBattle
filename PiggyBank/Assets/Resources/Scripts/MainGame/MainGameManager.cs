using UnityEngine;
using System.Collections;

public class MainGameManager : MonoBehaviour {

    private ArrayList players = new ArrayList();

    void Awake()
    {
        Game_PlayerControl[] playerObjs = GameObject.FindObjectsOfType(typeof(Game_PlayerControl)) as Game_PlayerControl[];
        for(int i=0;i<playerObjs.Length;i++)
        {
            playerObjs[i].SetupSpawnPoint(i);
            players.Add(playerObjs[i].gameObject);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
