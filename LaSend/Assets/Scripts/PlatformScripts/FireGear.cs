using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
public class FireGear : MonoBehaviour
{
    bool is_firstopen = false;
    [HideInInspector]
    public bool lopen = false;
    [HideInInspector]
    public bool sopen = false;
    public GameObject Bullet_L;
    public GameObject Bullet_S;
    //public float Cycle_Time;//越短发射越多
    private float Timerl;
    private float Timers;
    public float FireCd = 1f;
    // Use this for initialization
    void Start()
    {
        Timerl = 0;
        Timers = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lopen && is_firstopen)
        {
            Timerl -= Time.deltaTime;
            if (Timerl <= 0)
            {
                fireL();
                Timerl = FireCd;
            }
        }
        if(sopen && is_firstopen)
        {
            Timers -= Time.deltaTime;
            if (Timers <= 0)
            {
                fireS();
                Timers = FireCd;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        string ta = obj.gameObject.tag;
        if (ta == "LPlayer" || ta == "SPlayer")
        {
            is_firstopen = true;
        }
        Debug.Log(1);
    }
    void OnTriggerStay2D(Collider2D obj)
    {
        string thetag = obj.gameObject.tag;
        if (thetag == "LPlayer")
        {
            Debug.Log("LL");

            this.lopen = true;
        }
        if (thetag == "SPlayer")
        {
            Debug.Log("SS");
            this.sopen = true;
        }
    }
    void OnTriggerExit2D(Collider2D obj)
    {
        string thetag = obj.gameObject.tag;
        if (thetag == "LPlayer")
        {
            Debug.Log("LF");
            this.lopen = false;
        }
        if (thetag == "SPlayer")
        {
            Debug.Log("SF");
            this.sopen = false;
        }
    }
    void fireL()
    {
        GameObject bl = Instantiate(Bullet_L, this.transform.position, this.transform.rotation);
        Debug.Log("bulletl");
    }
    void fireS()
    {
        GameObject bs = Instantiate(Bullet_S, this.transform.position, this.transform.rotation);
        Debug.Log("bullets");
    }
}
