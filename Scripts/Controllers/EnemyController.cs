using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	public float lookRadius = 10f;
	Transform target;
	NavMeshAgent agent;
	CharacterCombat combat;
	Animator animator;


	// Start is called before the first frame update
	void Start()
    {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		combat = GetComponent<CharacterCombat>();
		animator = GetComponentInChildren<Animator>();
		agent.updateRotation = false;
	}

    // Update is called once per frame
    void Update()
    {
		float distance = Vector3.Distance(target.position, transform.position);
		if(distance <= lookRadius)
		{
			
			
			if(distance <= agent.stoppingDistance)
			{
				CharacterStats playerStats = target.GetComponent<CharacterStats>();
				agent.isStopped = true;
				//Attack the target
				if(playerStats != null)
				{
					Debug.Log("ENEMY ATTACK");
					combat.Attack(playerStats);
				}

				//Face the Target if not attacking
				if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
				{
					FaceTarget();
				}
			} else
			{
				agent.SetDestination(target.position);
				agent.isStopped = false;
			}
		}
    }

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		//interpolate between target rotation and current rotation
		transform.LookAt(target);
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
