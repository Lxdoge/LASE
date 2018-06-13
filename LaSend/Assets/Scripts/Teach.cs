using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Teach : MonoBehaviour {
    public DOTweenAnimation[] danima;
    private static int can_load;
	// Use this for initialization
	void Start () {
        can_load = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(can_load==3)
        {
            //切换场景
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.name)
        {
            case "S1":

                can_load++;
                break;
            case "S2": can_load++; break;
            case "end": can_load++; break;
            default:break;
        }
    }

    
}
