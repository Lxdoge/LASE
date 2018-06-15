using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    
    public GameObject gameManager;
    GameManager Manager;
    public GameObject mCamera;          //照相机
    AudioSource BGM;
    public AudioClip BossBgm;
    public AudioClip NormalBgm;
    
    public GameObject cameraPoint;      //相机定位
    public GameObject StateMachine;     //Boss状态机
    BossStateMachine BSM;
    public GameObject DistanceMachine;
    DistanceCtrl disCtrl;
    public GameObject lPlayer;
    public GameObject sPlayer;
    public GameObject Danger;
    public GameObject BeginPoint;
    public GameObject FinalBody;
    public GameObject[] Body;

    public int Life;
    // Use this for initialization
    private void Awake()
    {
    }
    void Start () {
        Manager = gameManager.GetComponent<GameManager>();
        disCtrl = DistanceMachine.GetComponent<DistanceCtrl>();
        BSM = StateMachine.GetComponent<BossStateMachine>();
        BGM = mCamera.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Manager.gameover)
        {
            BossOff();
        }
        if(Life == 1)
        {
            FinalBody.SetActive(true);
        }
        if(Life == 0)
        {
            BossOff();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossOn();
    }

    void BossOn()
    {
        lPlayer.transform.position = BeginPoint.transform.position;
        sPlayer.transform.position = BeginPoint.transform.position;
        Manager.boss = true;
        Danger.SetActive(true);
        //切BGM
        BGM.clip = BossBgm;
        BGM.Play();
        mCamera.GetComponent<Camera>().orthographicSize = 11;
        cameraPoint.GetComponent<CameraPoint>().enabled = false;
        cameraPoint.transform.position = StateMachine.transform.position;
        disCtrl.Xmax = 40;
        disCtrl.Ymax = 25;
        BSM.status = BossStateMachine.Status.Skill_3;
        Life = 9;
        for(int i = 0; i < 8; i++)
        {
            Body[i].SetActive(true);
            Body[i].GetComponent<BossBody>().Gear1.transform.position = Body[i].GetComponent<BossBody>().Gear1.GetComponentInParent<Transform>().position;
            Body[i].GetComponent<BossBody>().Gear2.transform.position = Body[i].GetComponent<BossBody>().Gear1.GetComponentInParent<Transform>().position;
        }
    }
    void BossOff()
    {
        Manager.boss = false;
        Danger.SetActive(false);
        //切BGM
        BGM.clip = NormalBgm;
        BGM.Play();
        mCamera.GetComponent<Camera>().orthographicSize = 5f;
        cameraPoint.GetComponent<CameraPoint>().enabled = true;
        disCtrl.Xmax = 19.5f;
        disCtrl.Ymax = 11.5f;
        BSM.status = BossStateMachine.Status.NotActive;
        FinalBody.SetActive(false);
        for (int i = 0; i < 8; i++)
        {
            Body[i].SetActive(false);
        }
    }
}

