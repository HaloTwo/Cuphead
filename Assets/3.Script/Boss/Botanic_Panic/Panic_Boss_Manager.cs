using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic_Boss_Manager : MonoBehaviour
{
    [SerializeField] private GameObject Boss1;
    [SerializeField] private GameObject Boss2;
    [SerializeField] private GameObject Boss3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Boss1.activeSelf && GameManager.Instance.curPhase == 1)
        {
            Boss2.SetActive(true);
        }else if(!Boss2.activeSelf && GameManager.Instance.curPhase == 2)
        {
            Boss3.SetActive(true);
        }
    }
}
