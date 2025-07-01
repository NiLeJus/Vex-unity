using System.Collections.Generic;
using UnityEngine;

[ES3Serializable]

/// <summary>
/// Use for creating new creature Data set in the Unity Editor > inspector
/// Right click in project tab => create => Vexplorer => new creature  
/// implémentation => https://www.youtube.com/watch?v=x8B_eXfcj6U&list=PLLf84Zj7U26kfPQ00JVI2nIoozuPkykDX&index=5&ab_channel=GameDevExperiments
/// </summary>
[CreateAssetMenu(fileName = "Vexplorer", menuName = "Vexplorer/Create new Creature")]
public class _CreatureBase : ScriptableObject
{
    [Header("Informations")]
    [field: SerializeField, TextArea] public string Name { get; private set; }
    [field: SerializeField, TextArea] public string description { get; private set; }
    public string Description
    {
        get { return description; }
    }

    [field: SerializeField] public Texture2D portrait { get; private set; }

    [Header("Indicator")]
    [field: SerializeField] public int Rarity { get; private set; }
    [field: SerializeField] public int Worth { get; private set; }

    [Header("Visual Prefab")]
    [field: SerializeField] public GameObject pfCreatureVisual { get; private set; }

    [Header("Type")]
    [field: SerializeField] public CreatureType Type1 { get; private set; }
    [field: SerializeField] public CreatureType Type2 { get; private set; }

    [Header("Stats")]
    [SerializeField] private StatBase strength;
    [SerializeField] private StatBase dexterity;
    [SerializeField] private StatBase intel;
    [SerializeField] private StatBase agility;
    [SerializeField] private StatBase physic;
    [SerializeField] private StatBase focus;

    public StatBase Strength => strength;
    public StatBase Dexterity => dexterity;
    public StatBase Intel => intel;
    public StatBase Agility => agility;
    public StatBase Physic => physic;
    public StatBase Focus => focus;


    public StatBase GetStat(StatsName stat)
    {
        switch (stat)
        {
            case StatsName.Strength: return strength;
            case StatsName.Dexterity: return dexterity;
            case StatsName.Intel: return intel;
            case StatsName.Agility: return agility;
            case StatsName.Physic: return physic;
            case StatsName.Focus: return focus;
            default: return null;
        }
    }


    public int GetAllLevelsAmount()
    {
        int total = 0;

        if (strength != null) total += strength.Level;
        if (dexterity != null) total += dexterity.Level;
        if (intel != null) total += intel.Level;
        if (agility != null) total += agility.Level;
        if (physic != null) total += physic.Level;
        if (focus != null) total += focus.Level;

        return total;
    }
    [field: SerializeField] public HandlerResources HandlerRessources { get; private set; }
    [field: SerializeField] public HandlerIndicators HandlerIndicators { get; private set; }

    [field: SerializeField] public int MaxHp { get; private set; }
    [field: SerializeField] public int MaxEp { get; private set; }


    [Header("Moves")]
    [field: SerializeField] public List<LearnableMove> learnableMoves { get; private set; }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }

    // Enum for creature types
    public enum CreatureType
    {
        Fire,
        Water,
        Air
    }


    void getHealth()
    {

    }
}

// Nested MoveSet class with auto-implemented property
[System.Serializable]
public class LearnableMove
{
    [field: SerializeField] public MoveBase Base { get; private set; }
    [field: SerializeField] public int level { get; private set; }
}
