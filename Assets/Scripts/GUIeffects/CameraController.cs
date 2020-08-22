using System;
using UnityEngine;

// Camera animation script
[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
	public void ShakeCamera()
	{
		try
		{
			GetComponent<Animator>().SetTrigger("Shake");
		}
		catch (NullReferenceException)
		{
			AnimatorIsNull();
		}
	}

	public void BonusHealth()
	{
		try
		{
			GetComponent<Animator>().SetTrigger("Bonus Health");
		}
		catch (NullReferenceException)
		{
			AnimatorIsNull();
		}
	}

	private void AnimatorIsNull()
	{
		throw new NullReferenceException("MainCamera hasn't Animator component");
	}
}
