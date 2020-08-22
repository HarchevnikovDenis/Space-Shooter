using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShooting : MonoBehaviour, IShooter
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private List<WeaponCharacterictic> weapons;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].timeSinceLastShoot = weapons[i].rate;
        }
    }

    private void Update()
    {
        if(Input.GetAxis("Fire1") != 0 && weapons[0].timeSinceLastShoot >= weapons[0].rate)
        {
            Shoot(0);
        }
        else
        {
            AddTimeSinceLastShoot(0);
        }

        if (Input.GetAxis("Fire2") != 0 && weapons[1].timeSinceLastShoot >= weapons[1].rate)
        {
            Shoot(1);
        }
        else
        {
            AddTimeSinceLastShoot(1);
        }
    }

    private void AddTimeSinceLastShoot(int i)
    {
        weapons[i].timeSinceLastShoot += Time.deltaTime;
    }


    public void Shoot(int indexOfGun)
    {
        GameObject bullet = Instantiate(weapons[indexOfGun].prefab, shootPoint.position, transform.rotation);
        WeaponController controller = bullet.GetComponent<WeaponController>();
        controller.SetDirectionToBullet(bullet.transform.right.normalized);
        controller.SetLayer(gameObject.layer);
        bullet.GetComponent<WeaponCollision>().isFromForceField = true;

        bullet.GetComponent<WeaponCollision>().SetWeaponDamage(weapons[indexOfGun].damage);
        
        weapons[indexOfGun].timeSinceLastShoot = 0.0f;
    }
}
