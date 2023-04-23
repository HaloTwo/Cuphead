using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    public GameObject[] mugshots;
    public List<TextMeshProUGUI> menus = new List<TextMeshProUGUI>();

    public Color ActiveColor;
    public Color DeactiveColor;

    int idx = 0;
    bool Keybored = false;

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

    private void OnEnable()
    {
        if (GameManager.Instance.CurState == GameState.GAMEOVER)
        {
            StartCoroutine(Card());           
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Keybored)
        {
            switch (Idx)
            {
                case 0:
                    SceneManager.LoadSceneAsync("Goopy_Le_Grande");
                    break;
                case 1:
                    GameManager.Instance.CurState = GameState.TRANSITION_OUT;
                    break;
                default:
                    Application.Quit();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Idx = Mathf.Max(Idx - 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Idx = Mathf.Min(Idx + 1, menus.Count - 1);
        }
    }

    IEnumerator Card()
    {
        yield return new WaitForSeconds(2f);
        Keybored = true;
        int phase = GameManager.Instance.curPhase;
        mugshots[0].SetActive(true);
        mugshots[phase].SetActive(true);       
    }
}
