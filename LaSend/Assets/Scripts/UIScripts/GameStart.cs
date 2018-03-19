using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameStart : MonoBehaviour {
    public GameObject select_level;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Seclect_Level()
    {
        select_level.GetComponent<RectTransform>().DOLocalMoveX(400, 1.0f, false);
    }
}
