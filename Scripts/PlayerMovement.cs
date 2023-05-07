using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent player;
    public Animator playerAnimator;
    public GameObject targetDest;
	public Transform castPoint;
	public Transform attackPoint;
	[SerializeField] private float maxMana = 100f;
	[SerializeField] private float currentMana = 0f;
	[SerializeField] private float manaRechargeRate = 2f;
	[SerializeField] private float timeToWaitForRecharge = 1f;
	[SerializeField] private float timeBetweenCasts = 1.5f;
	private float currentCastTimer;
	private float currentManaRechargeTimer;
	public Spell spellToCast;
	public Weapon weaponToUse;
	private bool castingMagic = false;
	private bool isAttacking = false;
	private Quaternion rotation;

	void Awake()
	{
		currentMana = maxMana;
	}
	void Update()
    {
		bool shiftPressed = Input.GetKey("left shift");
		bool spacePressed = Input.GetKey("space");
		bool qPressed = Input.GetKey("q");
		bool xPressed = Input.GetKey("x");
		bool IsWalking = playerAnimator.GetBool("IsWalking");
		bool IsRunning = playerAnimator.GetBool("IsRunning");
		bool IsJumping = playerAnimator.GetBool("IsJumping");
		bool MagicAttack1 = playerAnimator.GetBool("MagicAttack1");
		bool MagicAttack3 = playerAnimator.GetBool("MagicAttack3");
		bool MagicAttack5 = playerAnimator.GetBool("MagicAttack5");
		bool MelleeAttack = playerAnimator.GetBool("MelleeAttack");
		bool hasEnoughMana = currentMana - spellToCast.spellToCast.ManaCost >= 0;

		if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))
            {
				targetDest.transform.position = hitPoint.point;
                player.SetDestination(hitPoint.point);
				if (hitPoint.transform.tag == "Enemy")
				{
					
					playerAnimator.SetBool("IsRunning", true);
					isAttacking = true;
				}

			}
			if (shiftPressed && !IsRunning)
				playerAnimator.SetBool("IsRunning", true);
			else if(!IsWalking)
				playerAnimator.SetBool("IsWalking", true);
		}
		if (shiftPressed && !IsRunning)
		{
			playerAnimator.SetBool("IsRunning", true);
			playerAnimator.SetBool("IsWalking", false);
		}

		//daca a ajuns la inamic se opreste din alergat si ataca
		if (isAttacking && player.remainingDistance <= player.stoppingDistance)
		{
			Debug.Log("ATTACK");
			playerAnimator.SetBool("IsRunning", false);
			SwordAttack();

		}
		if (isAttacking)
			Debug.Log(playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

		if (!MagicAttack1 && xPressed && !castingMagic && hasEnoughMana)
		{
			player.isStopped = true;
			castingMagic = true;
			currentMana -= spellToCast.spellToCast.ManaCost;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitPoint;
			player.updateRotation = true;
			playerAnimator.SetBool("MagicAttack1", true);

			Debug.Log("CAST");
			currentCastTimer = 0;
			currentManaRechargeTimer = 0;

			if (Physics.Raycast(ray, out hitPoint))
			{
				targetDest.transform.position = hitPoint.point;
				Vector3 relativePos = targetDest.transform.position - transform.position;
				rotation = Quaternion.LookRotation(relativePos, Vector3.up);
				transform.rotation = rotation;
				if (IsWalking || IsRunning)
					player.SetDestination(hitPoint.point);
				CastSpell();
			}

		}
		if (!xPressed && player.isStopped == true)
		{
			currentCastTimer += Time.deltaTime;
			playerAnimator.SetBool("MagicAttack1", false);

			if (currentCastTimer > timeBetweenCasts)
			{
				player.isStopped = false;
				castingMagic = false;
			}
		}
		if (currentMana < maxMana && !castingMagic && !xPressed)
		{
			currentManaRechargeTimer += Time.deltaTime;
			if (currentManaRechargeTimer > timeToWaitForRecharge)
			{
				currentMana += manaRechargeRate * Time.deltaTime;
				if (currentMana >= maxMana)
					currentMana = maxMana;
			}
		}
		
		else if ((IsWalking || IsRunning) && player.remainingDistance <= player.stoppingDistance)
		{
			playerAnimator.SetBool("IsWalking", false);
			playerAnimator.SetBool("IsRunning", false);
		}
	
		if (IsRunning)
		{
			player.speed = 4;
			playerAnimator.SetBool("IsWalking", false);
		}
		else
			player.speed = 1.25f;
	}

	void CastSpell()
	{
		Instantiate(spellToCast, castPoint.position, castPoint.rotation);
	}
	void SwordAttack()
	{
		player.isStopped = true;
		playerAnimator.SetBool("MelleeAttack", true);
		Instantiate(weaponToUse, attackPoint.position, attackPoint.rotation);
	}
}
