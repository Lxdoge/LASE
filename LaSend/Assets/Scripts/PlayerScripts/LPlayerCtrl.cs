using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LPlayerCtrl : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rBody;                 //刚体
    Animator animator;                 //动画
    GhostSprites ghost;
    PolygonCollider2D polygonCollider2D;
    CircleCollider2D circleCollider2D;
    /// <summary>
    /// /////////////////////////角色状态变量//////////////////////////////////
    /// </summary>
    [HideInInspector]
    public enum Status { normal, light, death };//角色状态
    [HideInInspector]
    public Status status;              //角色当前状态
    [HideInInspector]
    public bool facingRight;   //角色朝向
    [HideInInspector]
    public bool death;         //死亡
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

    public float lightMoveSpeed;       //发光状态移动速度限制
    public float lightMoveForce;       //发光状态移动力
    /// <summary>
    /// //////////////////////////轴变量//////////////////////////////////////
    /// </summary>
    float hor;                         //水平输入
    float ver;                         //垂直输入
    /// <summary>
    /// /////////////////////////跳跃变量//////////////////////////////////
    /// </summary>
    public float jumpSpeed;            //跳跃初速度
    public float jumpForce;            //跳跃力
    public float jumpTime;             //跳跃判断时间
    public Transform groundCheck;      //落地检验物体的空间状态
    public LayerMask isGround;         //地面层

    public bool jump;                         //跳跃状态确认
    public bool grounded;                     //落地检验
    bool jumpInitial;                  //跳跃初次启动确认
    float jumpTimer;                   //跳跃判断剩余时间
    /// <summary>
    /// /////////////////////////能量变量/////////////////////////////////
    /// </summary>
    public Slider LSlider;             //能量条
    public float EnergySpeed;          //能量消耗速度（1/t）
    public float EnergyRecall;         //能量恢复速度
    float lenergy;//energy范围默认0到1
    // 初始化
    void Start()
    {
        facingRight = false;
        death = false;
        status = Status.normal;
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        ghost = GetComponentInChildren<GhostSprites>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        platSpeed = 0.0f;
        environSpeed = 0.0f;
        lenergy = 1;
    }

    // 每帧检测并更新状态
    void Update () {
        switch (status)
        {
            case Status.normal:
                NormalCtrl();
                break;
            case Status.light:
                LightCtrl();
                break;
            case Status.death:
                DeathCtrl();
                break;

        }
        
	}

    // 固定间隔根据角色状态更新物理系统
    private void FixedUpdate()
    {
        switch (status)
        {
            case Status.normal:
                NormalFixedCtrl();
                break;
            case Status.light:
                LightFixedCtrl();
                break;
        }
    }

    // 角色水平移动，单独写为了方便做过场（如果有的话）
    public void Move(float direction)
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
    //转向
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 local = transform.localScale;
        local.x = -local.x;
        transform.localScale = local;
    }

    // 检测并初始化跳跃
    void JumpCheck()
    {
        if (Input.GetButtonDown("LJump"))
        if (grounded && Input.GetButtonDown("LJump"))
        {
            //确认跳跃状态
            jump = true;
            //确认跳跃动作（Jump）是初次启动
            jumpInitial = true;
            //初始化跳跃判断计时器
            jumpTimer = jumpTime;
        }//down影响手感有待验证
    }

    // 跳跃
    void Jump()
    {
        if (jump)
        {
            //如果跳跃刚刚启动，赋予角色初速度
            if (jumpInitial)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y + jumpSpeed);
                animator.SetBool("Jump", true);
                jumpInitial = false;
            }
            //启动后在跳跃判定时间内，按住跳跃键会持续施加刚体力
            else if (jumpTimer > 0 && Input.GetButton("LJump"))
            {
                jumpTimer -= Time.fixedDeltaTime;
                rBody.AddForce(Vector2.up * jumpForce);
            }
            //判定结束后，结束跳跃操作
            else
            {
                jump = false;
            }
        }
        
    }

    // 落地检验
    void GroundCheck()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, isGround);
        animator.SetBool("Ground", grounded);
    }

    // 检测并初始化发光状态
    void LightCheck()
    {
        if (Input.GetButtonDown("Light"))
        {
            if (lenergy < 0.2)
                return;
            //切换状态
            status = Status.light;
            //去除重力
            rBody.gravityScale = 0;
            ghost.enabled = true;
            polygonCollider2D.enabled = false;
            circleCollider2D.enabled = true;
            animator.SetBool("Skill", true);
        }
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
            rBody.gravityScale = 0.0f;
            polygonCollider2D.enabled = false;
        }
    }

    //Normal状态
    void NormalCtrl()
    {
        hor = Input.GetAxis("LHorizontal");
        //根据移动方向翻转角色朝向
        if (hor > 0 && !facingRight)
            Flip();
        else if (hor < 0 && facingRight)
            Flip();
        if (grounded && animator.GetBool("Jump"))
        {
            animator.SetBool("Jump", false);
        }
        animator.SetFloat("SpeedY", rBody.velocity.y);
        ResumeEnergy();
        JumpCheck();
        LightCheck();
        DeathCheck();
        animator.SetFloat("Hor", Mathf.Abs(hor));
        LSlider.value = lenergy;
        GroundCheck();
    }
    void NormalFixedCtrl()
    {
        //水平移动（direction==0无限制时，以下写法影响惯性）
        if (Mathf.Abs(hor) >= 0.5)
        {
            Move(hor);
        }
        Move(hor);
        //跳跃
        if (jump)
            Jump();
    }

    //结束发光状态
    void LightEnd()
    {
        if (Input.GetButtonDown("Light") || lenergy <= 0 )//主动取消或能量耗尽
        {
            if (animator.GetBool("SkillON"))
                return;
            status = Status.normal;
            ghost.enabled = false;
            animator.SetBool("Skill", false);
            polygonCollider2D.enabled = true;
            circleCollider2D.enabled = false;
            rBody.gravityScale = 3;
        }
    }

    //发光状态移动
    void LightMove(float directionX, float directionY)
    {
        //如果当前速度没有达到角色当前位置水平输入方向上的速度限制，加速
        if (directionX * rBody.velocity.x < lightMoveSpeed)
            rBody.AddForce(Vector2.right * directionX * lightMoveForce);
        //否则，限制移动速度
        if (directionX * rBody.velocity.x > lightMoveSpeed || directionX == 0)
            rBody.velocity = new Vector2(directionX * lightMoveSpeed, rBody.velocity.y);
        //如果当前速度没有达到角色当前位置竖直输入方向上的速度限制，加速
        if (directionY * rBody.velocity.y < lightMoveSpeed)
            rBody.AddForce(Vector2.up * directionY * lightMoveForce);
        //否则，限制移动速度
        if (directionY * rBody.velocity.y > lightMoveSpeed || directionY == 0)
            rBody.velocity = new Vector2(rBody.velocity.x, directionY * lightMoveSpeed);

        if (animator.GetBool("SkillON"))
            rBody.velocity = new Vector2(0, 0);
    }

    //Light状态
    void LightCtrl()
    {
        hor = Input.GetAxis("LHorizontal");
        ver = Input.GetAxis("LVertical");
        if (hor > 0 && !facingRight)
            Flip();
        else if (hor < 0 && facingRight)
            Flip();
        ConsumeEnergy();
        LightEnd();
        DeathCheck();
        LSlider.value = lenergy;
    }
    void LightFixedCtrl()
    {
        float Hor, Ver;
        if (Mathf.Abs(hor) >= 0.5f)
            Hor = hor;
        else Hor = 0;
        if (Mathf.Abs(ver) >= 0.5f)
            Ver = ver;
        else Ver = 0;
        LightMove(Hor, Ver);
    }

    //能量控制函数
    void ConsumeEnergy()//消耗
    {
        lenergy -= EnergySpeed * Time.deltaTime;
    }
    void ResumeEnergy()//恢复
    {
        if (lenergy < 1)
            lenergy += EnergyRecall * Time.deltaTime;
        if (lenergy > 1)
            lenergy = 1;
    }

    //
    void DeathCtrl()
    {
        if (!animator.GetBool("Death"))
        {
            status = Status.normal;
            rBody.gravityScale = 3.0f;
            ghost.enabled = false;
            animator.SetBool("Skill", false);
            polygonCollider2D.enabled = true;
            circleCollider2D.enabled = false;
        }
    }
}
