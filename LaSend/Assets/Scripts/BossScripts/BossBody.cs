using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour {

    public GameObject Gear1;          //机关
    public GameObject Gear2;
    public GameObject BT;            //Boss控制器

    public GameObject Face;
    SpriteRenderer FaceSprite;

    public GameObject LeftEye;
    public GameObject RightEye;

    int TwinkleTime;
    bool active;
	// Use this for initialization
	void Start () {
        active = true;
        TwinkleTime = 5;
        FaceSprite = Face.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!active)
        {
            TwinkleTime -= 1;
            FaceSprite.color = new Color(FaceSprite.color.r, FaceSprite.color.g, FaceSprite.color.b, 1 - FaceSprite.color.a);
            if (TwinkleTime == 1)
            {
                active = true;
                TwinkleTime = 5;
                gameObject.SetActive(false);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BT.GetComponent<BossTrigger>().Life -= 1;
        Gear1.transform.position = LeftEye.transform.position;
        Gear2.transform.position = RightEye.transform.position;
        active = false;
    }
}
