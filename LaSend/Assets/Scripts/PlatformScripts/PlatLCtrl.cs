using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatLCtrl : MonoBehaviour {
    [HideInInspector]
    public enum Status { on, off, stop, re };//平台状态
    public Status status;
    [HideInInspector]
    public bool Mov;                       //平台移动开关
    public bool MovS;
    public float moveSpeed;                //平台移动速度
    public float removeSpeed;              //平台返回速度
    public float stopRange;                //平台停止检测范围
    public float stopTime;                 //平台停止时间
    private float stopTimer;               //停止计时器
    public Transform[] tPoints;            //平台路径点

    Rigidbody2D rBody;                     //平台刚体
    float dis;                             //与当前目标的距离
	// Use this for initialization
	void Start () {
        Mov = false;
        MovS = false;
        rBody = GetComponent<Rigidbody2D>();
        status = Status.off;
        stopTimer = stopTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Mov || MovS)
        {
            if(status == Status.off)
                status = Status.on;
            if (Mov)
                MoveLCtrl();
            else if (MovS)
                MoveSCtrl();
        }
    }
    void MoveLCtrl()
    {
        switch (status)
        {
            case Status.off:
                break;
            case Status.on:
                //检测是否到达目标点
                dis = Vector2.Distance(transform.position, tPoints[1].position);
                if (dis < stopRange)//如果是，更新状态
                {
                    status = Status.stop;
                    rBody.velocity = new Vector2(0, 0);
                    transform.position = tPoints[1].position;
                }
                break;
            case Status.stop:
                if (stopTimer >= 0)
                {
                    stopTimer -= Time.deltaTime;
                }
                else
                {
                    stopTimer = stopTime;
                    status = Status.re;
                }
                break;
            case Status.re:
                dis = Vector2.Distance(transform.position, tPoints[0].position);
                if (dis < stopRange)//如果是，更新状态
                {
                    status = Status.off;
                    rBody.velocity = new Vector2(0, 0);
                    transform.position = tPoints[0].position;
                    Mov = false;
                }
                break;
        }
    }
    void MoveSCtrl()
    {
        switch (status)
        {
            case Status.off:
                break;
            case Status.on:
                //检测是否到达目标点
                dis = Vector2.Distance(transform.position, tPoints[2].position);
                if (dis < stopRange)//如果是，更新状态
                {
                    status = Status.stop;
                    rBody.velocity = new Vector2(0, 0);
                    transform.position = tPoints[2].position;
                }
                break;
            case Status.stop:
                if (stopTimer >= 0)
                {
                    stopTimer -= Time.deltaTime;
                }
                else
                {
                    stopTimer = stopTime;
                    status = Status.re;
                }
                break;
            case Status.re:
                dis = Vector2.Distance(transform.position, tPoints[0].position);
                if (dis < stopRange)//如果是，更新状态
                {
                    status = Status.off;
                    rBody.velocity = new Vector2(0, 0);
                    transform.position = tPoints[0].position;
                    MovS = false;
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (status)
        {
            case Status.off:
                break;
            case Status.on:
                if(Mov)
                    rBody.velocity = new Vector2(tPoints[1].position.x - transform.position.x, tPoints[1].position.y - transform.position.y).normalized * moveSpeed;
                else if(MovS)
                    rBody.velocity = new Vector2(tPoints[2].position.x - transform.position.x, tPoints[2].position.y - transform.position.y).normalized * moveSpeed;
                break;
            case Status.stop:
                
                break;
            case Status.re:
                rBody.velocity = new Vector2(tPoints[0].position.x - transform.position.x, tPoints[0].position.y - transform.position.y).normalized * removeSpeed;
                break;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        SPlayerCtrl sc = collision.gameObject.GetComponent<SPlayerCtrl>();
        //修正平台上的玩家角色的移动速度(因为平台移动方向会变，不能在enter时更新）
        if (collision.transform.tag == "SPlayer")
        {
            if(sc.status == SPlayerCtrl.Status.down || sc.status == SPlayerCtrl.Status.up)
            {
                sc.platSpeed = rBody.velocity.x;
                if (!sc.jump)
                    sc.rBody.velocity = new Vector2(sc.rBody.velocity.x, rBody.velocity.y);
            }
            if (sc.status == SPlayerCtrl.Status.left || sc.status == SPlayerCtrl.Status.right)
            {
                sc.platSpeed = rBody.velocity.y;
                if (!sc.jump)
                    sc.rBody.velocity = new Vector2(rBody.velocity.x,sc.rBody.velocity.y);
            }
                
            //collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, rBody.velocity.y);
        }
        LPlayerCtrl lc = collision.gameObject.GetComponent<LPlayerCtrl>();
        if (collision.transform.tag == "LPlayer")
        {
            if(lc.status == LPlayerCtrl.Status.normal)
            {
                lc.platSpeed = rBody.velocity.x;
                if(!lc.jump)
                    lc.rBody.velocity = new Vector2(lc.rBody.velocity.x, rBody.velocity.y);
            }
            //collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, rBody.velocity.y);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //修正离开平台的玩家角色的移动速度限制
        if (collision.transform.tag == "SPlayer")
        {
            collision.gameObject.GetComponent<SPlayerCtrl>().platSpeed = 0;
        }
        if (collision.transform.tag == "LPlayer")
        {
            collision.gameObject.GetComponent<LPlayerCtrl>().platSpeed = 0;
        }
    }
}
