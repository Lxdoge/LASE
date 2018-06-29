using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {
    public Sprite pic;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LPlayer" || collision.tag == "SPlayer")
        {
            this.GetComponent<SpriteRenderer>().sprite = pic;
        }
    }
}
