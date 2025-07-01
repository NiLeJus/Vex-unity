using System;

[System.Serializable]
/// <summary>
/// Resources Common  Logic
/// https://www.notion.so/Resources-1ac8661f58da8072bdd0cf1100667319?pvs=4
/// 
/// TDD => implement correct float / int
/// </summary>
public class ResourceBase
{

    public eResourcesName key; // StatName as enum value 
    public string name; // Name in string
    public float value; // Actual Value
    public float maxValue;

    // Constructor 
    public ResourceBase(eResourcesName ressourceName, float maxValue, float? value)
    {
        this.key = ressourceName;
        this.name = ressourceName.ToString();
        this.maxValue = maxValue;
        this.value = value ?? maxValue;

        if (key == eResourcesName.Integrity && maxValue > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(ressourceName), "Integrity should not be over 100");
        }
    }

    // Propriétés exposées 
    public string Name => name;
    public float MaxValue => maxValue;
    public float Value => value;

    public int SetBoth
    {
        set
        {
            maxValue = value;
            this.value = value;
        }
    }

    public int SetMaxValue
    {
        set
        {
            maxValue = value;
            this.value = value;
        }
    }

    // Add amount to ressource
    public void AddAmount(float amount)
    {
        value += amount;
        if (value > maxValue)
        {
            value = maxValue;
        }
    }

    public void SubAmount(float amount)
    {
        value -= amount;
        if (value < 0f)
        {
            value = 0f;
        }
        if (value == 0f)
        {

        }
    }

    public void ResetToMax()
    {
        value = maxValue;
    }

    public void RessourceIsZero()
    {
        //TDD => implement event firing
    }

    public bool IsRessourceZero()
    {
        if (value == 0f)
        {
            return true;
        }
        return false;
    }

    public void SetNewMaxAndBaseValue(float newMaxValue)
    {
        maxValue = newMaxValue;
        value = newMaxValue;
    }
}

