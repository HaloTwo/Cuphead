using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parring_Zone : MonoBehaviour
{
    public GameObject[] parry;
    private int idx = 0;

    private void Start()
    {
        // 첫 번째 오브젝트만 켜놓고 나머지는 모두 꺼둠
        for (int i = 0; i < parry.Length; i++)
        {
            if (i == 0)
            {
                parry[i].SetActive(true);
            }
            else
            {
                parry[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (parry[idx].activeSelf && idx + 1 < parry.Length)
        {
            if (!parry[idx].activeSelf)
            {
                parry[idx + 1].SetActive(true);
                idx++;
            }
        }
        else if (!parry[idx].activeSelf && idx + 1 < parry.Length)
        {
            parry[idx + 1].SetActive(true);
            idx++;
        }
        else if (!parry[idx].activeSelf && idx == parry.Length - 1)
        {
            parry[0].SetActive(true);
            idx = 0;
        }
    }
}
