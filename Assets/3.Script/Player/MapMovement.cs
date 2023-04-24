using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class MapMovement : MonoBehaviour
{
    private Movement2D movement2D;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject Button;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject EquipButton;
    [SerializeField] private GameObject BosscardUI;
    [SerializeField] private TextMeshProUGUI Coin;
    //[SerializeField] private GameObject Boss;

    bool bosscardActive = false;
    private bool victroy = false;
    public delegate void Del();


    void Start()
    {
        TryGetComponent(out movement2D);
        TryGetComponent(out animator);
        //받은 위치를 현재 위치로 전환
        transform.position = Player_State.Instance.playerPostion;
        if (GameManager.Instance.FirstBoss == true && Player_State.Instance.FirstBoss == false)
        {
            victroy = true;
            animator.SetBool("Victroy", victroy);
            StartCoroutine(Wait_co(3.5f, Victory));
            Player_State.Instance.FirstBoss = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        if (!victroy)
        {
            movement2D.MoveTo(new Vector3(x, y, 0));

            if (x == 0 && y == 0)
            {
                animator.SetBool("isMove", false);
            }
            else
            {
                animator.SetBool("isMove", true);
                animator.SetFloat("MoveX", x);
                animator.SetFloat("MoveY", y);
            }

        }

        int coin = Player_State.Instance.coin;
        Coin.text = "" + coin;


        Player_State.Instance.playerPostion = transform.position;
    }

    private void LateUpdate()
    {
        Debug.Log(BosscardUI.activeSelf);
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseUI.activeSelf && !EquipButton.activeSelf && !Button.activeSelf)
        {
            Time.timeScale = 0f;
            PauseUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseUI.activeSelf )
        {
            Time.timeScale = 1f;
            PauseUI.SetActive(false);

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !PauseUI.activeSelf && !EquipButton.activeSelf && !Button.activeSelf)
        {
            EquipButton.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && EquipButton.activeSelf)
        {
            EquipButton.SetActive(false);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Button.SetActive(false);
    }

    void Victory()
    {
        victroy = false;
        animator.SetBool("Victroy", victroy);
    }

    public IEnumerator Wait_co(float time, Del func)
    {
        yield return new WaitForSeconds(time);
        func();
    }

}
