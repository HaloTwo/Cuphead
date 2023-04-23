using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossCardUI : CardManager
{
    [SerializeField] private string NextStage;

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }

        if (gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Z))
        {
            gameObject.SetActive(false);
            Loading.nextSceneName = NextStage;
            iris.nextSceneName = "Loading";
            iris.gameObject.SetActive(true);
        }
    }
}
