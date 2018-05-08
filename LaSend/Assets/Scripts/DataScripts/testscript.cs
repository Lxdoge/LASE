using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour {
    public GameObject gameManager;
    GameManager manager;
	// Use this for initialization
	void Start () {
        manager = gameManager.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LPlayer")
            collision.GetComponent<LPlayerCtrl>().death = true;
        if (collision.tag == "SPlayer")
            collision.GetComponent<SPlayerCtrl>().death = true;
    }
}
