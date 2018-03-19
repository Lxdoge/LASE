using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSkillCtrl : MonoBehaviour {
    public float rotateSpeed;
    public GameObject lPlayer;
    public bool skillon;
    float xAxis;
    float dir;
    float deltScale;
    // Use this for initialization
    private void Awake()
    {
        deltScale = 0;
    }
    void Start () {
        skillon = false;
        xAxis = 0.0f;
        dir = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {/*
        if(transform.localScale.x < 0.25f && skillon)
        {
            deltScale += 0.25f * Time.deltaTime;
            transform.localScale = new Vector3(deltScale, deltScale, 0);
        }
        else if(!skillon)
        {
            if (deltScale != 0) {
                deltScale = 0;
                transform.localScale = new Vector3(deltScale, deltScale, 0);
            }
                
        }
        transform.position = lPlayer.transform.position + new Vector3(0.038f, -0.074f, 0);
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));*/
        RotateOne();
	}
    //水平绕转
    void RotateOne()
    {
        xAxis += dir * rotateSpeed * Time.deltaTime;
        if (Mathf.Abs(xAxis) >= 180.0f)
            dir = -dir;
        if (xAxis < 0)
            deltScale += rotateSpeed * Time.deltaTime;
        else if (xAxis > 0)
            deltScale -= rotateSpeed * Time.deltaTime;
        transform.position = new Vector3(xAxis, 0, 0) / 540.0f + lPlayer.transform.position;
        transform.localScale = new Vector3(deltScale, deltScale, 0) / 3600.0f;
    }
}
