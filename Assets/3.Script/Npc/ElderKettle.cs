using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderKettle : MonoBehaviour
{
    [SerializeField]
    private GameObject Button;
    [SerializeField]
    private Movement2D player;
    [SerializeField]
    private PlayerMovement player_move;
    [SerializeField]
    private bool Button_bool = true;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player_move);
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            player.enabled = false;
            player_move.enabled = false;
            Debug.Log("플레이어와 대화합니다 ^_^");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.enabled = true;
            player_move.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Button_bool = true;
            Button.SetActive(Button_bool);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Button_bool = false;
            Button.SetActive(Button_bool);
        }
    }
}
