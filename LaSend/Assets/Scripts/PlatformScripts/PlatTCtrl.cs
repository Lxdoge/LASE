using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatTCtrl : MonoBehaviour {
    public GameObject Target;
    Transform TargetTrans;
    public float Speed;

	// Use this for initialization
	void Start () {
        TargetTrans = Target.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "LPlayer")
            collision.GetComponent<LPlayerCtrl>().disabled = true;
        if(collision.tag == "SPlayer")
            collision.GetComponent<SPlayerCtrl>().disabled = true;
        collision.GetComponent<Rigidbody2D>().velocity = new Vector3(TargetTrans.position.x - transform.position.x, TargetTrans.position.y - transform.position.y).normalized * Speed;
    }
}
