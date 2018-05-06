using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection{
    public int _collectionID;
    public bool _isCollected;

    public Collection()
    {
        _collectionID = 0;
        _isCollected = true;
    }

    public Collection(int collectionId, bool isCollected)
    {
        _collectionID = collectionId;
        _isCollected = isCollected;

    }

}
