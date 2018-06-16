using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointCtrl : MonoBehaviour
{
    public GameObject gameManager;
    GameManager manager;
    bool isopen;
    public int NumofSavePoint;     //在关卡内的编号 外部定义
    SpriteRenderer spriteRenderer;
    //public Material Default_m;
    public Sprite Light_sp;        //点亮后的图像 外部定义
    BoxCollider2D Box;
    public GameObject Fire;
    // Use this for initialization
    void Start ()
    {
        manager = gameManager.GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Box = GetComponent<BoxCollider2D>();
        isopen = manager.pd.savePoint[NumofSavePoint]._pointOn;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isopen)
            LightthePoint();
	}

    void LightthePoint()
    {
        //spriteRenderer.material = Default_m;
        spriteRenderer.sprite = Light_sp;
        Fire.SetActive(true);
        Box.enabled = false;
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(1);
        isopen = true;
        manager.pd.PlayerPos(transform.position);//暂定与存档点重合，建议每个存档点另写
        manager.pd.SavePoint(NumofSavePoint);
        manager.Save();
    }
}
