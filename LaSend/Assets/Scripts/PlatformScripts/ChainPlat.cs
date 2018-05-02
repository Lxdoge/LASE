using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ChainPlat : MonoBehaviour {
    public GameObject target;
    public float stay_time=0f;
    float countime;
    public float speedtime=0.5f;
    [Tooltip("如果是多平台中的第一关个就为true")]
    public bool is_open=false;
    public Vector2 MoveWay;
    Vector2 initpos;
   // public Slider Collection;
	// Use this for initialization
	void Start () {
        countime = 0f;
        initpos= this.transform.position;
	}
	// Update is called once per frame
	void Update () {
        if(stay_time!=0f)
        {
            if (is_open )
            {
                if(MoveWay.x!=0)
                {
                    this.transform.DOMoveX(MoveWay.x+initpos.x, speedtime);
                }
               if(MoveWay.y!=0)
                {
                    this.transform.DOLocalMoveY(MoveWay.y+initpos.y, speedtime);
                }
                //播音效和UI特效
                countime = 0f;
            }

            if (!is_open)
            {                
                countime += Time.deltaTime;
            }
            if(countime>=stay_time)
            {
                this.transform.DOMove(initpos, speedtime);
            }
        }    
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        switch (obj.gameObject.tag)
        {
            case "SPlayer"://之后细分
                {
                   if (target.GetComponent<ChainPlat>())
                    {
                        target.GetComponent<ChainPlat>().is_open = true;
                        
                    }
                }
                break;
            case "LPlayer":
                {
                    if (target.GetComponent<ChainPlat>())
                    {
                        target.GetComponent<ChainPlat>().is_open = true;
                        Debug.Log("!");
                    }
                }
                break;//同上
            default:
                break;
        }

    }
    void OnCollisionExit2D(Collision2D obj)
    {
        switch (obj.gameObject.tag)
        {
            case "SPlayer"://之后细分
                {
                    if (target.GetComponent<ChainPlat>())
                    {
                        target.GetComponent<ChainPlat>().is_open = false;
                    }
                }
                break;
            case "LPlayer":
                {
                    if (target.GetComponent<ChainPlat>())
                    {
                        target.GetComponent<ChainPlat>().is_open = false;
                    }
                }
                break;//同上
            default:
                break;
        }

    }
}
