using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isPlayer;

    public bool isFieldActive { get; set; }
    public int health { get; set; }
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    private void Awake()
    {
        health = maxHealth;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            health = 0;
        }

        if(isPlayer)
        {
            Billboard.instance.UpdateHealthPoints();
        }
    }

    public bool Kill()
    {
        return health == 0;
    }
}
