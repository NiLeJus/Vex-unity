using System.Collections.Generic;
using UnityEngine;
using static MoveBase;

public class Move
{
    public MoveBase Base { get; set; }
    public string Name { get; set; }
    public int PP { get; set; }
    private List<StatScaleEntry> statScales;
    private float flatDamages;

    #region // Constructors

    public Move(MoveBase moveBase)
    {
        Base = moveBase;
        Name = moveBase.Name;
        statScales = moveBase.statScales;
        flatDamages = moveBase.TotalFlatDamage;
    }
    #endregion

    public float ProcessDamages(HandlerStats statHandler)
    {
        float allDamages = flatDamages;
        float acc = 0f;

        Debug.Log($"[ProcessDamages] TotalFlatDamage (allDamages) = {allDamages}");

        foreach (StatScaleEntry entry in Base.statScales)
        {
            if (entry.scale > 0)
            {
                float statValue = statHandler.GetStatValue(entry.statName);
                float damageForEntry = (entry.scale + 1) * allDamages;

                Debug.Log(
                    $"[ProcessDamages] Stat: {entry.statName}, " +
                    $"Scale: {entry.scale}, " +
                    $"StatValue: {statValue}, " +
                    $"DamageForEntry: ({entry.scale}+1)*{allDamages} = {damageForEntry}"
                );

                acc += damageForEntry;
            }
            else
            {
                Debug.Log(
                    $"[ProcessDamages] Stat: {entry.statName} ignorée car scale <= 0 (scale={entry.scale})"
                );
            }
        }

        Debug.Log($"[ProcessDamages] Damage total retourné : {acc}");
        return acc + flatDamages;
    }


    public List<StatsName> UsedStats()
    {
        List<StatsName> toReturn = new List<StatsName>();

        foreach (StatScaleEntry entry in Base.statScales)
        {
            toReturn.Add(entry.statName);
        }

        return toReturn;
    }



    /// <summary>
    /// Récupère le scale pour un type de stat donné.
    /// </summary>
    public float GetScale(StatsName statName)
    {
        var entry = statScales.Find(e => e.statName == statName);
        return entry != null ? entry.scale : 0f;
    }
}
