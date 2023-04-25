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
        gameObject.SetActive(false);
    }
}
