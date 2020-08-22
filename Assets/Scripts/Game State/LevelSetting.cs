using UnityEngine;
using UnityEngine.UI;

public class LevelSetting : MonoBehaviour
{
    [SerializeField] private Text leftEnemies;
    [SerializeField] private int enemiesOnLevel;

    private void Start()
    {
        leftEnemies.text = enemiesOnLevel.ToString();
    }

    public void UpdateEnemiesCount()
    {
        enemiesOnLevel--;
        leftEnemies.text = enemiesOnLevel.ToString();
    }

}
