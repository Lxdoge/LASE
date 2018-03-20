using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameStart : MonoBehaviour {
    public GameObject select_level;
	// Use this for initialization
	void Start () {
        Debug.Log(Screen.width);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Seclect_Level()
    {
        select_level.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f, false);
    }
    public void Back()
    {
        select_level.GetComponent<RectTransform>().DOLocalMoveX(960, 0.5f, false);
    }
}
