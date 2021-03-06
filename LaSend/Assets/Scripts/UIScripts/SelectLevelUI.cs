﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class SelectLevelUI : MonoBehaviour//, IPointerEnterHandler
{

    //public ScrollRect scroll;
    //public ToggleGroup Tgroup;
    //public Toggle []Togs;
    public Button[] Buts;
    public float[] moveX;
    public HorizontalLayoutGroup butGrounp;
    int currentIndex;
    public int allIndex;
    //public List<Toggle> toggleList = new List<Toggle>();
    //float offsetX;
    //float centerX;
    void Awake()
    {

    }
    void Start()
    {
        allInit();
        // Tgroup.allowSwitchOff = true;
        currentIndex = 1;
    }
    void Update()
    {
        //Togs[currentIndex].isOn = true;
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

    public void OnPointerNext()
    {
        allInit();
        if (currentIndex == allIndex)
        {
            currentIndex = -1;
        }
        currentIndex += 1;
 
        ScrollMove();
    }
    public void OnPointerPre()
    {
        allInit();
        if (currentIndex == 0)
        {
            currentIndex = allIndex+1;
        }
        currentIndex -= 1;
        
        ScrollMove();
    }
    void ScrollMove()
    {
        //Buts[currentIndex].GetComponent<LayoutElement>().DOPreferredSize(new Vector2(360f, 360f), 0.5f, true);
        Buts[currentIndex].GetComponent<RectTransform>().DOScaleX(1.3f, 0.5f);
        Buts[currentIndex].GetComponent<RectTransform>().DOScaleY(1.3f, 0.5f);
        Buts[currentIndex].GetComponent<Image>().DOColor(Color.white, 0.01f);
        butGrounp.GetComponent<RectTransform>().DOLocalMoveX(moveX[currentIndex], 0.1f, true);
    }
    void allInit()
    {
        for (int i = 0; i < Buts.Length; i++)
        {

            Buts[i].GetComponent<RectTransform>().DOScaleX(0.8f, 0.5f);
            Buts[i].GetComponent<RectTransform>().DOScaleY(0.8f, 0.5f);
            Buts[i].GetComponent<Image>().color = Color.grey;
        }
    }


}
