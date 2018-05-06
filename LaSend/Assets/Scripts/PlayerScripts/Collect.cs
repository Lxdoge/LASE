using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Collect : MonoBehaviour {
    public Image targetstone;
    public float fadetime = 1.0f;
    public ParticleSystem collectUIfx;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {	
	}
    void OnTriggerEnter2D(Collider2D obj)
    {
        string ta = obj.gameObject.tag;
        if(ta=="LPlayer"||ta=="SPlayer")
        {
            targetstone.DOFade(1.0f,fadetime);
            //公有变量收集+1
            Destroy(this.gameObject);
            collectUIfx.Play();
        }

    }
}
