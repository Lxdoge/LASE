using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    string dataFileName = "LaSData.Xml";
    XmlManager xm = new XmlManager();
    public PlayerData pd;

    public GameObject lPlayer, sPlayer;
    public Vector3 lPlayer_Pos, sPlayer_Pos;


    public bool pause;

    [HideInInspector]
    public bool gameclear;
    [HideInInspector]
    public bool gameover;

    public int level;
    public int scenenum;
    private int maxlevel = 100;

    GameObject panelpause;
    Canvas thiscanvas;
    float CanvasWidth;
    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        pause = false;
        gameclear = false;
        gameover = false;
        Load();
        thiscanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(CanvasWidth);
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().name != "Loading")
        {
            panelpause = GameObject.Find("Panel_Pause");
            if (Input.GetKeyDown(KeyCode.Escape) && panelpause)
            {
                pause = !pause;
                Debug.Log(pause);
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

    void InitPlayers()
    {
        lPlayer.transform.position = lPlayer_Pos;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Loading");//读取xml知道数据保存在哪一个场景中，Globe.nextScene=目标场景层级
        Load();
        lPlayer.transform.position = pd.lPlayer_Pos;
    }

    //public void StartGame()
    //{
    //    SceneManager.LoadScene(1);
    //}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Save()
    {
        string dataFilePath = GetDataPath() + "/" + dataFileName;
        string serializeDataString = xm.SerializeObject(pd, typeof(PlayerData));
        xm.CreatXML(dataFilePath, serializeDataString);
    }

    public string GetDataPath()
    {
        return Application.dataPath;
    }

    public void Load()
    {
        string dataFilePath = GetDataPath() + "/" + dataFileName;
        if (xm.HasFile(dataFilePath))
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


    public void PrintData()
    {
        Debug.Log("pd_pos" + pd.lPlayer_Pos);
    }

    public void load_NewLevel(int levelnum)
    {
        pause = false;
        Globe.nextScene = levelnum;
        if (levelnum <= maxlevel)//maxlevel为当前解锁的最大关卡、也可以是最大关卡，想玩哪关玩哪关
        {
            SceneManager.LoadScene("Loading");
            //this.GetComponent<LoadScene>().enabled = true;
            //添加函数GetInitData()加载目标关卡及目标关卡的全部初始数据，以重新开始该关卡
        }
        //else if(levelnum==SceneManager.GetActiveScene().buildIndex)
        //{
        //    SceneManager.LoadScene("Loading");
        //}
    }

    /// <summary>
    /// ///////////////////////////////////////////////////////////
    /// </summary>
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

    public void Restart()
    {
        if (panelpause)
        {

            Globe.nextScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Loading");
            
            pause = false;
            //场景重启的代码需要LXD写
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
