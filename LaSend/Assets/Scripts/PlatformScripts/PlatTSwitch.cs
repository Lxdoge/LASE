using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatTSwitch : MonoBehaviour {
    public GameObject Up;
    public GameObject Down;

    public bool L;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (L)
        {
            if(collision.tag == "LPlayer")
            {
                Up.SetActive(true);
                Down.SetActive(true);
            }
        }
        else
        {
            if(collision.tag == "SPlayer")
            {
                Up.SetActive(true);
                Down.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (L)
        {
            if (collision.tag == "LPlayer")
            {
                Up.SetActive(false);
                Down.SetActive(false);
            }
        }
        else
        {
            if (collision.tag == "SPlayer")
            {
                Up.SetActive(false);
                Down.SetActive(false);
            }
        }
    }
}
