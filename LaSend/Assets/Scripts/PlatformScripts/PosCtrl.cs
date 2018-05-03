using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCtrl : MonoBehaviour {
    public float Range=10;
    public GameObject PL;
    public GameObject PS;
    public bool is_open = false;
    float distance;
    float a, b;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        a = Mathf.Sqrt(Mathf.Pow((this.transform.position.x - PL.transform.position.x), 2) + Mathf.Pow((this.transform.position.y - PL.transform.position.y), 2));
        b = Mathf.Sqrt(Mathf.Pow((this.transform.position.x - PS.transform.position.x), 2) + Mathf.Pow((this.transform.position.y - PS.transform.position.y), 2));
        distance = Mathf.Min(a, b);
        if (distance <= Range)
        {
            this.is_open = true;
        }
        else
        {
            this.is_open = false;
        }

    }
}
