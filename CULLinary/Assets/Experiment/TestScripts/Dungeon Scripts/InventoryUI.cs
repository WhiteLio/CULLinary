using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] private GameObject inventoryUI;  // The entire UI
	[SerializeField] private Transform inventoryPanel;   // The parent object of all the items
	private bool isShowing = false;

	private DungeonPlayerInventory inventory;

	private InventorySlot[] slots;
	private List<Item> itemList = new List<Item>(); // Inventory
	[SerializeField] private int inventoryLimit = 20;
	
    [SerializeField] private GameObject damageCounter_prefab;

	void Start()
	{
		inventory = DungeonPlayerInventory.instance;
		inventory.OnItemAdd += AddItem;
		inventory.OnItemRemove += RemoveItem;
		slots = inventoryPanel.GetComponentsInChildren<InventorySlot>();

		Scene scene = SceneManager.GetActiveScene();
		if (scene.name != "TestRestaurant" ) // Might need to change later if restaurant scene's name changes
			inventoryUI.SetActive(false); // Don't need this line for restaurant scene if not inventory won't show up on first interaction w cooking station
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			ToggleVisiblity();
		}
	}

	public void ToggleVisiblity()
	{
		isShowing = !isShowing;
		inventoryUI.SetActive(isShowing);
	}

	private void AddItem(Item item)
	{
		if (itemList.Count < inventoryLimit)
		{
			itemList.Add(item);
			UpdateUI();
		}
        else
        {
            Debug.Log("Not enough room");
        }

	}

	private void RemoveItem(Item item)
	{
		itemList.Remove(item);
		UpdateUI();
	}

    public List<Item> GetItemList()
    {
        return itemList;
    }

	private void OnDestroy()
	{
 		inventory.OnItemAdd -= AddItem;
		inventory.OnItemRemove -= RemoveItem;
	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	private void UpdateUI()
	{

		for (int i = 0; i < slots.Length; i++)
		{
			if (i < itemList.Count)
			{
				slots[i].AddItem(itemList[i]);
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
	}
}
