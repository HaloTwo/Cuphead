using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] // 에셋을 만들기 위한 방법
public class StageData : ScriptableObject
{
    [SerializeField] private Vector2 limitMin;
    [SerializeField] private Vector2 limitMax;
    public Vector2 LimitMin => limitMin; // get한거랑 같음
    public Vector2 LimitMax => limitMax;

}
