using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bullets : MonoBehaviour {
    [HideInInspector]
    public float life;
    public float speed;
    Vector2 targetpos;
    public GameObject Player;//分两种Player，存成两个prefab
    // Use this for initialization
    float distance;
    void Start () {
        Destroy(this.gameObject, life);
        targetpos = Player.transform.position;
        distance = Mathf.Sqrt(Mathf.Pow(this.transform.position.x-targetpos.x, 2) + Mathf.Pow(this.transform.position.y -targetpos.y, 2));
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.DOMove(targetpos, distance/speed);
	}
}
