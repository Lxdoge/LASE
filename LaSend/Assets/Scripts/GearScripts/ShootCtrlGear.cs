using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCtrlGear : MonoBehaviour {
    public GameObject PlatBullet;
    public GameObject Target;
    Rigidbody2D PlatBody;
    Transform TargetTrans;
    Transform PlatTrans;
    SpriteRenderer PlatSprite;
    BoxCollider2D PlatBox;
    public float Speed;
    public float StartTime;
    public float BackTime;
    float NowTime;

	// Use this for initialization
	void Start () {
        PlatBody = PlatBullet.GetComponent<Rigidbody2D>();
        TargetTrans = Target.GetComponent<Transform>();
        PlatTrans = PlatBullet.GetComponent<Transform>();
        PlatSprite = PlatBullet.GetComponent<SpriteRenderer>();
        PlatBox = PlatBullet.GetComponent<BoxCollider2D>();
        NowTime = StartTime;

	}
	
	// Update is called once per frame
	void Update () {
        NowTime -= Time.deltaTime;
        if(NowTime <= 0)
        {
            PlatTrans.position = transform.position;
            if (!PlatSprite.enabled)
            {
                PlatSprite.enabled = true;
                PlatBox.enabled = true;
            }
            NowTime = BackTime;
        }
		PlatBody.velocity = new Vector2(TargetTrans.position.x - transform.position.x, TargetTrans.position.y - transform.position.y).normalized * Speed;
    }
}
