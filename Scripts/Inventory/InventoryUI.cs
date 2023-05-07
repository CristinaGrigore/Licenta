
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Transform ItemsParent;
	public GameObject inventoryUI;
	InventorySlot[] slots; 
	Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
		inventory = Inventory.instance;
		//subscribe UpdateUI to event
		inventory.onItemChangedCallback += UpdateUI;
		slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Inventory"))
		{
			inventoryUI.SetActive(!inventoryUI.activeSelf);
		}
    }

	void UpdateUI()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			}
			else
			{
				slots[i].ClearSlot();
			}
		}

	}
}
