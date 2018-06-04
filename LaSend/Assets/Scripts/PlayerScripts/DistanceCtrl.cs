using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCtrl : MonoBehaviour {
    public GameObject lPlayer;
    public GameObject sPlayer;
    public float Xmax;
    public float Ymax;
    Transform LT, ST;
    LPlayerCtrl LC;
    SPlayerCtrl SC;
	// Use this for initialization
	void Start () {
        LT = lPlayer.transform;
        ST = sPlayer.transform;
        LC = lPlayer.GetComponent<LPlayerCtrl>();
        SC = sPlayer.GetComponent<SPlayerCtrl>();
	}
	
	// Update is called once per frame
	void Update () {
        float deltX = Mathf.Abs(LT.position.x - ST.position.x);
        float deltY = Mathf.Abs(LT.position.y - ST.position.y);
        if(deltX > Xmax || deltY > Ymax)
        {
            LC.death = true;
            SC.death = true;
        }
	}
}
