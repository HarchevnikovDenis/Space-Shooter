using UnityEngine;

// Removing Health Slot / Script for Animation
[RequireComponent(typeof(Animator))]
public class LoseHPanimation : MonoBehaviour
{
	public void Delete()
	{
		Destroy(gameObject);
	}
}
