using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Out : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
