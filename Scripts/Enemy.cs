using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactible
{
	PlayerManager playerManager;
	CharacterStats enemyStats;

	void Start()
	{
		playerManager = PlayerManager.instance;
		enemyStats = GetComponent<CharacterStats>();
	}
	public override void Interract()
	{
		base.Interract();
		//Attack the enemy
		CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
		if (playerCombat != null)
			playerCombat.Attack(enemyStats);
	}
}
