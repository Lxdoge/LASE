using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
    Light Light;
    public float deltaIntensity;
    public float maxIntensity;
    float delta;
    bool Bright;
    public float Timer;
    public float maxTime;
    public bool ColorSwitch;
    int colorNow;
	// Use this for initialization
	void Start () {
        Light = GetComponent<Light>();
        delta = 1;
        Bright = false;
        colorNow = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Bright)
        {
            Flicker();
        }
        else
        {
            TimeRedun();
        }
	}

    void Flicker()
    {
        if (Light.intensity >= maxIntensity)
            delta = -1;
        Light.intensity += deltaIntensity * delta * Time.deltaTime;
        if (Light.intensity == 0)
        {
            Bright = false;
            delta = 1;
            if(ColorSwitch)
               Color();
        }
    }

    void TimeRedun()
    {
        Timer += Time.deltaTime;
        if(Timer >= maxTime)
        {
            Light.intensity = 0.01f;
            Bright = true;
            Timer = 0;
        }
    }

    void Color()
    {
        colorNow += 1;
        if (colorNow == 3)
            colorNow = 0;
        switch (colorNow)
        {
            case 0:Light.color = new Vector4(1,0.7f, 0.7f, 0.2f);
                break;
            case 1: Light.color = new Vector4(0.7f, 1, 0.7f, 0.2f);
                break;
            case 2: Light.color = new Vector4(0.7f, 0.7f, 1, 0.2f);
                break;
        }
    }
}
