using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
	public GameObject uiPrefab;
	public Transform target; // position that UI follows
	float visibleTime = 5f;
	float lastMadeVisibleTime;

	Transform ui; //spawn position
	Image healthSlider;
	Transform cam;
    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main.transform;

        foreach( Canvas c in FindObjectsOfType<Canvas>())
		{
			if(c.name == "EnemyHealthUI")
			{
				ui = Instantiate(uiPrefab, c.transform).transform;
				healthSlider = ui.GetChild(0).GetComponent<Image>();
				ui.gameObject.SetActive(false);
				break;
			}
		}
		GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void LateUpdate()
    {
		if (ui != null)
		{
			ui.position = target.position;
			ui.forward = -cam.forward;

			if(Time.time - lastMadeVisibleTime > visibleTime)
				ui.gameObject.SetActive(false);
		}
    }

	void OnHealthChanged(int maxHealth, int currentHealth)
	{
		if (ui != null)
		{
			ui.gameObject.SetActive(true);
			lastMadeVisibleTime = Time.time;
			float healthPercent = (float)currentHealth / maxHealth;

			healthSlider.fillAmount = healthPercent;

			if (currentHealth <= 0)
			{
				Destroy(ui.gameObject);
			}
		}
	}
}
