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
        if (targetpos.y - transform.position.y >= 0)
            transform.Rotate(new Vector3(0, 0, Vector3.Angle(new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y), Vector3.right)));
        else
            transform.Rotate(new Vector3(0, 0, -Vector3.Angle(new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y), Vector3.right)));
        distance = Vector3.Distance(targetpos, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        transform.DOMove(targetpos, distance/speed);
        life -= Time.deltaTime;
        if(life < 0)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter2D(Collider2D obj)
    {
        Destroy(gameObject);
    }
}
