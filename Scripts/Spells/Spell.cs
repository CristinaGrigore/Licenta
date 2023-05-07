using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Spell : MonoBehaviour
{
	public SpellSO spellToCast;
	private SphereCollider myCollider;
	private Rigidbody myRigidBody;

    void Awake()
    {
		myCollider = GetComponent<SphereCollider>();
		myCollider.isTrigger = true;
		myCollider.radius = spellToCast.SpellRadius;

		myRigidBody = GetComponent<Rigidbody>();
		myRigidBody.isKinematic = true;

		Destroy(this.gameObject, spellToCast.Lifetime);
    }

    // Update is called once per frame
    void Update()
    {
		if(spellToCast.Speed > 0f)
			transform.Translate(Vector3.forward * spellToCast.Speed * Time.deltaTime);

    }
	private void OnTriggerEnter(Collider collider)
	{
		//Apply Spell effects on whatever we hit
		//Apply hit particle effects
		//Apply sound effect
		if(collider.gameObject.CompareTag("Enemy"))
		{
			HealthComponent enemyHealth = collider.GetComponent<HealthComponent>();
			enemyHealth.TakeDamage(spellToCast.Damage);
		}
		Destroy(this.gameObject);
	}
}
