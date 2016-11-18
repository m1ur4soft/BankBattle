using UnityEngine;
using System.Collections;

public class Lobby_Status : MonoBehaviour {

    private StarValue[] Stars;

	// Use this for initialization
	void Start () {
        Stars = GetComponentsInChildren<StarValue>();
	}
	
    public void SetStarsValue(PigInfo info)
    {
        Stars[0].SetValue(info._Info.Speed);
        Stars[1].SetValue(info._Info.Power);
        Stars[2].SetValue(info._Info.Size);
        Stars[3].SetValue(info._Info.Weight);
    }
}
