using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

//https://blog.csdn.net/u010133610/article/details/55670800

public enum GearType
{
    typeA,//获取物，属性：bool是否为必需，碰撞为L+SPlayer，之后播放动画配合UI，关卡逻辑+1
    typeB,//伤害弹幕，属性：方向，速度，是否播放，碰撞为L+S
    typeC,//连环跳台，属性：是否激活，float激活时间，int移动方向，float移动位移，下一跳台target，碰撞为L+S，激活时间内接触后下一跳台激活
    typeD,//S开启的机关，属性：方向和位移大小，碰撞为S且status为attack，之后目标进行位移
}

public class Gear : MonoBehaviour
{
    public GearType type;

    public bool is_need = true;

    public float speed = 0;
    public int movedir = 0;

    public bool is_open = false;
    public int dir = 0;
    public float moveLong = 0;
    public GameObject Target;//目标
    public float stay_time = 0f;
    float countime;

    // Use this for initialization
    void Start()
    {
        countime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.type)
        {
            case GearType.typeA: break;
            case GearType.typeB: break;
            case GearType.typeC: break;
            case GearType.typeD: break;
        }

     

    }
    void OnCollision2DEnter(Collider2D obj)
    {
        switch (obj.gameObject.tag)
        {
            case "SPlayer"://之后细分
                {
                    //S的状态
                    if (this.type == GearType.typeA)//且S状态为Normal
                    {

                    }
                    if (this.type == GearType.typeB)//且S的状态为attack
                    {

                    }
                    if (this.type == GearType.typeC)//且S的状态为attack
                    {

                    }
                    if (this.type == GearType.typeC)//且S的状态为attack
                    {

                    }
                }
                break;
            case "LPlayer":
                {
                    //S的状态
                    if (this.type == GearType.typeA)//且S状态为Normal
                    {

                    }
                    if (this.type == GearType.typeB)//且S的状态为attack
                    {

                    }
                    if (this.type == GearType.typeC)//且S的状态为attack
                    {

                    }
                    if (this.type == GearType.typeD)//且S的状态为attack
                    {

                    }
                }
                break;//同上
            case "SPlayerAttack":
                {
                    if (this.type == GearType.typeA)//且S状态为Normal
                    {

                    }
                    if (this.type == GearType.typeB)//且S的状态为attack
                    {

                    }
                    if (this.type == GearType.typeC)//且S的状态为attack
                    {

                    }
                    if (this.type == GearType.typeD)//且S的状态为attack
                    {

                    }
                }
                break;
        }

    }
    void GearA()
    {

    }
    void GearB()
    {

    }
    void GearC()
    {

    }
    void GearD()
    {

    }

}
