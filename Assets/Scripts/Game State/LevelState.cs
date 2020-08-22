using UnityEngine;

// Game state of the scene
public class LevelState : MonoBehaviour
{
    public static LevelState instance;
    public bool isGameOver { get; set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(this);
            }
        }
    }

}
