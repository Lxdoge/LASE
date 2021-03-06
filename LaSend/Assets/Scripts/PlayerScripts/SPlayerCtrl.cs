﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SPlayerCtrl : MonoBehaviour {
    public Rigidbody2D rBody;                 //刚体
    Animator animator;
    /// <summary>
    /// /////////////////////////角色状态变量//////////////////////////////////
    /// </summary>
    [HideInInspector]
    public enum Status { down, right, up, left, death };//角色状态
    [HideInInspector]
    public Status status;              //角色当前状态
    [HideInInspector]
    public bool facingRight = false;   //角色朝向
    public GameObject lMark;           //转向标记
    PolygonCollider2D polygonCollider2D;
    [HideInInspector]
    public bool death;         //死亡
    [HideInInspector]
    public bool disabled;      //不可控制状态
    /// <summary>
    /// /////////////////////////水平移动变量//////////////////////////////////
    /// </summary>
    public float moveSpeed;            //角色对自身水平移动速度限制
    public float moveForce;            //角色自身水平移动力

    float hSpeed;                      //外界对角色水平移动速度限制
    [HideInInspector]
    public float platSpeed;            //平台属性提供的速度限制变量（例如传送带）
    [HideInInspector]
    public float environSpeed;         //环境属性提供的速度限制变量（例如风）
    /// <summary>
    /// //////////////////////////轴变量//////////////////////////////////////
    /// </summary>
    float hor;                         //水平输入
    float ver;                         //垂直输入
    public Vector2 Sgravity2D;         //重力
    /// <summary>
    /// /////////////////////////跳跃变量//////////////////////////////////
    /// </summary>
    public float jumpSpeed;            //跳跃初速度
    public float jumpForce;            //跳跃力
    public float jumpTime;             //跳跃判断时间
    public Transform groundCheck;      //落地检验物体的空间状态
    public LayerMask isGround;         //地面层

    public 
    /// <summary>
    /// ////////////////////
    /// </summary>
    bool jump;                         //跳跃状态确认
    public bool grounded;                     //落地检验
    bool jumpInitial;                  //跳跃初次启动确认
    float jumpTimer;                   //跳跃判断剩余时间
    /// <summary>
    ///  ////////////////////////能量变量/////////////////////////////////
    /// </summary>
    public Slider SSlider;             //能量条
    public float EnergyCost;
    public float EnergyRecall;         //能量恢复速度
    float senergy;//energy范围默认0到1

    private void Awake()
    {
        status = Status.down;
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        Sgravity2D = new Vector2(0, -29.43f);
        platSpeed = 0.0f;
        environSpeed = 0.0f;
        senergy = 1;
        disabled = false;
    }
    // 初始化
    void Start()
    {
        
    }

    // 每帧检测并更新状态
    void Update () {
        if(status == Status.death)
        {
            DeathCtrl();
            return;
        }
        if (animator.GetBool("Attack"))
            return;

        hor = Input.GetAxis("SHorizontal");
        ver = Input.GetAxis("SVertical");
        switch (status)
        {
            case Status.down:
                animator.SetFloat("SpeedY", rBody.velocity.y);
                animator.SetFloat("Hor", Mathf.Abs(hor));
                if (hor > 0 && !facingRight)
                    Flip();
                else if (hor < 0 && facingRight)
                    Flip();
                break;
            case Status.up:
                animator.SetFloat("SpeedY", -rBody.velocity.y);
                animator.SetFloat("Hor", Mathf.Abs(hor));
                if (hor < 0 && !facingRight)
                    Flip();
                else if (hor > 0 && facingRight)
                    Flip();
                break;
            case Status.right:
                animator.SetFloat("SpeedY", -rBody.velocity.x);
                animator.SetFloat("Hor", Mathf.Abs(ver));
                if (ver < 0 && !facingRight)
                    Flip();
                else if (ver > 0 && facingRight)
                    Flip();
                break;
            case Status.left:
                animator.SetFloat("SpeedY", rBody.velocity.x);
                animator.SetFloat("Hor", Mathf.Abs(ver));
                if (ver > 0 && !facingRight)
                    Flip();
                else if (ver < 0 && facingRight)
                    Flip();
                break;
        }
        
        //根据移动方向翻转角色朝向ccc

        SSlider.value = senergy;
        ResumeEnergy();
        

        AttackCheck();
       
        JumpCheck();
        GChangeCheck();
        DeathCheck();
        GroundCheck();
        if (grounded)
            disabled = false;
    }

    //
    private void FixedUpdate()
    {
        rBody.AddForce(Sgravity2D);
        if (disabled)
            return;
        switch (status)
        {
            case Status.down:
                FixupGDown();
                break;
            case Status.up:
                FixupGUp();
                break;
            case Status.right:
                FixupGRight();
                break;
            case Status.left:
                FixupGLeft();
                break;
        }
    }

    //检测并转换重力状态
    void GChangeCheck()
    {
        if (Input.GetButtonDown("SGravity"))
        {
            if (senergy < EnergyCost)
                return;
            ConsumeEnergy();
            switch (lMark.GetComponent<Lmark>().status)
            {
                case Lmark.Status.down:
                    Sgravity2D = new Vector2(0, -29.43f);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    status = Status.down;
                    break;
                case Lmark.Status.right:
                    Sgravity2D = new Vector2(29.43f, 0);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    status = Status.right;
                    break;
                case Lmark.Status.left:
                    Sgravity2D = new Vector2(-29.43f, 0);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                    status = Status.left;
                    break;
                case Lmark.Status.up:
                    Sgravity2D = new Vector2(0, 29.43f);
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    status = Status.up;
                    break;
            }
        }
    }
    // 检测并初始化跳跃
    void JumpCheck()
    {
        if (grounded && (Input.GetButtonDown("SJump")))
        {
            if (animator.GetBool("Attack"))
                return;
            //确认跳跃状态
            jump = true;
            //确认跳跃动作（Jump）是初次启动
            jumpInitial = true;
            //初始化跳跃判断计时器
            jumpTimer = jumpTime;
        }//down影响手感有待验证
    }

    // 检测并播放死亡动画
    void DeathCheck()
    {
        if (death)
        {
            death = false;
            animator.SetBool("Death", true);

            status = Status.death;
            rBody.velocity = Vector3.zero;
            Sgravity2D = Vector2.zero;
            polygonCollider2D.enabled = false;
        }
    }
    //Down和Up状态的移动
    public void MoveGHor(float direction)
    {
        //更新外界对角色水平移动速度限制
        hSpeed = platSpeed + environSpeed;
        //如果当前速度没有达到角色当前位置输入方向上的速度限制，加速
        if (direction * rBody.velocity.x < moveSpeed + direction * hSpeed)
            rBody.AddForce(Vector2.right * direction * moveForce);
        //否则，限制移动速度
        if (direction * rBody.velocity.x > moveSpeed + direction * hSpeed || direction == 0)//direction == 0影响惯性
            rBody.velocity = new Vector2(direction * moveSpeed + hSpeed, rBody.velocity.y);
    }
    void JumpGHor(int direction)
    {
        if (jump)
        {
            //如果跳跃刚刚启动，赋予角色初速度
            if (jumpInitial)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed * direction);
                animator.SetBool("Jump", true);
                jumpInitial = false;
            }
            //启动后在跳跃判定时间内，按住跳跃键会持续施加刚体力
            else if (jumpTimer > 0 && (Input.GetButton("SJump")))
            {
                jumpTimer -= Time.fixedDeltaTime;
                rBody.AddForce(Vector2.up * jumpForce * direction);
            }
            //判定结束后，结束跳跃操作
            else
            {
                jump = false;
            }
        }
        
    }
    void FixupGDown()
    {
        if (animator.GetBool("Attack"))
        {
            MoveGHor(0);
            return;
        }
        //水平移动（direction==0无限制时，以下写法影响惯性）
        if (Mathf.Abs(hor) >= 0.5f)
        {
            MoveGHor(hor);
        }
        else
            MoveGHor(0);
        //跳跃
        if (jump)
            JumpGHor(1);
    }
    void FixupGUp()
    {
        if (animator.GetBool("Attack"))
        {
            MoveGHor(0);
            return;
        }
        //水平移动（direction==0无限制时，以下写法影响惯性）
        if (Mathf.Abs(hor) >= 0.5f)
        {
            MoveGHor(hor);
        }
        else
            MoveGHor(0);
        //跳跃
        if (jump)
            JumpGHor(-1);
    }

    //Right和Left状态的移动
    void MoveGVer(float direction)
    {
        //更新外界对角色竖直移动速度限制
        hSpeed = platSpeed + environSpeed;
        //如果当前速度没有达到角色当前位置输入方向上的速度限制，加速
        if (direction * rBody.velocity.y < moveSpeed + direction * hSpeed)
            rBody.AddForce(Vector2.up * direction * moveForce);
        //否则，限制移动速度
        if (direction * rBody.velocity.y > moveSpeed + direction * hSpeed || direction == 0)//direction == 0影响惯性
            rBody.velocity = new Vector2(rBody.velocity.x, direction * moveSpeed + hSpeed);
        //Debug.Log(hSpeed);
    }
    void JumpGVer(int direction)
    {
        if (jump)
        {
            //如果跳跃刚刚启动，赋予角色初速度
            if (jumpInitial)
            {
                rBody.velocity = new Vector2(jumpSpeed * direction, rBody.velocity.y);
                animator.SetBool("Jump", true);
                jumpInitial = false;
            }
            //启动后在跳跃判定时间内，按住跳跃键会持续施加刚体力
            else if (jumpTimer > 0 && (Input.GetButton("SJump")))
            {
                jumpTimer -= Time.fixedDeltaTime;
                rBody.AddForce(Vector2.right * jumpForce * direction);
            }
            //判定结束后，结束跳跃操作
            else
            {
                jump = false;
            }
        }
    }
    void FixupGRight()
    {
        if (animator.GetBool("Attack"))
        {
            MoveGVer(0);
            return;
        }
        //水平移动（direction==0无限制时，以下写法影响惯性）
        if (Mathf.Abs(ver) >= 0.5)
        {
            MoveGVer(-ver);
        }
        else
            MoveGVer(0);
        //跳跃
        if (jump)
            JumpGVer(-1);
    }
    void FixupGLeft()
    {
        if (animator.GetBool("Attack"))
        {
            MoveGVer(0);
            return;
        }
        //水平移动（direction==0无限制时，以下写法影响惯性）
        if(Mathf.Abs(ver) >= 0.5)
        {
            MoveGVer(-ver);
        }
        else
            MoveGVer(0);
        //跳跃
        if (jump)
            JumpGVer(1);
    }

    // 落地检验
    void GroundCheck()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, isGround);
        animator.SetBool("Ground", grounded);
    }
    //转向
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 local = transform.localScale;
        local.x = -local.x;
        transform.localScale = local;
    }
    //攻击
    void AttackCheck()
    {
        if (Input.GetButtonDown("SAttack") && grounded)
        {
            if (senergy < EnergyCost)
                return;
            ConsumeEnergy();
            animator.SetBool("Attack", true);
        }
    }

    //能量控制函数
    void ConsumeEnergy()//消耗
    {
        senergy -= EnergyCost;
    }
    void ResumeEnergy()//恢复
    {
        if (senergy < 1)
            senergy += EnergyRecall * Time.deltaTime;
        if (senergy > 1)
            senergy = 1;
    }

    void DeathCtrl()
    {
        if (!animator.GetBool("Death"))
        {
            Sgravity2D = new Vector2(0, -29.43f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            status = Status.down;
            polygonCollider2D.enabled = true;
        }
    }

}
