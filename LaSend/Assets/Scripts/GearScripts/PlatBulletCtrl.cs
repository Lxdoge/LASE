using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatBulletCtrl : MonoBehaviour {
    Animator animator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D box;
    bool hit;
    public float BoomTime;
    float Timer;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Timer = BoomTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (hit)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                animator.SetBool("On", true);
                Timer = BoomTime;
            }
        }

        if (animator.GetBool("Off"))
        {
            animator.SetBool("Off", false);
            spriteRenderer.enabled = false;
            Timer = BoomTime;
            hit = false;
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hit)
            hit = true;
    }
}
