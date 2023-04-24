using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardMenu : MonoBehaviour
{
    public List<TextMeshProUGUI> menus = new List<TextMeshProUGUI>();

    public Color ActiveColor;
    public Color DeactiveColor;
    public GameObject HighLight;

    int idx = 0;
    public int Idx
    {
        get { return idx; }
        set
        {
            menus[idx].color = DeactiveColor;
            idx = value;
            menus[idx].color = ActiveColor;
        }
    }

    void Start()
    {
        Idx = 0;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    switch (Idx)
        //    {
        //        case 0:
        //
        //            break;
        //        case 1:
        //
        //            break;
        //        default:
        //            Application.Quit();
        //            break;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Idx = Mathf.Max(Idx - 1, 0);
            HighLight.transform.localPosition = new Vector2(-177.3919f, -177.9441f);
            HighLight.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Idx = Mathf.Min(Idx + 1, menus.Count - 1);
            HighLight.transform.localPosition = new Vector2(156f, -177.9441f);
            HighLight.transform.localScale = new Vector3(1.25f, 1f, 1f);
        }
    }
}
