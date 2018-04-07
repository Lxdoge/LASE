using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatNCtrl : MonoBehaviour {
    bool HitSimulate;
    public float hitTime;
    public float flowTime;//冗余时间
    public float moveDis;
    float Timer;
    Vector3 initPositon;
	// Use this for initialization
	void Start () {
        HitSimulate = false;
        Timer = hitTime;
        initPositon = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (HitSimulate)
        {
            Timer -= Time.deltaTime;
            if (Timer >= hitTime / 2)
            {
                transform.Translate(Vector3.down * moveDis * Time.deltaTime);
            }
            else if (Timer < hitTime / 2&& Timer >= 0)
            {
                transform.Translate(Vector3.up * moveDis * Time.deltaTime);
            }
            else if(Timer < 0&&Timer > -flowTime)
            {
                transform.position = initPositon;
            }
            else if(Timer <= -flowTime)
            {
                Timer = hitTime;
                HitSimulate = false;
            }
        }

	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitSimulate = true;
    }
}
