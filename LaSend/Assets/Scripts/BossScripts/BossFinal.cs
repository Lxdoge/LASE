using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour {
    public GameObject BT;            //Boss控制器

    public GameObject Face;
    SpriteRenderer FaceSprite;
    public Sprite sprite;
    public Sprite spriteN;
    public GameObject cameraPoint;

    int TwinkleTime;
    bool active;
    bool N;
    // Use this for initialization
    void Start () {
        active = true;
        TwinkleTime = 24;
        FaceSprite = Face.GetComponent<SpriteRenderer>();
        N = true;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
        if (!active)
        {
            Debug.Log(Time.timeScale);
            TwinkleTime -= 1;
            Time.timeScale += 0.01f;
            if (N)
            {
                N = !N;
                FaceSprite.sprite = sprite;
            }
            else if (!N)
            {
                N = !N;
                FaceSprite.sprite = spriteN;
            }
            if (TwinkleTime == 1)
            {
                FaceSprite.sprite = sprite;
                Time.timeScale = 1.0f;
                cameraPoint.GetComponent<CameraPoint>().enabled = true;
                BT.GetComponent<BossTrigger>().mCamera.GetComponent<Camera>().orthographicSize = 11;
                gameObject.SetActive(false);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BT.GetComponent<BossTrigger>().Life -= 1;
        BT.GetComponent<BossTrigger>().mCamera.GetComponent<Camera>().orthographicSize = 5;
        BT.GetComponent<BossTrigger>().mCamera.transform.position = Face.transform.position;
        Time.timeScale = 0.01f;
        active = false;
    }
}
