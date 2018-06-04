using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete : MonoBehaviour {
    public GameObject gameManager;
    GameManager Manager;
    public int Level;
	// Use this for initialization
	void Start () {
        Manager = gameManager.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Manager.load_NewLevel(Level);
    }
}
