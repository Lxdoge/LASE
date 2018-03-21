using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    string dataFileName = "LaSData.Xml";
    XmlManager xm = new XmlManager();
    public PlayerData pd;

    public GameObject lPlayer, sPlayer;
    public Vector3 lPlayer_Pos, sPlayer_Pos;
    

    [HideInInspector]
    public bool pause;
    [HideInInspector]
    public bool gameclear;
    [HideInInspector]
    public bool gameover;

    public int level;
    public int scenenum;
    private int maxlevel;

    private void Awake()
    {
    }
    // Use this for initialization
    void Start () {
        pause = false;
        gameclear = false;
        gameover = false;
        Load();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitPlayers()
    {
        lPlayer.transform.position = lPlayer_Pos;
    }

    public void LoadGame()
    {

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
        //读取xml知道数据保存在哪一个场景中，Globe.nextScene=目标场景层级
        SceneManager.LoadScene("Loading");
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

}
