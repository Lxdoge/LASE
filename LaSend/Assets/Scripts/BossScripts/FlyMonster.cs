using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonster : MonoBehaviour {
    bool FacingRight;
    public int PointNum;            //路径点总数
    public GameObject[] WayPoint;   //路径点数组
    int NextPoint;                  //当前目标路径点
    Rigidbody2D rBody;              //刚体
    public GameObject gameManager;  //
    GameManager Manager;            //
    bool OntheWay;
    public float stopRange;         //停止检测范围
    public float stopTime;          //停止时间
    float Dis;
    int Direction;
    public float Speed;             //移动速度

    public GameObject death;
    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody2D>();
        OntheWay = true;
        Direction = 1;
        FacingRight = false;
        Manager = gameManager.GetComponent<GameManager>();
        
    }
	
	// Update is called once per frame
	void Update () {
        Dis = Vector3.Distance(transform.position, WayPoint[NextPoint].transform.position);
        if (OntheWay)
        {
            if(Dis < stopRange)
            {
                OntheWay = false;
                NextPoint += Direction;
                if (NextPoint == PointNum - 1)
                    Direction = -1;
                else if (NextPoint == 0)
                    Direction = 1;
            }
        }
	}

    private void FixedUpdate()
    {
        if (Manager.gameover)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            death.SetActive(true);
            rBody.velocity = new Vector3(WayPoint[NextPoint].transform.position.x - transform.position.x, WayPoint[NextPoint].transform.position.y - transform.position.y).normalized * Speed;
        }
        if (!OntheWay)
        {
            rBody.velocity = new Vector3(WayPoint[NextPoint].transform.position.x - transform.position.x, WayPoint[NextPoint].transform.position.y - transform.position.y).normalized * Speed;
            if((WayPoint[NextPoint].transform.position.x - transform.position.x )> 0 && !FacingRight)
            {
                Flip();
            }
            else if((WayPoint[NextPoint].transform.position.x - transform.position.x)< 0 && FacingRight)
            {
                Flip();
            }
            OntheWay = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        death.SetActive(false);
        rBody.velocity = Vector3.zero;
    }
    

    //转向
    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 local = transform.localScale;
        local.x = -local.x;
        transform.localScale = local;
    }
}
