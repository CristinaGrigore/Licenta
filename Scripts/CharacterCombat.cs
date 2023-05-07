using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
	CharacterStats myStats;
	public float attackSpeed = 0.5f;
	//Delay taking damage 1 second (until the animation kicks in)
	public float attackDelay = 1f;
	//callback to notify Animator when attacking
	public event System.Action OnAttack;
	public bool inCombat { get; private set; }
	//if a character hasn't attacked in 5s they're no longer in combat
	const float combatCooldown = 5f;

	float lastAttackTime;
	public float attackCooldown = 0f;

	void Start()
	{
		myStats = GetComponent<CharacterStats>();
	}

	void Update()
	{
		attackCooldown -= Time.deltaTime;
		
		if(Time.time - lastAttackTime > combatCooldown)
		{
			inCombat = false;
		}
	}

   public void Attack(CharacterStats targetStats)
	{
		if(attackCooldown <= 0f)
		{
			// the coroutine delays the damage (for animation)
			StartCoroutine(DoDamage(targetStats, attackDelay));

			if (OnAttack != null)
				OnAttack();
			//the greater the speed the smaller the cooldown
			attackCooldown = 1f / attackSpeed;
			inCombat = true;
			lastAttackTime = Time.time;
		}

	}

	IEnumerator DoDamage(CharacterStats stats, float delay)
	{
		yield return new WaitForSeconds(delay);
		stats.TakeDamage(myStats.damage.GetValue());
		if (stats.currentHealth <= 0)
			inCombat = false;
		yield return new WaitForSeconds(delay);
	}
}
