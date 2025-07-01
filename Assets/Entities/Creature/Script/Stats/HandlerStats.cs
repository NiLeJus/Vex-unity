using System;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Abstract Stats Managing Logic
/// </summary>
[System.Serializable]
public class HandlerStats
{
    [SerializeField] private StatBase physic;
    [SerializeField] private StatBase strength;
    [SerializeField] private StatBase dexterity;
    [SerializeField] private StatBase agility;
    [SerializeField] private StatBase focus;
    [SerializeField] private StatBase intel;

    public StatBase Strength => strength;
    public StatBase Dexterity => dexterity;
    public StatBase Intel => intel;
    public StatBase Agility => agility;
    public StatBase Physic => physic;
    public StatBase Focus => focus;


    public Action OnStatLevelChanged;

    #region Actions Events // 

    #endregion

    public HandlerStats(
    StatBase _physic,
    StatBase _strength,
    StatBase _agility,
    StatBase _dexterity,
    StatBase _focus,
    StatBase _intel)
    {
        physic = _physic;
        strength = _strength;
        agility = _agility;
        dexterity = _dexterity;
        focus = _focus;
        intel = _intel;

    }

    // Pour accès par enum si besoin
    public StatBase GetStat(StatsName stat)
    {
        switch (stat)
        {
            case StatsName.Physic: return physic;
            case StatsName.Strength: return strength;
            case StatsName.Agility: return agility;
            case StatsName.Dexterity: return dexterity;
            case StatsName.Focus: return focus;
            case StatsName.Intel: return intel;
            default: return null;
        }
    }

    public float GetStatValue(StatsName stat)
    {
        switch (stat)
        {
            case StatsName.Physic: return physic.Value;
            case StatsName.Strength: return strength.Value;
            case StatsName.Agility: return agility.Value;
            case StatsName.Dexterity: return dexterity.Value;
            case StatsName.Focus: return focus.Value;
            case StatsName.Intel: return intel.Value;
            default:
                Debug.LogWarning($"Stat inconnu : {stat}");
                return 0f;
        }
    }


    public List<StatBase> GetStats(params StatsName[] stats)
    {

        List<StatBase> payload = new List<StatBase>();

        foreach (var wantedStat in stats)
        {

            StatBase foundStat = wantedStat switch
            {
                StatsName.Physic => physic,
                StatsName.Strength => strength,
                StatsName.Agility => agility,
                StatsName.Dexterity => dexterity,
                StatsName.Focus => focus,
                StatsName.Intel => intel,
                _ => null
            };

            if (foundStat != null)
            {
                payload.Add(foundStat);
            }
        }

        return payload;
    }


    public StatBase[] GetAllStatsArray()
    {
        return new StatBase[] { physic, strength, agility, dexterity, focus, intel };
    }


    public int[] GetStatsValues(params StatsName[] stats)
    {
        int[] results = new int[stats.Length];
        for (int i = 0; i < stats.Length; i++)
        {
            results[i] = GetStat(stats[i])?.Value ?? 0;
        }
        return results;
    }

    public int GetAllLevelsAmount()
    {
        int total = 0;

        // Vérification de null pour chaque stat avant accumulation
        if (strength != null) total += strength.Level;
        if (dexterity != null) total += dexterity.Level;
        if (intel != null) total += intel.Level;
        if (agility != null) total += agility.Level;
        if (physic != null) total += physic.Level;
        if (focus != null) total += focus.Level;

        return total;
    }

    public void AddExperienceToStat(StatsName statName, float experience)
    {
        switch (statName)
        {
            case StatsName.Strength:
                strength.AddExperience(experience);
                break;
            case StatsName.Dexterity:
                dexterity.AddExperience(experience);
                break;
            case StatsName.Intel:
                intel.AddExperience(experience);
                break;
            case StatsName.Agility:
                agility.AddExperience(experience);
                break;
            case StatsName.Physic:
                physic.AddExperience(experience);
                break;
            case StatsName.Focus:
                focus.AddExperience(experience);
                break;
            default:
                throw new ArgumentException($"Statistique inconnue : {statName}");
        }
    }

    public int GetAllQualitiesAmount()
    {
        int acc = 0;

        //foreach (var stat in stats)
        //{
        //    if (stat != null)
        //        Debug.Log("acc is ${acc}");
        //    Debug.Log(stat.Name);
        //    acc += stat.Quality;
        //}
        return acc;
    }

    // Réinitialise toutes les statistiques à leurs valeurs de base
    public void ResetAllStats()
    {

    }
}
