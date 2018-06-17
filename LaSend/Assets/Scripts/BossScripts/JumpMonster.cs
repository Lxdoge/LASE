using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMonster : MonoBehaviour {
    Rigidbody2D rBody;
    public Transform groundCheck;
    public LayerMask isGround;         //地面层
    public float JumpSpeed;
    bool grounded;
	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
        grounded = false;
	}
	
	// Update is called once per frame
	void Update () {
        GroundCheck();
        if (grounded)
            rBody.velocity = new Vector2(0, JumpSpeed);
    }
    void GroundCheck()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, isGround);
    }
}
