﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class SelectLevelUI : MonoBehaviour//, IPointerEnterHandler
{

    //public ScrollRect scroll;
    public ToggleGroup Tgroup;
    public Toggle []Togs;
    public Button []Buts;
    public float[] moveX;
    public HorizontalLayoutGroup butGrounp;
    int currentIndex;

    //public List<Toggle> toggleList = new List<Toggle>();
    float offsetX;
    float centerX;
    void Awake()
    {
        
    }
    void Start()
    {
        Debug.Log(centerX);
        allInit();
        Tgroup.allowSwitchOff = true;
        currentIndex = 0;
    }
    void Update()
    {
        Togs[currentIndex].isOn = true;
        ScrollMove();
    }
    //float countOffset()
    //{
    //    float groupleft = butGrounp.padding.left;
    //    float groupright=butGrounp.padding.right;
    //    float spca = butGrounp.spacing;
    //    float buttonwidth = Buts[currentIndex].GetComponent<Image>().preferredWidth ;
    //    offsetX = groupleft + (currentIndex - 1) * (buttonwidth + spca) + currentIndex / 2;
    //    return offsetX;
    //}
    public void OnPointer(int sendNum)
    {
        allInit();
        currentIndex = sendNum;
        //ScrollMove();
    }
    void ScrollMove()
    {
        Buts[currentIndex].GetComponent<LayoutElement>().DOPreferredSize(new Vector2(360f, 360f), 0.5f, false);
        Buts[currentIndex].GetComponent<Image>().DOColor(Color.white, 0.01f);
        butGrounp.GetComponent<RectTransform>().DOLocalMoveX(moveX[currentIndex],0.1f,false);
            }
    void allInit()
    {
        for (int i = 0; i < Togs.Length; i++)
        {
            Buts[i].GetComponent<LayoutElement>().DOPreferredSize(new Vector2(300f, 300f), 0.5f, false);
            Buts[i].GetComponent<Image>().color = Color.grey;
        }
    }


}
