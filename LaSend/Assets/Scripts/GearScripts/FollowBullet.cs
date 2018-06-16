using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : MonoBehaviour {
    public float life;
    public float speed;
    public bool toL;
    [HideInInspector]
    public Vector3 targetpos;
    [HideInInspector]
    public GameObject gameManager;
    GameManager Manager;
    Animator animator;
    Rigidbody2D rBody;
    GameObject lPlayer;
    GameObject sPlayer;
    [HideInInspector]
    public LPlayerCtrl lpCtrl;
    [HideInInspector]
    public SPlayerCtrl spCtrl;

    // Use this for initialization
    void Start () {
        lPlayer = GameObject.Find("LPlayer");
        sPlayer = GameObject.Find("SPlayer");

        lpCtrl = lPlayer.GetComponent<LPlayerCtrl>();
        spCtrl = sPlayer.GetComponent<SPlayerCtrl>();

        gameManager = GameObject.Find("GameManager");
        Manager = gameManager.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        Follow();
        if (!Manager.boss)
        {
            Delete();
        }
    }

    void Follow()
    {
        if (toL)
        {
            targetpos = lPlayer.transform.position;
        }
        if (!toL)
        {
            targetpos = sPlayer.transform.position;
        }
        if (targetpos.y - transform.position.y >= 0)
            transform.Rotate(new Vector3(0, 0, Vector3.Angle(new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y), Vector3.right)));
        else
            transform.Rotate(new Vector3(0, 0, -Vector3.Angle(new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y), Vector3.right)));
        rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = new Vector3(targetpos.x - transform.position.x, targetpos.y - transform.position.y).normalized * speed;
    }

    void Delete()
    {
        GetComponent<Collider2D>().enabled = false;
        rBody.velocity = Vector3.zero;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LPlayer" || collision.gameObject.tag == "SPlayer")
        {
            lpCtrl.death = true;
            spCtrl.death = true;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetComponent<FollowBullet>().toL != toL)
                Delete();
        }

    }
}
