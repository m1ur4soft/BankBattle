using UnityEngine;
using System.Collections;

public class Lobby_DisplayModel : MonoBehaviour {

    private PigInfo _info = null;

    // 回転用変数
    public float fRotSpeed ;
    bool isRot = false;

	// Use this for initialization
	void Start () {
        _info = GetComponentInChildren<PigInfo>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isRot)
        {
            transform.Rotate(0.0f, fRotSpeed, 0.0f);
        }
	}

    public void StartRotation()
    {
        isRot = true;
    }
    public void StopRotation()
    {
        isRot = false;
        transform.rotation = new Quaternion(0,0,0,0);
    }

    public PigInfo GetPigInfo()
    {
        return _info;
    }
}
