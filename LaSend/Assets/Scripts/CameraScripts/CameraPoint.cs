using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour {
    public GameObject LPlayer;
    public GameObject SPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(LPlayer.transform.position.x + SPlayer.transform.position.x, LPlayer.transform.position.y + SPlayer.transform.position.y, 0) / 2;
	}
}
