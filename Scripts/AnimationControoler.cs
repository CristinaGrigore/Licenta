using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationControoler : MonoBehaviour
{
	public AnimationClip replaceableAnimation;
	// NPCs only have one default set of attacks
	public AnimationClip[] defaultAttackAnimationSet;
	// for the playable character the attack changes based on weapons
	public AnimationClip[] currentAttackAnimationSet;
	const float smoothTime = .1f; //.1 seconds damp time
	protected Animator animator;
	NavMeshAgent agent; //for character's speed
	protected CharacterCombat combat;
	public AnimatorOverrideController overrideController;

	protected virtual void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		combat = GetComponent<CharacterCombat>();
		if(overrideController == null)
			overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
		animator.runtimeAnimatorController = overrideController;
		currentAttackAnimationSet = defaultAttackAnimationSet;

		//subscribe OnAttack to OnAttack from CharacterCombat
		combat.OnAttack += OnAttack;
	}

	// Update is called once per frame
	protected virtual void Update()
    {
		//divide current speed by maximum possible speed
		if (agent != null)
		{
			float speedPercent = agent.velocity.magnitude / agent.speed;
			if(combat.inCombat)
				animator.SetFloat("speedPercent", 0f);
			else
				animator.SetFloat("speedPercent", speedPercent, smoothTime, Time.deltaTime);

			animator.SetBool("inCombat", combat.inCombat);
		}
    }

	protected virtual void OnAttack()
	{
		animator.SetTrigger("attack");
		
		int attackIndex = Random.Range(0, currentAttackAnimationSet.Length);
		Debug.Log("attack with " + currentAttackAnimationSet[attackIndex].name + " replace " + replaceableAnimation.name);
		//swap basic sword attack with random attack animation
		overrideController[replaceableAnimation.name] = currentAttackAnimationSet[attackIndex];
	}
}
