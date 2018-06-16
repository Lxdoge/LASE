﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Globe
{
    public static int nextScene;
}
public class LoadScene : MonoBehaviour
{
    public Slider loadingSlider;

    public Text loadingText;
    //public RawImage loadingImage;
    public float loadingSpeed = 1;
    //public float rotspeed = 40;
    private float targetValue;

    private AsyncOperation operation;

    // Use this for initialization  
    void Start()
    {
        //显示进度条
        loadingSlider.value = 0.0f;

        Time.timeScale = 1.0f;
        //启动协程  
        StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(Globe.nextScene);
        //阻止当加载完成自动切换  
        operation.allowSceneActivation = false;

        yield return operation;
    }

    // Update is called once per frame  
    void Update()
    {
        //loadingImage.transform.GetComponent<RectTransform>().Rotate(0, 0, rotspeed * Time.deltaTime);
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9  
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.value)
        {
            //插值运算  
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }

        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景  
            operation.allowSceneActivation = true;
        }
    }
}