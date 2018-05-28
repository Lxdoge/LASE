using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour {
    public GameObject lPlayer;
    public GameObject sPlayer;
    LPlayerCtrl lpCtrl;
    SPlayerCtrl spCtrl;
	// Use this for initialization
	void Start () {

        lpCtrl = lPlayer.GetComponent<LPlayerCtrl>();
        spCtrl = sPlayer.GetComponent<SPlayerCtrl>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LPlayer" || collision.tag == "SPlayer")
        {
            lpCtrl.death = true;
            spCtrl.death = true;
        }
    }
}
