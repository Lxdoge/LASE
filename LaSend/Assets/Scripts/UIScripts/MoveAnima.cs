using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MoveAnima : MonoBehaviour, IPointerClickHandler
{
    public bool rotate;
    public bool move;
    public bool fade;
    public float fadeTime;
    public Vector2 destination;
    public float rotspeed = 20;
    //public float movespeedH;
    //public float movespeedV;
    public float moveTime = 1.0f;
    public float waitTime = 0f;
    public static bool can_moveandrot;
	// Use this for initialization
	void Start () {
       MoveAnima.can_moveandrot = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        MoveAnima.can_moveandrot = true;   
    }
    // Update is called once per frame
    void Update () {
        if (MoveAnima.can_moveandrot)
        {
            Invoke("mrf", waitTime);
        }
    }
    void mrf()
    {
        
        if (move == true)
        {
            this.GetComponent<RectTransform>().DOLocalMoveX(destination.x, moveTime, true);
            this.GetComponent<RectTransform>().DOLocalMoveY(destination.y, moveTime, true);
            //this.transform.Translate(movespeedH * Time.deltaTime, movespeedV * Time.deltaTime, 0);//左上
        }
        if (rotate == true)
        {
            this.GetComponent<RectTransform>().Rotate(0, 0, rotspeed * Time.deltaTime);
        }
        if (fade == true)
        {
            this.GetComponent<RawImage>().DOColor(Color.clear, fadeTime);
            Destroy(this.gameObject, fadeTime + 1.0f);
        }
    }

}
