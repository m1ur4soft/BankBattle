using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarValue : MonoBehaviour {

    public GameObject[] Stars;
    public float MaXValue=100.0f;
    public float Value;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            SetValue(Value);
        }
	}

    public void SetValue(float val)
    {
        Value = val;
        float rateVal = Value / (MaXValue / Stars.Length);
        foreach(GameObject go in Stars)
        {
            Image img = go.transform.FindChild("StarValue").GetComponent<Image>();
            if (rateVal >= 1.0f)
            {
                img.fillAmount = 1.0f;
                rateVal -= 1.0f;
            }
            else
            {
                img.fillAmount = rateVal;
                rateVal = 0.0f;
            }

        }
        
    }
}
