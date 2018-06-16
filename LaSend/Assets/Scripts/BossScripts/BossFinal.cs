using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour {
    public GameObject BT;            //Boss控制器

    public GameObject Face;
    SpriteRenderer FaceSprite;
    public Sprite sprite;

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
                FaceSprite.sprite = sprite;
                gameObject.SetActive(false);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BT.GetComponent<BossTrigger>().Life -= 1;
        active = false;
    }
}
