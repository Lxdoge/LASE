using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullets : MonoBehaviour {

    public float life;
    public float speed;
    public bool toL;
    [HideInInspector]
    public Vector3 targetpos;
    Animator animator;
    Rigidbody2D rBody;
    // Use this for initialization
    float distance;
    void Start () {
        animator = GetComponent<Animator>();
        if(toL)
        {
            targetpos = GameObject.Find("LPlayer").transform.position;
        }
        if (!toL)
        {
            targetpos = GameObject.Find("SPlayer").transform.position;
        }
        if (targetpos.y - transform.position.y >= 0)
            transform.Rotate(new Vector3(0, 0, Vector3.Angle(new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y), Vector3.right)));
        else
            transform.Rotate(new Vector3(0, 0, -Vector3.Angle(new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y), Vector3.right)));
        rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y).normalized * speed;
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
        if(life <= 0)
        {
            animator.SetBool("Boom", true);
            GetComponent<Collider2D>().enabled = false;
            rBody.velocity = Vector3.zero;
            if (animator.GetBool("Death"))
            {
                Destroy(gameObject);
            }
        }
	}
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "LPlayer")
        {
            obj.GetComponent<LPlayerCtrl>().death = true;
        }
        else if (obj.tag == "SPlayer")
        {
            obj.GetComponent<SPlayerCtrl>().death = true;
        }
        life = 0;
        animator.SetBool("Boom", true);
        GetComponent<Collider2D>().enabled = false;
        rBody.velocity = Vector3.zero;
    }
}
