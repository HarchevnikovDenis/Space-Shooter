using System.Collections.Generic;
using UnityEngine;

// script that creates a random item after the death of enemy
public class CreateRandomItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    private PlayerMovement player;
    private new Transform transform;

    private void Start()
    {
        transform = GetComponent<Transform>();
        
        try
        {
            player = FindObjectOfType<PlayerMovement>();
        }
        catch (System.NullReferenceException)
        {
            throw new System.NullReferenceException(gameObject.name + " didn't find PlayerMovement Component");
        }
    }

    public void CreateItem()
    {
        if (Mathf.Abs(transform.position.x) < player.MaxX && Mathf.Abs(transform.position.y) < player.MaxY)
        {
            if (Random.Range(0.0f, 1.0f) <= 0.1f)
            {
                if (Random.Range(0.0f, 1.0f) <= 0.5f)
                {
                    Instantiate(items[0], transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(items[1], transform.position, Quaternion.identity);
                }
            }
        }
    }
}
