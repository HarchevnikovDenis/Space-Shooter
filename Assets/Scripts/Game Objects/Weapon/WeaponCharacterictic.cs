using UnityEngine;

// Weapon Indicators
[System.Serializable]
public class WeaponCharacterictic
{
    public GameObject prefab;
    public float rate;
    public int damage;
    public float timeSinceLastShoot { get; set; }
}
