using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCtrl : MonoBehaviour {
    public float Range;
    public GameObject PL;
    public GameObject PS;
    bool is_open = true;
    public GameObject Gear;
    Transform gearTransform;
    Transform LT, ST;

    float distance;
    float Dl, Ds;
    // Use this for initialization
    void Start()
    {
        LT = PL.transform;
        ST = PS.transform;
        gearTransform = Gear.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Dl = Vector2.Distance(LT.position, gearTransform.position);
        Ds = Vector2.Distance(ST.position, gearTransform.position);
        distance = Dl > Ds ? Dl : Ds;
        if(distance > Range && is_open)
        {
            is_open = false;
            Gear.SetActive(false);
        }
        if(distance < Range && !is_open)
        {
            is_open = true;
            Gear.SetActive(true);
        }
    }
}
