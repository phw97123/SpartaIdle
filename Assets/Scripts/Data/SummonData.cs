using System;

public enum SummonType
{
    Weapon, Armor, Skill
}

public class SummonData 
{
    public Action<int, int> OnExpChanged;
    public Action<int> OnLevelChanged;

    public SummonType Type;
    public int summonCurrentExp = 0;
    public int summonMaxExp = 100;
    public int summonLevel = 1;
    public int summonMaxLevel = 10;

    public int baseSummonPrice = 500; 

    public SummonData(SummonType type)
    {
        this.Type = type;
    }

    public void UpdateExp(int addValue)
    {
        summonCurrentExp += addValue; 
        while(summonCurrentExp >= summonMaxExp && summonLevel<summonMaxLevel)
        {
            LevelUp(); 
        }
        OnExpChanged?.Invoke(summonCurrentExp, summonMaxExp); 
    }

    private void LevelUp()
    {
        summonLevel++;
        summonCurrentExp -= summonMaxExp;
        summonMaxExp += summonMaxExp / 5; 
        OnLevelChanged?.Invoke(summonLevel);
    }

    public string GetTypeName(SummonType type)
    {
        switch(type)
        {
            case SummonType.Armor: return "°©¿Ê";
            case SummonType.Weapon: return "¹«±â";
            case SummonType.Skill: return "½ºÅ³"; 
        }
        return null; 
    }
}
