using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
public class FireGear : MonoBehaviour {
    
    public float lifetime;
    public float bullet_speed;
    public bool is_open = false;
    public GameObject Bullet_L;
    public GameObject Bullet_S;
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        this.GetComponent<Collider2D>().enabled = this.is_open ;

	}
    void OnCollisionEnter2D(Collider2D obj)
    {
        if(this.GetComponent<PosCtrl>().is_open)
        {
            string ta = obj.gameObject.tag;
            if(tag=="LPlayer"&&tag=="SPlayer")
            {
                fire(tag);
            }
        }
    }
    void fire(string player)
    {
        GameObject bul= Instantiate(Bullet_L, this.transform.position, this.transform.rotation);
        GameObject bus = Instantiate(Bullet_S, this.transform.position, this.transform.rotation);
    }
}
