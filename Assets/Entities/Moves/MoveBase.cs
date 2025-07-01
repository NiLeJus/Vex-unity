using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Use for creating new move Data set in the Unity Editor > inspector
/// Right click in project tab => right click => create => Vexplorer => new Move
/// </summary>
[CreateAssetMenu(fileName = "Move", menuName = "Creature/Create new Move")]
public class MoveBase : ScriptableObject
{
    // Commons
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }
    [field: SerializeField] public int Power { get; private set; }

    #region Stats Scales //
    [SerializeField] public List<StatScaleEntry> statScales;

    public Dictionary<StatsName, int> GetAllScales()
    {
        var dict = new Dictionary<StatsName, int>();
        foreach (var entry in statScales)
        {
            dict[entry.statName] = entry.scale;
        }
        return dict;
    }

    [Serializable]
    public class StatScaleEntry
    {
        public StatsName statName;
        public int scale;
    }

    #endregion

    #region Damages Related //

    [SerializeField] public List<DamageEntry> flatDamages;
    public Dictionary<eDamageTypeName, float> GetAllFlatDamages()
    {
        var dict = new Dictionary<eDamageTypeName, float>();
        foreach (var entry in flatDamages)
        {
            dict[entry.damageType] = entry.flatDamage;
        }
        return dict;
    }

    [System.Serializable]
    public class DamageEntry
    {
        public eDamageTypeName damageType;
        public float flatDamage;
    }

    #endregion

    public float TotalFlatDamage
    {
        get
        {
            float total = 0f;
            foreach (var entry in flatDamages)
            {
                total += entry.flatDamage;
            }
            return total;
        }
    }



    public float GetFlatDamage(eDamageTypeName type)
    {
        var entry = flatDamages.Find(e => e.damageType == type);
        return entry != null ? entry.flatDamage : 0f;
    }

    [field: SerializeField] public int EnergyConsumption { get; private set; }
    [field: SerializeField] public int AttackCount { get; private set; }

    [SerializeField] private List<Effect> appliedEffectsOnTarget;
    [SerializeField] private List<Effect> appliedEffectsOnSelf;
}

public enum eDamageTypeName
{
    Physical,
    Energy
}

