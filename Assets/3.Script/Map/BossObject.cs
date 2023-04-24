using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObject : MonoBehaviour
{
    [SerializeField] private GameObject Stage;
    [SerializeField] private GameObject Button;
    [SerializeField] private GameObject Background;
    [SerializeField] private Animator Flag;
    [SerializeField] private bool playerInRange = false;


    private void Awake()
    {
        GameObject.Find("Flag").TryGetComponent(out Flag);
    }

    private void Start()
    {
        if (GameManager.Instance.FirstBoss == true || GameManager.Instance.SecoundBoss == true)
        {
            Flag.SetBool("Flag", true);
        }
    }

    void Update()
    {
        //버튼이 켜졌고, 플레이어와 다가간 상태에서, z버튼을 누르면 보스UI 출력
        if (Button.activeSelf == true && playerInRange && Input.GetKeyDown(KeyCode.Z))
        {       
            Background.SetActive(true);
            Stage.SetActive(true);
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Background.SetActive(false);
        }
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어와 충돌하면 True
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
