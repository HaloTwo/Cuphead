using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Power : MonoBehaviour
{
    private PlayerMovement player;
    [SerializeField]
    private GameObject[] PowerCard;
    [SerializeField]
    private int gauge;
    [SerializeField]
    private float posYOffset = 5f; // 각각의 카드가 움직이는 Y축 거리


    public Animator Card1_animator;

    public Animator Card2_animator;

    public Animator Card3_animator;

    public Animator Card4_animator;

    public Animator Card5_animator;

    private void Start()
    {
        TryGetComponent(out player);
    }

    private void Update()
    {

        gauge = player.power_gauge;

        if (gauge <= 20)
        {
            Card1_animator.SetBool("Power", false);
            Card2_animator.SetBool("Power", false);
            Card3_animator.SetBool("Power", false);
            Card4_animator.SetBool("Power", false);
            Card5_animator.SetBool("Power", false);

            PowerCard[0].transform.localPosition = new Vector3(PowerCard[0].transform.localPosition.x, -150f + (posYOffset * gauge), PowerCard[0].transform.localPosition.z);

            PowerCard[1].transform.localPosition = new Vector3(PowerCard[1].transform.localPosition.x, -150f, PowerCard[0].transform.localPosition.z);
            PowerCard[2].transform.localPosition = new Vector3(PowerCard[2].transform.localPosition.x, -150f, PowerCard[0].transform.localPosition.z);
            PowerCard[3].transform.localPosition = new Vector3(PowerCard[3].transform.localPosition.x, -150f, PowerCard[0].transform.localPosition.z);
            PowerCard[4].transform.localPosition = new Vector3(PowerCard[4].transform.localPosition.x, -150f, PowerCard[0].transform.localPosition.z);
        }
        else if (gauge <= 40)
        {
            Card2_animator.SetBool("Power", false);
            Card3_animator.SetBool("Power", false);
            Card4_animator.SetBool("Power", false);
            Card5_animator.SetBool("Power", false);
            PowerCard[1].transform.localPosition = new Vector3(PowerCard[1].transform.localPosition.x, -150f + (posYOffset * (gauge - 20)), PowerCard[1].transform.localPosition.z);

            PowerCard[2].transform.localPosition = new Vector3(PowerCard[2].transform.localPosition.x, -150f, PowerCard[2].transform.localPosition.z);
            PowerCard[3].transform.localPosition = new Vector3(PowerCard[3].transform.localPosition.x, -150f, PowerCard[3].transform.localPosition.z);
            PowerCard[4].transform.localPosition = new Vector3(PowerCard[4].transform.localPosition.x, -150f, PowerCard[4].transform.localPosition.z);

        }
        else if (gauge <= 60)
        {
            Card3_animator.SetBool("Power", false);
            Card4_animator.SetBool("Power", false);
            Card5_animator.SetBool("Power", false);
            PowerCard[2].transform.localPosition = new Vector3(PowerCard[2].transform.localPosition.x, -150f + (posYOffset * (gauge - 40)), PowerCard[2].transform.localPosition.z);

            PowerCard[3].transform.localPosition = new Vector3(PowerCard[3].transform.localPosition.x, -150f, PowerCard[3].transform.localPosition.z);
            PowerCard[4].transform.localPosition = new Vector3(PowerCard[4].transform.localPosition.x, -150f, PowerCard[4].transform.localPosition.z);

        }
        else if (gauge <= 80)
        {
            Card4_animator.SetBool("Power", false);
            Card5_animator.SetBool("Power", false);
            PowerCard[3].transform.localPosition = new Vector3(PowerCard[3].transform.localPosition.x, -150f + (posYOffset * (gauge - 60)), PowerCard[3].transform.localPosition.z);

            PowerCard[4].transform.localPosition = new Vector3(PowerCard[4].transform.localPosition.x, -150f, PowerCard[4].transform.localPosition.z);

        }
        else if (gauge <= 100)
        {
            Card5_animator.SetBool("Power", false);
            PowerCard[4].transform.localPosition = new Vector3(PowerCard[4].transform.localPosition.x, -150f + (posYOffset * (gauge - 80)), PowerCard[4].transform.localPosition.z);
        }




        if (gauge >= 20 && gauge < 40)
        {
            Card1_animator.SetBool("Power", true);
            PowerCard[0].transform.localPosition = new Vector3(PowerCard[0].transform.localPosition.x, -50f , PowerCard[0].transform.localPosition.z);

            Card1_animator.SetBool("All", false);
            Card2_animator.SetBool("All", false);
            Card3_animator.SetBool("All", false);
            Card4_animator.SetBool("All", false);
            Card5_animator.SetBool("All", false);
        }
        else if (gauge >= 40 && gauge < 60)
        {
            PowerCard[0].transform.localPosition = new Vector3(PowerCard[0].transform.localPosition.x, -50f, PowerCard[0].transform.localPosition.z);
            PowerCard[1].transform.localPosition = new Vector3(PowerCard[1].transform.localPosition.x, -50f, PowerCard[1].transform.localPosition.z);

            Card1_animator.SetBool("Power", true);
            Card2_animator.SetBool("Power", true);

            Card1_animator.SetBool("All", false);
            Card2_animator.SetBool("All", false);
            Card3_animator.SetBool("All", false);
            Card4_animator.SetBool("All", false);
            Card5_animator.SetBool("All", false);

        }
        else if (gauge >= 60 && gauge < 80)
        {
            PowerCard[0].transform.localPosition = new Vector3(PowerCard[0].transform.localPosition.x, -50f, PowerCard[0].transform.localPosition.z);
            PowerCard[1].transform.localPosition = new Vector3(PowerCard[1].transform.localPosition.x, -50f, PowerCard[1].transform.localPosition.z);
            PowerCard[2].transform.localPosition = new Vector3(PowerCard[2].transform.localPosition.x, -50f, PowerCard[2].transform.localPosition.z);
      
            Card1_animator.SetBool("Power", true);
            Card2_animator.SetBool("Power", true);
            Card3_animator.SetBool("Power", true);


            Card1_animator.SetBool("All", false);
            Card2_animator.SetBool("All", false);
            Card3_animator.SetBool("All", false);
            Card4_animator.SetBool("All", false);
            Card5_animator.SetBool("All", false);


        }
        else if (gauge >= 80 && gauge < 100)
        {
            PowerCard[0].transform.localPosition = new Vector3(PowerCard[0].transform.localPosition.x, -50f, PowerCard[0].transform.localPosition.z);
            PowerCard[1].transform.localPosition = new Vector3(PowerCard[1].transform.localPosition.x, -50f, PowerCard[1].transform.localPosition.z);
            PowerCard[2].transform.localPosition = new Vector3(PowerCard[2].transform.localPosition.x, -50f, PowerCard[2].transform.localPosition.z);
            PowerCard[3].transform.localPosition = new Vector3(PowerCard[3].transform.localPosition.x, -50f, PowerCard[3].transform.localPosition.z);

            Card1_animator.SetBool("Power", true);
            Card2_animator.SetBool("Power", true);
            Card3_animator.SetBool("Power", true);
            Card4_animator.SetBool("Power", true);

            Card1_animator.SetBool("All", false);
            Card2_animator.SetBool("All", false);
            Card3_animator.SetBool("All", false);
            Card4_animator.SetBool("All", false);
            Card5_animator.SetBool("All", false);

        }
        else if (gauge == 100)
        {
            PowerCard[0].transform.localPosition = new Vector3(PowerCard[0].transform.localPosition.x, -50f, PowerCard[0].transform.localPosition.z);
            PowerCard[1].transform.localPosition = new Vector3(PowerCard[1].transform.localPosition.x, -50f, PowerCard[1].transform.localPosition.z);
            PowerCard[2].transform.localPosition = new Vector3(PowerCard[2].transform.localPosition.x, -50f, PowerCard[2].transform.localPosition.z);
            PowerCard[3].transform.localPosition = new Vector3(PowerCard[3].transform.localPosition.x, -50f, PowerCard[3].transform.localPosition.z);
            PowerCard[4].transform.localPosition = new Vector3(PowerCard[4].transform.localPosition.x, -50f, PowerCard[4].transform.localPosition.z);
           
            Card1_animator.SetBool("Power", true);
            Card2_animator.SetBool("Power", true);
            Card3_animator.SetBool("Power", true);
            Card4_animator.SetBool("Power", true);
            Card5_animator.SetBool("Power", true);

            Card1_animator.SetBool("All", true);
            Card2_animator.SetBool("All", true);
            Card3_animator.SetBool("All", true);
            Card4_animator.SetBool("All", true);
            Card5_animator.SetBool("All", true);
        }
    }
}

