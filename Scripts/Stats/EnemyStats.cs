using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
	{
		base.Die();
		//Death animation / add ragdoll effect

		Destroy(gameObject);
	}
}
