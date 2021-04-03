using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string inventory;
    public int stage; //references the game progress in terms of stage
    public int currentIndex; //references whether we are in the rest/dungeon
    public string playerName;
    public int money;
    public int maxHealth;
    public int rangeDamage;

    public PlayerData()
    {
        this.inventory = "";
        this.stage = 0;
        this.money = 0;
        this.currentIndex = 1; //Default goes to restaurant
        this.playerName = "John Doe";
        this.maxHealth = 150;
        this.rangeDamage = 20;
    }

    public string GetInventoryString()
    {
        return this.inventory;
    }

    public int GetStage()
    {
        return this.stage;
    }

    public int GetCurrentIndex()
    {
        return this.currentIndex;
    }

    public string GetPlayerName()
    {
        return this.playerName;
    }

    public int GetMoney()
    {
        return this.money;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public int GetRangeDamage()
    {
        return this.rangeDamage;
    }

    public void SetRangeDamage(int dmg)
    {
        this.rangeDamage = dmg;
    }

    public void SetInventoryString(string inventory)
    {
        this.inventory = inventory;
    }

    public void SetStage(int stage)
    {
        this.stage = stage;
    }

    public void SetCurrentIndex(int currentIndex)
    {
        this.currentIndex = currentIndex;
    }

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public void SetMoney(int money)
    {
        this.money = money;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string jsonData)
    {
        try
        {
            JsonUtility.FromJsonOverwrite(jsonData, this);
        } 
        catch
        {
            Debug.Log("No save file...");
        }
    }

}