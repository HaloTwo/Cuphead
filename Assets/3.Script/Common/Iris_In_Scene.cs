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

    //����������
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
        //LoadSceneAsync - �񵿱� ������� �Ͻ������� �߻����� �ʴ� ���
        //LoadScene - ���� ������� �ҷ��� ���� �Ѳ����� �ҷ����� �ҷ����� ���� ��ٸ��� ���

        asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;
        //allowSceneActivation - ����� �غ�� ��� ����� Ȱ��ȭ��

        //����� �������� �ʾ����� �ݺ�
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
