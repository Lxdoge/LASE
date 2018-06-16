using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData {
    public Vector3 lPlayer_Pos;
    public Vector3 sPlayer_Pos;
    public int Level_Num;
    /// <summary>
    /// ///////////////存档点////////////////////////////////
    /// </summary>
    public SavePoint[] savePoint;
    /// <summary>
    /// ///////////////第一关收集品//////////////////////////
    /// </summary>
    public Collection[] collection_1;
    /// <summary>
    /// ///////////////第二关收集品//////////////////////////
    /// </summary>
    public Collection[] collection_2;
    /// /// <summary>
    /// ///////////////第二关收集品//////////////////////////
    /// </summary>
    public Collection[] collection_3;

    public PlayerData()
    {
        Level_Num = 1;
        lPlayer_Pos = new Vector3(0, 0, 0);
        sPlayer_Pos = new Vector3(0, 0, 0);
        savePoint = new SavePoint[7];
        for (int i = 0; i < 7; i++)
            savePoint[i] = new SavePoint(i, false);
        collection_1 = new Collection[7];
        collection_2 = new Collection[7];
        collection_3 = new Collection[7];
        for (int i = 0; i < 7; i++)
            collection_1[i] = new Collection(i, false);
        for (int i = 0; i < 7; i++)
            collection_2[i] = new Collection(i, false);
        for (int i = 0; i < 7; i++)
            collection_3[i] = new Collection(i, false);
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
        savePoint[num] = new SavePoint(num, true);
    }

    public void GetCollection_1(int num)
    {
        collection_1[num] = new Collection(num, true);
    }

    public void GetCollection_2(int num)
    {
        collection_2[num] = new Collection(num, true);
    }

    public void GetCollection_3(int num)
    {
        collection_3[num] = new Collection(num, true);
    }
}
