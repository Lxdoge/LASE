using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCtrl : MonoBehaviour {
    public float Range=10;
    public GameObject PL;
    public GameObject PS;
    bool is_open = false;
    BoxCollider2D box;
    FireGear fire;
    
    float distance;
    float a, b;
    // Use this for initialization
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        fire = GetComponent<FireGear>();
    }

    // Update is called once per frame
    void Update()
    {
        a = Vector3.Distance(transform.position, PL.transform.position);
        b = Vector3.Distance(transform.position, PS.transform.position);
        distance = Mathf.Min(a, b);

        if (is_open)
        {
            if(distance > Range)
            {
                is_open = false;
                fire.enabled = false;
                box.enabled = false;
            }
        }
        else
        {
            if (distance <= Range)
            {
                is_open = true;
                fire.enabled = true;
                box.enabled = true;
            }
        }

    }
}
