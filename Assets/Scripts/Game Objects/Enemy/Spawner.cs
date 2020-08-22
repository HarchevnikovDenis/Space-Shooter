using System.Collections.Generic;
using UnityEngine;

// enemy creation at points
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawners = new List<Transform>();
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating("Spawn", 0.0f, 2.0f);
    }

    private void Spawn()
    {
        if (LevelState.instance.isGameOver)
        {
            CancelInvoke();
            return;
        }

        int index = Random.Range(0, spawners.Count);

        Instantiate(enemies[0], spawners[index].position, Quaternion.identity);
        ShowPointer(index);
    }

    private void ShowPointer(int index)
    {
        try
        {
            spawners[index].gameObject.GetComponent<Pointer>().ShowPointer();
        }
        catch (System.NullReferenceException)
        {
            throw new System.NullReferenceException(spawners[index].name + " hasn't Pointer Component");
        }
    }
}
