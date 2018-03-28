using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organo : MonoBehaviour {
    public bool timer;
    public float[] contents;//一个数组,代表第x个机关触发后存在时长有多少，虽然想要设置成为-1时长无限但目前没有想到解决办法
    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timer==true)
        {

        }
	}
}
