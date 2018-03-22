using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class PanelBack : MonoBehaviour,IPointerClickHandler {
    public float backTime = 0.5f;
    bool doback;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(doback)
        {
            Tweener panelback = this.transform.parent.GetComponent<RectTransform>().DOLocalMoveX(960, backTime);
            panelback.SetUpdate(true);
            //this.transform.parent.GetComponent<RectTransform>().DOLocalMoveX(960, backTime, true);
            Invoke("init", backTime);
        }
	}
    public void OnPointerClick(PointerEventData pointdata)
    {
        doback = true;    
    }
    void init()
    {
        doback = false;
    }
}
