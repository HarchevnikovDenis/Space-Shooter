using System;
using UnityEngine;

// creating items that give the player bonuses
public class PickUpItems : MonoBehaviour
{
    [SerializeField] private GameObject pickUpEffect;
    [SerializeField] private Item item;

    private HealthController controller;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            try
            {
                controller = collision.gameObject.GetComponent<HealthController>();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(gameObject.name + " hasn't HealthController component");
            }
        }
        else
        {
            return;
        }

        if (item == Item.medKit)
        {
            CollectHP();
            return;
        }

        if(item == Item.forceField)
        {
            CollectForceField(collision.gameObject.transform);
            return;
        }
    }

    private void CollectHP()
    {
        if (!CanPickUp())
        {
            return;
        }

        Billboard.instance.UpdateHealthPoints();
        Camera.main.GetComponent<CameraController>().BonusHealth();

        CreateEffects();
        Destroy(gameObject);
    }

    private void CollectForceField(Transform player)
    {
        if(!CanPickUp())
        {
            return;
        }

        CreateEffects();

        try
        {
            GetComponent<ForceFieldCreator>().CreateForceField(player);
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException(gameObject.name + " hasn't ForceFieldCreator Component");
        }

        Destroy(gameObject);
    }

    private void CreateEffects()
    {
        GameObject effect = Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3.0f);
    }

    private bool CanPickUp()
    {
        switch (item)
        {
            case Item.medKit:
                if(controller.health < controller.MaxHealth)
                {
                    controller.health++;
                    return true;
                }
                else
                {
                    return false;
                }
            case Item.forceField:
                if(!controller.isFieldActive)
                {
                    controller.isFieldActive = true;
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return true;
        }
    }
}

enum Item
{
    medKit,
    forceField
}
