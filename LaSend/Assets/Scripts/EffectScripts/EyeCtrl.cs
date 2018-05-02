using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCtrl : MonoBehaviour {
    public GameObject LookPoint;
    Rigidbody2D rbody;
    Vector2 direction;
    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        direction = new Vector2(LookPoint.transform.position.x - transform.position.x, LookPoint.transform.position.y - transform.position.y);
        rbody.velocity = direction.normalized;
	}
}
