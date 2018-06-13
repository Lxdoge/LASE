using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {
    
    public GameObject gameManager;
    GameManager Manager;
    public GameObject mCamera;          //照相机
    public GameObject cameraPoint;      //相机定位
    public GameObject StateMachine;     //Boss状态机
    BossStateMachine BSM;
    public GameObject DistanceMachine;
    DistanceCtrl disCtrl;
    public GameObject lPlayer;
    public GameObject sPlayer;
    public GameObject Danger;
    public GameObject BeginPoint;
    // Use this for initialization
    private void Awake()
    {
    }
    void Start () {
        Manager = gameManager.GetComponent<GameManager>();
        disCtrl = DistanceMachine.GetComponent<DistanceCtrl>();
        BSM = StateMachine.GetComponent<BossStateMachine>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Manager.gameover)
        {
            Manager.boss = false;
            Danger.SetActive(false);
            //切BGM
            mCamera.GetComponent<Camera>().orthographicSize = 5.5f;
            cameraPoint.GetComponent<CameraPoint>().enabled = true;
            disCtrl.Xmax = 19.5f;
            disCtrl.Ymax = 11.5f;
            BSM.status = BossStateMachine.Status.NotActive;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lPlayer.transform.position = BeginPoint.transform.position;
        sPlayer.transform.position = BeginPoint.transform.position;
        Manager.boss = true;
        Danger.SetActive(true);
        //切BGM
        mCamera.GetComponent<Camera>().orthographicSize = 11;
        cameraPoint.GetComponent<CameraPoint>().enabled = false;
        cameraPoint.transform.position = StateMachine.transform.position;
        disCtrl.Xmax = 40;
        disCtrl.Ymax = 25;
        BSM.status = BossStateMachine.Status.Skill_3;
    }
}

