using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class MagicSystem : MonoBehaviour
{
	[SerializeField] private float maxMana = 100f;
	[SerializeField] private float currentMana = 0f;
	[SerializeField] private float manaRechargeRate = 2f;
	[SerializeField] private float timeBetweenCasts = 1.5f;
	[SerializeField] private Transform castPoint;

	private bool castingMagic = false;
	//private PlayerInput playerInput;
	private float currentCastTimer;
	public Animator playerAnimator;

	private void Awake()
	{
		//playerInput = new PlayerInput();
		//playerInput.CharacterControls.Spell.performed += context =>
		{

			SpellCast();
		};
	}
	private void SpellCast()
	{
		
		currentCastTimer = 0;
		castingMagic = true;
		//Vector2 mouse_pos = playerInput.CharacterControls.MousePosition.ReadValue<Vector2>();
		mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
		transform.localRotation = Quaternion.Euler(-mouse_pos.y, mouse_pos.x, 0);
		


	}


	private void OnEnable()
	{
		//playerInput.Enable();
	}

	private void Update()
	{
		bool zPressed = Input.GetKey("z");
		bool MagicAttack3 = playerAnimator.GetBool("MagicAttack3");

		if (castingMagic)
		{
			currentCastTimer += Time.deltaTime;
			Debug.Log("CAst!");

			if (!MagicAttack3 && zPressed)
				playerAnimator.SetBool("MagicAttack3", true);

			if (currentCastTimer > timeBetweenCasts)
				castingMagic = false;
		}
		if (MagicAttack3 && !zPressed)
			playerAnimator.SetBool("MagicAttack3", false);
	}
}
*/