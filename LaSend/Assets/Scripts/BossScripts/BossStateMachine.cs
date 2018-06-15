using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour {
    [HideInInspector]
    public enum Status {NotActive, Skill_1, Skill_2, Skill_3 };//Boss状态
    [HideInInspector]
    public Status status;

    public GameObject[] Killer_1;
    public float Cd_1;

    public GameObject[] Killer_2;
    public float Cd_2;

    public GameObject[] Killer_3;
    public float Cd_3;

    float Timer;
    bool active;

    public GameObject gameManager;
    GameManager Manager;
    // Use this for initialization
    private void Awake()
    {
        status = Status.NotActive;
        Timer = Cd_1;
        active = false;
    }
    void Start () {
        Manager = gameManager.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Manager.gameover)
        {
            status = Status.NotActive;
            Timer = Cd_1;
        }
        switch (status)
        {
            case Status.NotActive:
                if (active)
                {
                    active = !active;
                    CloseKill_1();
                    CloseKill_2();
                    CloseKill_3();
                }
                break;
            case Status.Skill_1:
                DoKill_1();
                break;
            case Status.Skill_2:
                DoKill_2();
                break;
            case Status.Skill_3:
                DoKill_3();
                break;
        }
	}

    void DoKill_1()
    {
        if (!active)
            active = true;
        Timer -= Time.deltaTime;
        if(Timer <= 0.0f)
        {
            status = Status.Skill_2;
            Timer = Cd_2;

            CloseKill_1();
            OpenKill_2();
            
        }
    }

    void DoKill_2()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0.0f)
        {
            status = Status.Skill_3;
            Timer = Cd_3;

            CloseKill_2();
            OpenKill_3();
            
        }
    }

    void DoKill_3()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0.0f)
        {
            status = Status.Skill_1;
            Timer = Cd_1;

            OpenKill_1();
            CloseKill_3();
        }
    }
    //启动技能1所有机关
    void OpenKill_1()
    {
        for (int i = 0; i < 12; i++)
        {
            Killer_1[i].SetActive(true);
        }
    }
    //关闭技能1所有机关
    void CloseKill_1()
    {
        for (int i = 0; i < 12; i++)
        {
            Killer_1[i].SetActive(false);
        }
    }
    //启动技能2所有机关
    void OpenKill_2()
    {
        for (int i = 0; i < 2; i++)
        {
            Killer_2[i].SetActive(true);
        }
    }
    //关闭技能2所有机关
    void CloseKill_2()
    {
        for (int i = 0; i < 2; i++)
        {
            Killer_2[i].SetActive(false);
        }
    }
    //启动技能3所有机关
    void OpenKill_3()
    {
        for (int i = 0; i < 16; i++)
        {
            Killer_3[i].SetActive(true);
        }
    }
    //关闭技能3所有机关
    void CloseKill_3()
    {
        for (int i = 0; i < 16; i++)
        {
            Killer_3[i].SetActive(false);
        }
    }
}
