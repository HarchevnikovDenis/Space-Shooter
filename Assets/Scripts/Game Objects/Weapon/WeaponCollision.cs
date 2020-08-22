using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField] private GameObject boomEffect;
    [SerializeField] private GameObject hitEffect;

    private LevelSetting levelSetting;
    // Effects that will be removed over time
    private List<GameObject> particles = new List<GameObject>();
    private int damage;

    public bool isFromForceField { get; set; }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            particles.Add(transform.GetChild(i).gameObject);
        }

        try
        {
            levelSetting = FindObjectOfType<LevelSetting>();
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException(gameObject.name + " didn't find LevelSetting component");
        }
    }

    public void SetWeaponDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HealthController>())
        {
            if (collision.gameObject.GetComponent<HealthController>().isFieldActive)
            {
                ShowDamageEffect();
                return;
            }

            collision.gameObject.GetComponent<HealthController>().Damage(damage);

            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                ShakeCamera();
            }

            if (collision.gameObject.GetComponent<HealthController>().Kill())
            {
                Boom(collision.gameObject);
                if (collision.gameObject.GetComponent<PlayerMovement>())
                {
                    Debug.Log("DEATH!!!");
                    // PLAYER Death
                    LevelState.instance.isGameOver = true;
                }
                if (collision.gameObject.GetComponent<EnemyMovement>())
                {
                    // ENEMY Death
                    try
                    {
                        collision.gameObject.GetComponent<CreateRandomItem>().CreateItem();
                    }
                    catch (NullReferenceException)
                    {
                        throw new NullReferenceException(collision.gameObject.name + " hasn't a CreateRandomItem Component");
                    }
                    levelSetting.UpdateEnemiesCount();
                }
            }
            else
            {
                ShowDamageEffect();
            }
        }

        if (collision.gameObject.GetComponent<WeaponCollision>())
        {
            if (collision.gameObject.GetComponent<WeaponCollision>().damage >= 2)
            {
                Boom(collision.gameObject);
            }
            else
            {
                ShowDamageEffect();
                Destroy(collision.gameObject);
            }
        }
    }

    private void ShakeCamera()
    {
        try
        {
            Camera.main.GetComponent<CameraController>().ShakeCamera();
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException(Camera.main.gameObject.name + " hasn't Camera Controller component");
        }
    }

    public void ShowDamageEffect()
    {
        DeleteShownParticles();

        Destroy(gameObject);
        GameObject hit = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(hit, 0.5f);
    }

    private void Boom(GameObject collision)
    {
        DeleteShownParticles();

        Destroy(gameObject);
        GameObject bang = Instantiate(boomEffect, transform.position, Quaternion.identity);
        Destroy(bang, 5.0f);
        Destroy(collision);
    }

    private void DeleteShownParticles()
    {
        foreach (GameObject particle in particles)
        {
            particle.transform.SetParent(null);
            Destroy(particle, 5.0f);
        }
    }
}
