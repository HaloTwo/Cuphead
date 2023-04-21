using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public enum state
    {
        TITLE = 1,
        TRANSITION,
    }

    private state curState = state.TITLE;

    public state CurState
    {
        get { return curState; }

        set
        {
            curState = value;
            switch (curState)
            {
                case state.TITLE:
                    titleCanvas.SetActive(true);
                    break;

                case state.TRANSITION:
                    iris.nextSceneName = "MainMenu";
                    StopCoroutine(Blink_co);
                    iris.gameObject.SetActive(true);
                    break;

                default:
                    break;
            }
        }
    }

    public GameObject titleCanvas;
    public Iris_In_Scene iris;
    public GameObject text;

    Coroutine Blink_co;
    public float blinkInterval;

    AsyncOperation asyncLoad;

    private void Start()
    {
        asyncLoad = iris.GetComponent<Iris_In_Scene>().asyncLoad;
    }

    void Update()
    {
        if (Blink_co == null)
            Blink_co = StartCoroutine(CoBlink());

        if (Input.anyKey)
        {
            CurState = state.TRANSITION;
        }
    }

    IEnumerator CoBlink()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkInterval);

            text.SetActive(!text.activeSelf);
        }
    }
}
