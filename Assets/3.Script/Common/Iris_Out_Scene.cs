using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Out_Scene : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    public void OnAnimationEnd()
    {
        Debug.Log(GameManager.Instance.CurState + " < Intro ³Êµµ ‰Î ?");
        gameObject.SetActive(false);
    }
}
