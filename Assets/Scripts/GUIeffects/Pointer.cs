using System;
using UnityEngine;

// The direction indicators in which the next enemy was created
[RequireComponent(typeof(Animator))]
public class Pointer : MonoBehaviour
{
	private Animator animator;

	private void Awake()
	{
		try
		{
			animator = GetComponent<Animator>();
		}
		catch (NullReferenceException)
		{
			throw new NullReferenceException(gameObject.name + " hasn't Animator component");
		}
	}

	public void ShowPointer()
	{
		animator.SetTrigger("Show Pointer");
	}
}
