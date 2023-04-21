using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iris_In_Scene : MonoBehaviour
{
    //[System.NonSerialized]
    [SerializeField]
     public string nextSceneName = string.Empty;

    [System.NonSerialized]
    public AsyncOperation asyncLoad;

    //생성했을때
    private void OnEnable()
    {
        if (nextSceneName != string.Empty)
            StartCoroutine(LoadMyAsyncScene());
    }

    public void OnTransitionEnd()
    {
        if (asyncLoad != null)
            asyncLoad.allowSceneActivation = true;
    }

    IEnumerator LoadMyAsyncScene()
    {
        //LoadSceneAsync - 비동기 방식으로 일시중지가 발생하지 않는 방식
        //LoadScene - 동기 방식으로 불러올 씬을 한꺼번에 불러오고 불려오는 동안 기다리는 방식

        asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;
        //allowSceneActivation - 장면이 준비된 즉시 장면을 활성화함

        //장면이 생성되지 않았으면 반복
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
