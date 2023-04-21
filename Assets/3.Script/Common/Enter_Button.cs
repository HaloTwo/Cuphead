using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Button : MonoBehaviour
{

    [SerializeField] private string NextStage;
    [SerializeField] private Iris_In_Scene iris;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
