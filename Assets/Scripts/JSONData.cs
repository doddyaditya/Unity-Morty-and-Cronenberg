using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JSONData
{
    public dataWrapper[] dataInfo;
    
}

[Serializable]
public class dataWrapper
{
    public string username;
    public int score;
}