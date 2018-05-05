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
        Debug.Log(1);
        if (collision.tag == "LPlayer")
        {
            Debug.Log(0);
            manager.gameover = true;
        }
    }
}
