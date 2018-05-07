﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CollectionCtrl : MonoBehaviour {
    //public Image targetstone;
    //public ParticleSystem collectUIfx;

    public GameObject gameManager;
    GameManager manager;
    SpriteRenderer spriteRenderer;
    CircleCollider2D Box;
    bool isCollected;
    public int NumofCollection;       //在关卡内的编号，外部定义
    public int Level_Num;             //关卡编号，外部定义
    // Use this for initialization
    void Start () {
        manager = gameManager.GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Box = GetComponent<CircleCollider2D>();
        switch (Level_Num)
        {
            case 1:
                isCollected = manager.pd.collection_1[NumofCollection]._isCollected;
                break;
            case 2:break;
            case 3:break;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (isCollected)//已收集状态
        {
            //关于UI的更新写在这里！

            //关闭显示等，变成空物体，避免重复检测
            spriteRenderer.enabled = false;
            Box.enabled = false;
            enabled = false;
        }
	}
    void OnTriggerEnter2D(Collider2D obj)
    {
        isCollected = true;
        manager.pd.GetCollection_1(NumofCollection);//记录但并不立刻写入存档
        /*
        string ta = obj.gameObject.tag;
        if(ta=="LPlayer"||ta=="SPlayer")
        {
            if(this.is_last)
            {
                //下一关
            }
            targetstone.DOFade(1.0f,fadetime);
            //公有变量收集+1
            Destroy(this.gameObject);
            collectUIfx.Play();
        }
        */
    }
}