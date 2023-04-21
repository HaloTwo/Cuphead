using UnityEngine;

public class CardManager : MonoBehaviour
{
    public MapMovement player;
    public Movement2D player_move;
    public Iris_In_Scene iris;


    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player_move);
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
    }

    public virtual void OnEnable()
    {
        player_move.enabled = false;
        player.enabled = false;
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Loading.nextSceneName = string.Empty;     
        }
    }

    private void OnDisable()
    {
        player.enabled = true;
        player_move.enabled = true;
    }
}