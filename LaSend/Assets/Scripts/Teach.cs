using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Teach : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            this.GetComponent<DOTweenAnimation>().DOPlay();
        }
    }


    
}
