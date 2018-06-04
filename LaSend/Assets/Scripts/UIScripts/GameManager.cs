using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    string dataFileName = "LaSData.Xml"; //存档文件名
    XmlManager xm = new XmlManager();    //存档管理类
    public PlayerData pd;                //玩家数据

    public GameObject lPlayer, sPlayer;  //玩家角色

    
    public int levelNum;
    

    [HideInInspector]
    public bool pause;                   //暂停
    [HideInInspector]
    public bool gameclear;               //通关
    [HideInInspector]
    public bool gameover;                //失败
    [HideInInspector]
    public bool boss;                //Boss

    //private int maxlevel = 100;

    GameObject panelpause;
    //Canvas thiscanvas;
    float CanvasWidth;
    private void Awake()
    {
        //pd = new PlayerData();
        //Save();
        Load();

        pause = false;
        gameclear = false;
        gameover = false;
        boss = false;
    }
    void Start()
    {
        //pd = new PlayerData();
        //Save();
        //thiscanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            if (boss)
            {
                return;
            }
            gameover = false;
            ReFromSavePoint();
        }
        //Debug.Log(CanvasWidth);
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().name != "Loading")
        {
            panelpause = GameObject.Find("Panel_Pause");
            if (Input.GetKeyDown(KeyCode.Escape) && panelpause)
            {
                pause = !pause;
                //Debug.Log(pause);
            }

            if (pause == true)
            {
                this.showPanel("Panel_Pause");
                Tweener pauseout = panelpause.GetComponent<RectTransform>().DOLocalMoveX(0, 0.2f);
                pauseout.SetUpdate(true);
                //panelpause.GetComponent<RectTransform>().DOMoveX(0f,0.1f, true);
                Time.timeScale = 0;
            }
            else if (pause == false)
            {
                Time.timeScale = 1;
                Tweener pausein = panelpause.GetComponent<RectTransform>().DOLocalMoveX(960, 0.2f);
                pausein.SetUpdate(true);
            }
        }
        else
        {
            pause = false;
            Time.timeScale = 1;
            panelpause = null;
        }
        
    }
    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }
    //失败-从存档点重新开始
    public void ReFromSavePoint()
    {
        Load();
        LoadData();
    }
    //把状态信息从pd写入存档文件
    public void Save()
    {
        string dataFilePath = GetDataPath() + "/" + dataFileName;
        string serializeDataString = xm.SerializeObject(pd, typeof(PlayerData));
        xm.CreatXML(dataFilePath, serializeDataString);
    }
    //获取游戏根目录
    public string GetDataPath()
    {
        return Application.dataPath;
    }
    //从存档中读取玩家数据到PD
    public void Load()
    {
        string dataFilePath = GetDataPath() + "/" + dataFileName;//文件地址
        if (xm.HasFile(dataFilePath))//如果文件存在
        {
            string dataString = xm.LoadXML(dataFilePath);
            PlayerData pdFromXML = xm.DeserializeObject(dataString, typeof(PlayerData)) as PlayerData;
            if (pdFromXML != null)
            {
                pd = pdFromXML;
            }
            else
                Debug.Log("xml is null!");
        }
    }
    //根据PD更改场景内物体信息
    public void LoadData()
    {
        //角色位置
        lPlayer.transform.position = pd.lPlayer_Pos;
        sPlayer.transform.position = pd.sPlayer_Pos;
    }
    //根据存档载入游戏
    public void LoadGame()
    {
        Load();
        Globe.nextScene = pd.Level_Num;
        SceneManager.LoadScene("Loading");//读取xml知道数据保存在哪一个场景中，Globe.nextScene=目标场景层级
    }


    public void PrintData()
    {
        Debug.Log("pd_pos" + pd.lPlayer_Pos);
    }

    public void load_NewLevel(int levelnum)
    {
        pause = false;
        Globe.nextScene = levelnum;
        switch (levelnum)//根据选关用关卡初始状态覆盖存档
        {
            case 0:break;//主菜单
            case 1:
                pd = new PlayerData();
                pd.PlayerPos(new Vector3(5.64f, -1.71f, 0));
                pd.PlayerLevel(1);
                pd.SavePoint(0);
                Save();
                break;
            case 2:
                pd = new PlayerData();
                pd.PlayerPos(new Vector3(-3.64f, 0.23f, 0));
                pd.PlayerLevel(2);
                pd.SavePoint(0);
                Save();
                break;
            case 3:break;//loading
        }
        SceneManager.LoadScene("Loading");
    }
    
    //void PauseGame()
    //{
    //    pause = true;//暂停的算法日后要改

    //}
    public void BackToGame()
    {
        if (panelpause)
        {
            Tweener panelback = panelpause.GetComponent<RectTransform>().DOLocalMoveX(960, 0.2f);
            panelback.SetUpdate(true);
            if (pause)
            {
                pause = false;
            }
            ////panelpause.GetComponent<RectTransform>().DOMoveX(960, 0.1f,true);
            //Tweener tweener = panelpause.GetComponent<RectTransform>().DOMoveX(960, 0.1f);
            ////设置这个Tween不受Time.scale影响
            //tweener.SetUpdate(true);
        }
        else return;

    }
    //public void BackToMenu()
    //{
    //    if (panelpause)
    //    {           
    //        Globe.nextScene = 0;
    //        pause = false;
    //        SceneManager.LoadScene("Loading");
    //        //以后获取canvas画布宽度更新
    //    }
    //    else return;
    //}
    //public void Setting()
    //{

    //    if (GameObject.Find("Panel_Setting"))
    //    {
    //        if(GameObject.Find("Panel_Pause"))
    //        {
    //            Time.timeScale = 0;
    //        }          
    //        GameObject.Find("Panel_Setting").GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f, true);//setting层是不透明的，要有返回效果，层级要高于pause
    //    }

    //    else return;
    //}
    //重新开始本关
    public void Restart()
    {
        if (panelpause)
        {
            load_NewLevel(SceneManager.GetActiveScene().buildIndex);
        }
        else return;
    }
    public void showPanel(string PanelName)
    {
        if (GameObject.Find(PanelName))
        {
            //GameObject.Find(PanelName).GetComponent<RectTransform>().DOLocalMove(new Vector3(0,0,0), 0.1f, true);
            Tweener panelout = GameObject.Find(PanelName).GetComponent<RectTransform>().DOLocalMoveX(0, 0.2f);
            panelout.SetUpdate(true);
        }
        else
        {
            Debug.Log("errorpannel!");
        }
    }
    /////////////////////////////////////

    public void ChangeMU()
    {
        Slider mu = GameObject.Find("Slider_MU").GetComponent<Slider>();
        if(mu)
        {
            Debug.Log("mu");
            Camera.main.GetComponent<AudioSource>().volume = mu.value;
        }
       
    }
    //音效再看

}
