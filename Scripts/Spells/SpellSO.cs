using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]

public class SpellSO : ScriptableObject
{
	public float ManaCost = 5f;
	public float Lifetime = 2f;
	public float Speed = 1f;
	public float Damage = 10f;
	public float SpellRadius = 0.5f;
	//Status effects
	//Thumbnail in the UI
	//Time between Casts

}
