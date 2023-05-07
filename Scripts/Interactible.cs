using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
	public float radius = 2f;
	public Transform interractionTransform;
	bool isFocused = false;
	Transform player;
	bool hasInterracted = false;

	public void OnFocused(Transform playerTransform)
	{
		isFocused = true;
		player = playerTransform;
		hasInterracted = false;
	}

	public void OnDefocused()
	{
		isFocused = false;
		player = null;
		hasInterracted = false;
	}

	public virtual void Interract()
	{
		//Debug.Log("interracting w/ " + transform.name);
	}
	 
	void Update()
	{
		if(isFocused && !hasInterracted)
		{
			float distance = Vector3.Distance(player.position, interractionTransform.position);
			if(distance <= radius)
			{
				Interract();
				hasInterracted = true;
			}
		}
	}
	void OnDrawGizmosSelected ()
	{
		if(interractionTransform == null) 
			interractionTransform = transform;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interractionTransform.position, radius);
	}
}
