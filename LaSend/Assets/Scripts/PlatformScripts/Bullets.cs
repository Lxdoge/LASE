using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullets : MonoBehaviour {

    public float life=3;
    public float speed;
    public bool toL;
    [HideInInspector]
    public Vector2 targetpos;
    // Use this for initialization
    float distance;
    void Start () {
        if(toL)
        {
            targetpos = GameObject.Find("LPlayer").transform.position;
        }
        if (!toL)
        {
            targetpos = GameObject.Find("SPlayer").transform.position;
        }
        Destroy(this.gameObject, life);
        distance = Mathf.Sqrt(Mathf.Pow(this.transform.position.x-targetpos.x, 2) + Mathf.Pow(this.transform.position.y -targetpos.y, 2));
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.DOMove(targetpos, distance/speed);
	}
    void OnTriggerEnter2D(Collider2D obj)
    {
        Destroy(this.gameObject);
    }
}
