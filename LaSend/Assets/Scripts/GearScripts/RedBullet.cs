using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour {

    public float Life;             //最大生命时长
    public float Speed;            //速度
    public float AddSpeed;         //加速度

    public float Scale;
    public float AddScale;

    public GameObject Target;      //目标
    
    private Rigidbody2D rBody;     //刚体

    [HideInInspector]
    public LPlayerCtrl lpCtrl;
    [HideInInspector]
    public SPlayerCtrl spCtrl;

    //自初始化
    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        Scale = transform.localScale.x;
    }

    // Use this for initialization
    void Start()
    {
        lpCtrl = GameObject.Find("LPlayer").GetComponent<LPlayerCtrl>();
        spCtrl = GameObject.Find("SPlayer").GetComponent<SPlayerCtrl>();

        //设定速度
        rBody.velocity = new Vector3(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y).normalized * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Life <= 0)
        {
            Delete();
        }
        Life -= Time.deltaTime;
        Speed += AddSpeed * Time.deltaTime;
        rBody.velocity = rBody.velocity.normalized * Speed;
        Scale += AddScale * Time.deltaTime;
        transform.localScale = transform.localScale.normalized * Scale;
    }

    void Delete()
    {
        GetComponent<Collider2D>().enabled = false;
        rBody.velocity = Vector3.zero;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LPlayer" || collision.tag == "SPlayer")
        {
            lpCtrl.death = true;
            spCtrl.death = true;
        }
        Delete();
    }
}
