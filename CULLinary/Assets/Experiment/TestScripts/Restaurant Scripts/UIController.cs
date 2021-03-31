using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// In charge of controlling when to show Menu UI and other notifications
public class UIController : MonoBehaviour
{
    public CookingStation cookingStation;

    private int totalAmt = 0;
    private int currButtonIdx = 0;

    // For selecting menu items using wasd
    [Header("Recipe Menu Items")]
    private int eggplantNum = 0;
    private int goldeggplantNum = 0;
    private int cornNum = 0;
    private int potatoNum = 0;

    public GameObject inventoryParent;

    public GameObject menuFirstButton;
    public GameObject menuSecondButton;
    public GameObject menuThirdButton;
    public GameObject menuFourthButton;
    public GameObject menuCloseButton;

    [Header("UI Elements")]
    public GameObject moneyText;
    public GameObject menuPanel;
    public GameObject counterNotifPanel;
    public GameObject notEnoughIngredientsNotifPanel;
    public GameObject closeNotifButton;
    public InventoryUI inventoryPanel;
    public UIRecipeBook recipeBookPanel;
    public GameObject ing_closeButton;
    public GameObject confirmLeaveNotifPanel;
    public GameObject confirmLeaveButton;

    private PlayerManager playerManager;

    [Header("Controls")]
    public string upKey;
    public string downKey;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        if (playerManager != null)
        {
            totalAmt = playerManager.GetMoney();
            moneyText.GetComponent<Text>().text = "Amount earned: $" + totalAmt.ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            currButtonIdx--;
            if (currButtonIdx == 0)
                currButtonIdx = 5; //loop back to the last option
            FindNextSelectedKey();
        }
        if (Input.GetKeyDown(downKey))
        {
            currButtonIdx++;
            if (currButtonIdx == 6)
                currButtonIdx = 1; //loop back to the first option
            FindNextSelectedKey();
        }
    }

    void FindNextSelectedKey()
    {
        GameObject selectedButton = null;

        switch (currButtonIdx)
        {
            case 1:
                selectedButton = menuFirstButton;
                break;
            case 2:
                selectedButton = menuSecondButton;
                break;
            case 3:
                selectedButton = menuThirdButton;
                break;
            case 4:
                selectedButton = menuFourthButton;
                break;
            case 5:
                selectedButton = menuCloseButton;
                break;
        }
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(selectedButton); //set a new selected object
    }

    // For Inventory Panel and Menu Panel (Called by CookingStation)
    public void ShowCookingPanel()
    {
        inventoryPanel.SetActive(true);
        menuPanel.SetActive(true);

        // Check ingredient count and disable any menu buttons if not enough ingredients
        eggplantNum = 0;
        goldeggplantNum = 0;
        cornNum = 0;
        potatoNum = 0;

        CheckInventory(); // update ingredient count

        // Debug.Log("current ing count (ep/gep/c/p): " + eggplantNum + "; " + goldeggplantNum + "; " + cornNum + "; "+ potatoNum);

        if (eggplantNum < 3)
        {
            menuFirstButton.GetComponent<Button>().interactable = false;
        }
        if (goldeggplantNum < 3)
        {
            menuSecondButton.GetComponent<Button>().interactable = false;
        }
        if ( !((eggplantNum >= 1) && (cornNum >= 1) && (potatoNum >= 1)) )
        {
            menuThirdButton.GetComponent<Button>().interactable = false;
        }
        if ( !((cornNum >= 2) && (potatoNum >= 1)) )
        {
            menuFourthButton.GetComponent<Button>().interactable = false;
        }

        recipeBookPanel.SetActive(true);
    }

    public void CloseCookingPanel()
    {
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        recipeBookPanel.SetActive(false);
    }

    // NOTIF: "Not enough counter space"
    public void ShowCounterNotifPanel()
    {
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        counterNotifPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(closeNotifButton); //set a new selected object
    }

    public void CloseCounterNotifPanel()
    {
        counterNotifPanel.SetActive(false);
        cookingStation.EnableMovementOfPlayer(); // Enable player movement so they can serve food when receive notif that counter has no space
    }

    // NOTIF: "Not enough ingredients"
    public void ShowNotEnoughIngredientsNotifPanel()
    {
        notEnoughIngredientsNotifPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(ing_closeButton); //set a new selected object
    }

    public void CloseNotEnoughIngredientsNotifPanel()
    {
        notEnoughIngredientsNotifPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    // NOTIF: "Confirm leave restaurant" if counter has food
    public void ShowConfirmLeaveNotifPanel()
    {
        confirmLeaveNotifPanel.SetActive(true);
    }

    public void CloseConfirmLeaveNotifPanel()
    {
        confirmLeaveNotifPanel.SetActive(false);
    }

    // To update the Amount Earned at top left hand corner
    public void AddWrongDishEarnings()
    {
        totalAmt += 50;
        moneyText.GetComponent<Text>().text = "Amount earned: $" + totalAmt.ToString();
        AddToGameData();
    }

    public void AddCorrectDishEarnings()
    {
        totalAmt += 100;
        moneyText.GetComponent<Text>().text = "Amount earned: $" + totalAmt.ToString();
        AddToGameData();
    }

    private void AddToGameData()
    {
        
        if (playerManager != null)
        {
            playerManager.SetMoney(totalAmt);
        }
    }

    // Check inventory to see what food can be cooked
    public void CheckInventory()
    {
        foreach (Transform child in inventoryParent.transform) // iterating thru inventory slots
        {
            GameObject itemButton = child.GetChild(0).gameObject; 
            GameObject icon = itemButton.transform.GetChild(0).gameObject;

            if (icon.GetComponent<Image>().isActiveAndEnabled == true) // only if there is an icon assigned
            {
                string foodName = icon.GetComponent<Image>().sprite.name;

                switch (foodName)
                {
                    case "eggplant":
                        eggplantNum++;
                        break;
                    case "golden_eggplant":
                        goldeggplantNum++;
                        break;
                    case "corn":
                        cornNum++;
                        break;
                    case "potato":
                        potatoNum++;
                        break;
                    default: // Do nothing if nothing assigned
                        break;
                }
            }   
            
           
        }
    }
}
