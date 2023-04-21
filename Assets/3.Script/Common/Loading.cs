using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public static string nextSceneName = string.Empty;

    void Start()
    {
        if (nextSceneName != string.Empty)
        {

            StartCoroutine(LoadMyAsyncScene());

            Debug.Log("�ε� �ڷ�ƾ �Ծ� ?");
        }
        else
            Debug.Log("�ε� �ڷ�ƾ �����");
    }

    IEnumerator LoadMyAsyncScene()
    {
        yield return new WaitForSeconds(1.7f);

        var asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
