using UnityEngine;

// creation of a force field at the place of death of enemy
public class ForceFieldCreator : MonoBehaviour
{
    [SerializeField] private GameObject forceFieldPrefab;

    public void CreateForceField(Transform player)
    {
        GameObject forceField = Instantiate(forceFieldPrefab, player.position, Quaternion.identity);
        forceField.transform.SetParent(player);
    }
}
