using System;
using UnityEngine;

// force field protection
[RequireComponent(typeof(Animator))]
public class ForceField : MonoBehaviour
{
    [SerializeField] private float forceFieldLifetime;

    private HealthController controller;
    private Animator animator;
    private float time;

    private void Start()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        controller = player.GetComponent<HealthController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(time >= forceFieldLifetime)
        {
            animator.SetBool("isEnd", true);
            time = 0.0f;
            return;
        }

        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<WeaponCollision>())
        {
            WeaponCollision bullet = collision.gameObject.GetComponent<WeaponCollision>();
            if (!bullet.isFromForceField)
            {
                Protect(bullet);
            }
        }
    }

    private void Protect(WeaponCollision bullet)
    {
        bullet.ShowDamageEffect();
    }

    public void StopProtect()
    {
        try
        {
            controller.isFieldActive = false;
        }
        catch (NullReferenceException)
        {
            throw new NullReferenceException(gameObject.name + " hasn't HealthController component");
        }

        Destroy(gameObject);
    }
}
