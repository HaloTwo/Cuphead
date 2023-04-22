using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Boss
{
    [SerializeField]
    GameObject before;
    [SerializeField]
    GameObject after;


    // Update is called once per frame
    void Update()
    {
        if(Currenthp <=0)
        {
            before.SetActive(false);
            after.SetActive(true);
            StartCoroutine(Boss_off_co(1f));
        }
    }
}
