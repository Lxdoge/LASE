using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint {
    public int _pointID;
    public bool _pointOn;
	
    public SavePoint()
    {
        _pointID = 1;
        _pointOn = false;
    }

    public SavePoint(int pointId, bool pointOn)
    {
        _pointID = pointId;
        _pointOn = pointOn;
    }
}
