using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Weapon : MonoBehaviour
{
	public WeaponSO weapon;
	private SphereCollider myCollider;
	private Rigidbody myRigidBody;

	void Awake()
    {
		myCollider = GetComponent<SphereCollider>();
		myCollider.isTrigger = true;

		myRigidBody = GetComponent<Rigidbody>();
		myRigidBody.isKinematic = true;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("HITTTT");
			HealthComponent enemyHealth = collider.GetComponent<HealthComponent>();
			enemyHealth.TakeDamage(weapon.Damage);
		}
		
	}
}
