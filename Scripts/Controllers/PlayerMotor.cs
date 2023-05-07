using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
	Transform target;	//target to follow
	NavMeshAgent player;
	Animator animator;
	// Start is called before the first frame update
	void Start()
    {
		player = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if(target != null)
		{
			player.SetDestination(target.position);
			//Face the Target if not attacking
			if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				FaceTarget();
			}
		}
	}
    public void MoveToPoint(Vector3 point)
    {
		player.SetDestination(point);
    }

	public void FollowTarget(Interactible newTarget)
	{
		player.stoppingDistance = newTarget.radius * .8f;
		// handle rotation separately or else Player won't change rotation unless past a certain
		//stopping distance 
		player.updateRotation = false;
		target = newTarget.interractionTransform;
	}
	
	public void StopFollowingTarget()
	{
		player.stoppingDistance = 0f;
		player.updateRotation = true;
		target = null;
	}

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		//don't look up and down
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		//interpolate between current rotation and new rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
