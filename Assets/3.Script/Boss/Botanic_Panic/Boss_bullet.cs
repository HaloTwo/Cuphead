using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_bullet : MonoBehaviour
{


    public float bullet_speed = 10f;
    private void OnEnable()
    {
        
    }


    private void FixedUpdate()
    {
        transform.Translate(new Vector2(-bullet_speed, 0f) * Time.deltaTime);
    }


}
