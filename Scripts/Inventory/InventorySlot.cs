using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public Button removeButton;
	ItemSO item;

	public void AddItem(ItemSO newItem)
	{
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	public void ClearSlot()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	public void OnRemoveButton()
	{
		Debug.Log("remove");
		Inventory.instance.Remove(item);
	}

	public void UseItem()
	{
		if(item != null)
		{
			item.Use();
		}
	}
}
