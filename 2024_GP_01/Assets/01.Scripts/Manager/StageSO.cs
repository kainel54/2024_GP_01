using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/StageInfo")]
public class StageSO : ScriptableObject
{
    public List<StageTime> stageTimes;
}

[Serializable]
public struct StageTime
{
    public int min, sec;
}
