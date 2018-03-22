using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameStart : MonoBehaviour {
    public GameObject select_level;
    public RawImage Logo;
	// Use this for initialization
	void Start () {
        Logo.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
        if (MoveAnima.can_moveandrot)
        {
            Invoke("ShowLogo", 2.0f);
        }

    }
    public void Seclect_Level()
    {
        select_level.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f, true);
    }
    public void Back()
    {
        select_level.GetComponent<RectTransform>().DOLocalMoveX(960, 0.5f, true);
    }
    void ShowLogo()
    {
        if(Logo)
        {
            Logo.DOColor(Color.white,3.0f);
        }
    }
}
