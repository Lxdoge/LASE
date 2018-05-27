using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGearCtrl : MonoBehaviour {
    
    public GameObject Bullet;
    public float Timer;
    public float FireCD;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            Timer = FireCD;
            Fire();
        }
	}

    void Fire()
    {
        Instantiate(Bullet, transform.position, transform.rotation);
    }
}
