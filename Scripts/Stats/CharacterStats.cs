
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public int maxHealth = 100;
	//any other class is able to get this value but it 
	//can only be set from within this class
	public int currentHealth { get; private set; }
	public Stat damage;
	public Stat armor;
	public event System.Action<int, int> OnHealthChanged; //first - max health, 2nd - current health
	void Awake()
	{
		currentHealth = maxHealth;

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
			TakeDamage(10);
	}

	public void TakeDamage(int damage)
	{
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);
		currentHealth -= damage;

		Debug.Log(transform.name + " takes " + damage + " damage.");

		if (OnHealthChanged != null)
			OnHealthChanged(maxHealth, currentHealth);
		if(currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		//die in some way??
		Debug.Log(transform.name + " DIED!");
	}

}
