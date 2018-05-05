using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class SavePoint : MonoBehaviour {
    [HideInInspector]
    public bool [][]is_open;
    public Sprite litpic;
    public Animation lightSavePoint;
    string path;
    int scene;
    public int c;
	// Use this for initialization
	void Start () {
        is_open = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(is_open)
        {
            this.GetComponent<SpriteRenderer>().sprite = litpic;
            lightSavePoint.Play();
        }
	}
    void OnTriggerEnter2D(Collider2D obj)
    {
        string ta = obj.gameObject.tag;
        if(tag=="LPlayer"&&tag=="SPlayer")
        {
            this.is_open = true;
        }
    }
}
