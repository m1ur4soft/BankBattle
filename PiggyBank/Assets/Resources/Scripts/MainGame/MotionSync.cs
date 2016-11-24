using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MotionSync : NetworkBehaviour{

    [SyncVar]
    private Vector3 syncPos; // 同期用座標
    [SyncVar]
    private Quaternion syncRot; // 同期用角度

    [SerializeField]
    private Transform _Transform;
    [SerializeField]
    private float lerpRate = 15;

    // Use this for initialization
    void Start()
    {

    }

    public void Setup()
    {
        _Transform = this.transform;
        syncPos = _Transform.position;
        syncRot = _Transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //クライアント側のTransformを取得
        TransmitMotion();
        //現在Transformと取得したTransformを補間する
        LerpMotion();
    }
    
    /*-----------------------------------------------------------------------------------*/

    // 補間するメソッド
    void LerpMotion()
    {
        if (!isLocalPlayer)
        {
            _Transform.position = Vector3.Lerp(_Transform.position, syncPos, Time.deltaTime * lerpRate);
            _Transform.rotation = Quaternion.Lerp(_Transform.rotation,syncRot, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideMotionToServer(Vector3 pos, Quaternion rot)
    {
        syncPos = pos;
        syncRot = rot;
    }

    [Client]
    void TransmitMotion()
    {
        if (isLocalPlayer)
        {
            CmdProvideMotionToServer(_Transform.position, _Transform.rotation);
        }
    }	
}
