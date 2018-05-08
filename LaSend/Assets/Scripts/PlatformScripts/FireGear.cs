using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
public class FireGear : MonoBehaviour
{


    [HideInInspector]
    public bool lopen = false;
    [HideInInspector]
    public bool sopen = false;
    public GameObject Bullet_L;
    public GameObject Bullet_S;
    public GameObject Gun;
    //public float Cycle_Time;//越短发射越多
    public float Timerl;
    public float Timers;
    public float FireCd = 1f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lopen)
        {
            Timerl -= Time.deltaTime;
            if (Timerl <= 0)
            {
                FireL();
                Timerl = FireCd;
            }
        }
        if (sopen)
        {
            Timers -= Time.deltaTime;
            if (Timers <= 0)
            {
                FireS();
                Timers = FireCd;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        
    }
    void OnTriggerStay2D(Collider2D obj)
    {
        string thetag = obj.gameObject.tag;
        if (thetag == "LPlayer")
        {
            lopen = true;
        }
        if (thetag == "SPlayer")
        {
            sopen = true;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        string thetag = obj.gameObject.tag;
        if (thetag == "LPlayer")
        {
            lopen = false;
        }
        if (thetag == "SPlayer")
        {
            sopen = false;
        }
    }
    void FireL()
    {
        Instantiate(Bullet_L, Gun.transform.position, Gun.transform.rotation);
    }
    void FireS()
    {
        Instantiate(Bullet_S, Gun.transform.position, Gun.transform.rotation);
    }
}
