using System;
using System.Collections.Generic;
using UnityEngine;

// display player HP
public class Billboard : MonoBehaviour
{
    [SerializeField] private GameObject hpImage;
    [SerializeField] private HealthController controller;

    private List<GameObject> healths = new List<GameObject>();
    public static Billboard instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < controller.health; i++)
        {
            GameObject hp = Instantiate(hpImage, transform.position, transform.rotation);
            healths.Add(hp);
            hp.transform.SetParent(gameObject.transform);
        }
    }
    
    public void UpdateHealthPoints()
    {
        if(controller.health < healths.Count)
        {
            for(int i = healths.Count - 1; i >= controller.health; i--)
            {
                // Lose HP
                try
                {
                    healths[i].GetComponent<Animator>().SetTrigger("Lose");
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException(healths[i].name + " hasn't Animator component");
                }

                healths.Remove(healths[i]);
            }
        }

        if(controller.health > healths.Count)
        {
            // Add HP
            GameObject hp = Instantiate(hpImage, transform.position, transform.rotation);
            healths.Add(hp);
            hp.transform.SetParent(gameObject.transform);
        }
    }
}
