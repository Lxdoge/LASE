using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {
    public Vector3 lPlayer_Pos;
    public Vector3 sPlayer_Pos;
    public int Level_Num;
    public SavePoint savePoint;
    public Collection collection;//目前需要每个收集品一个……emmmmmmmmm我之后再改吧

    public PlayerData()
    {
        Level_Num = 1;
        lPlayer_Pos = new Vector3(0, 0, 0);
        sPlayer_Pos = new Vector3(0, 0, 0);
        savePoint = new SavePoint(0, 1);
        collection = new Collection(0, 0, false);
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void PlayerPos(Vector3 pos)
    {
        lPlayer_Pos = pos;
        sPlayer_Pos = pos;
    }
    
    public void PlayerLevel(int level)
    {
        Level_Num = level;
    }

    public void SavePoint(int num)
    {
        savePoint = new SavePoint(num, 1);
    }

    public void GetCollection(int num, int level)
    {
        collection = new Collection(num, level, true);
    }
}
