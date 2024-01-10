using System;
using System.Diagnostics;
using UnityEngine.UI;

public class PlayerData 
{
    public string name = "PHW";
    public Image iconImage = null;
    public int level = 1;
    public int currentExp = 0;
    public int maxExp = 100;

    public event Action OnExpChanged;
    public event Action OnLevelChanged;

    public Health health; 

    public PlayerData(Health health)
    { 
        this.health = health;
    }

    public void UpdateExp(int addValue)
    {
        currentExp += addValue;
        while (currentExp >= maxExp)
        {
            LevelUp();
        }
        OnExpChanged?.Invoke();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= maxExp;

        maxExp += maxExp / 5;
        OnLevelChanged?.Invoke();
    }
}
