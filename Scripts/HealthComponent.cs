using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	[SerializeField] private float MaxHealth = 50;
	private float CurrentHealth;

	public void TakeDamage(float damageToApply)
	{
		CurrentHealth -= damageToApply;
		if(CurrentHealth <= 0)
		{
			Destroy(this.gameObject);
		}
	}
    // Start is called before the first frame update
    void Awake()
    {
		CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
