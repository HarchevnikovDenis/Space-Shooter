using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour, IShooter
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private List<WeaponCharacterictic> weapons = new List<WeaponCharacterictic>();


    private void Update()
    {
        if(LevelState.instance.isGameOver)
        {
            return;
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].timeSinceLastShoot >= weapons[i].rate)
            {
                Shoot(i);
            }
            else
            {
                weapons[i].timeSinceLastShoot += Time.deltaTime;
            }
        }
    }

    // Index of Weapon
    public void Shoot(int index)
    {
        GameObject newBullet = Instantiate(weapons[index].prefab, shootPoint.position, transform.rotation);
        newBullet.GetComponent<WeaponController>().SetDirectionToBullet(newBullet.transform.right.normalized);
        newBullet.GetComponent<WeaponController>().SetLayer(gameObject.layer);
        newBullet.GetComponent<WeaponCollision>().SetWeaponDamage(weapons[index].damage);

        weapons[index].timeSinceLastShoot = 0.0f;
    }
}
