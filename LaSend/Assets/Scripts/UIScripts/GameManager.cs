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


    [HideInInspector]
    public static bool pause;
    [HideInInspector]
    public bool gameclear;
    [HideInInspector]
    public bool gameover;

    public int level;
    public int scenenum;
    private int maxlevel;

    GameObject panelpause;
    Canvas thiscanvas;
    float CanvasWidth;
    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        GameManager.pause = false;
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        else
        {
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
        Globe.nextScene = levelnum;
        if (levelnum <= maxlevel)//maxlevel为当前解锁的最大关卡、也可以是最大关卡，想玩哪关玩哪关
        {
            this.GetComponent<LoadScene>().enabled = true;
            //添加函数GetInitData()加载目标关卡及目标关卡的全部初始数据，以重新开始该关卡
        }
    }

    /// <summary>
    /// ///////////////////////////////////////////////////////////
    /// </summary>
    void PauseGame()
    {
        GameManager.pause = true;//暂停的算法日后要改
        Time.timeScale = 0;
        panelpause = GameObject.Find("Panel_Pause");
        panelpause.GetComponent<RectTransform>().DOMoveX(0, 0.5f);
    }
    //public void BackToGame()
    //{
    //    if (panelpause)
    //    {
    //        Time.timeScale = 1;
    //        panelpause.GetComponent<RectTransform>().DOMoveX(960, 0.5f);
    //    }
    //    else return;

    //}
    public void BackToMenu()
    {
        if (panelpause)
        {
            Time.timeScale = 1;
            Globe.nextScene = 0;
            SceneManager.LoadScene(Globe.nextScene);
            //以后获取canvas画布宽度更新
        }
        else return;
    }
    public void Setting()
    {

        if (GameObject.Find("Panel_Setting"))
        {
            if(GameObject.Find("Panel_Pause"))
            {
                Time.timeScale = 0;
            }          
            GameObject.Find("Panel_Setting").GetComponent<RectTransform>().DOMoveX(0, 0.5f, false);//setting层是不透明的，要有返回效果，层级要高于pause
        }

        else return;
    }

    public void Restart()
    {
        if (panelpause)
        {
            Time.timeScale = 1;
            Globe.nextScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(Globe.nextScene);
            //场景重启的代码需要LXD写
        }
        else return;
    }
    public void showPanel(string PanelName)
    {
        GameObject.Find(PanelName).GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f, false);
    }
}
