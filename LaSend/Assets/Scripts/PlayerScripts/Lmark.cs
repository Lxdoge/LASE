using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lmark : MonoBehaviour {
    [HideInInspector]
    public enum Status { down, right, left, up };//角色状态
    [HideInInspector]
    public Status status;              //角色当前状态
    public GameObject lPlayer;
    public GameObject sPlayer;
    public GameObject Gmark;
    SpriteRenderer spriteR;
    public Sprite[] spriteMark = new Sprite[4];
    float disX, disY;
    void Start () {
        status = Status.right;
        spriteR = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Gmark.transform.position;
        disY = lPlayer.transform.position.y - sPlayer.transform.position.y;
        disX = lPlayer.transform.position.x - sPlayer.transform.position.x;
        if (disY - disX > 0)
        {
            if (disX + disY >= 0)
            {
                status = Status.down;
                spriteR.sprite = spriteMark[0];
            }
            else
            {
                status = Status.right;
                spriteR.sprite = spriteMark[1];
            }
                
        }
        else
        {
            if (disX + disY > 0)
            {
                status = Status.left;
                spriteR.sprite = spriteMark[2];
            }
            else
            {
                status = Status.up;
                spriteR.sprite = spriteMark[3];
            }
        }
        
	}
}
