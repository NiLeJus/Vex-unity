using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]



/// <summary>
/// Abstract Resources Logic
/// https://www.notion.so/Resources-1ac8661f58da8072bdd0cf1100667319?pvs=4
/// </summary>
public class HandlerResources
{

    #region Events // 

    //That event is made to notify it's related Creature class 
    public event System.Action OnHealthDepleted;
    #endregion

    #region Ressource Related

    private ResourceBase health;
    private ResourceBase energy;
    private ResourceBase integrity;

    private List<ResourceBase> allRessources;


    // Propriétés exposées pour accéder directement aux ressources
    public ResourceBase Health => health;
    public ResourceBase Energy => energy;
    public ResourceBase Integrity => integrity;
    private HandlerStats _handlerStats;

    #endregion

    #region Constructors
    public HandlerResources(HandlerStats handlerStats)
    {
        _handlerStats = handlerStats;

        health = new ResourceBase(eResourcesName.Health, maxHp(), maxHp());
        energy = new ResourceBase(eResourcesName.Energy, maxEp(), maxEp());
        integrity = new ResourceBase(eResourcesName.Integrity, maxIt(), maxIt());

        Debug.LogWarning(health);
        Debug.LogWarning(energy);
        Debug.LogWarning(integrity);
    }
    #endregion

    #region Calculate Ressources Value //
    private float maxHp() { return (_handlerStats.Physic.Value * 1.5f) + (_handlerStats.Strength.Value) * 10 + 10; }
    private float maxEp() { return (_handlerStats.Focus.Value * 1.5f) + (_handlerStats.Intel.Value) * 10; }
    private float maxIt() => 100;
    #endregion

    private void Initialize()
    {
        // Initialiser la liste avec toutes les ressources
        allRessources = new List<ResourceBase> { health, energy, integrity };
    }


    #region Manipulate Ressources //

    // Méthode pour ajouter une quantité à une ressource spécifique
    private void _AddAmountToRessource(eResourcesName ressourceName, int amount)
    {
        switch (ressourceName)
        {
            case eResourcesName.Health:
                health.AddAmount(amount);
                break;
            case eResourcesName.Energy:
                energy.AddAmount(amount);
                break;
            case eResourcesName.Integrity:
                integrity.AddAmount(amount);
                break;

            default:
                throw new ArgumentException($"Statistique inconnue : {ressourceName}");
        }
    }

    void AddAmountToRessource(KeyValuePair<eResourcesName, int>[] toProcess)
    {
        foreach (var entry in toProcess)
        {
            _AddAmountToRessource(entry.Key, entry.Value);
        }
    }

    public void SubAmountToRessource(eResourcesName ressourceName, float amount)
    {

        Debug.LogWarning(ressourceName + ", sub  " + amount);

        switch (ressourceName)
        {
            case eResourcesName.Health:

                health.SubAmount(amount);
                break;
            case eResourcesName.Energy:

                energy.SubAmount(amount);
                break;
            case eResourcesName.Integrity:

                integrity.SubAmount(amount);
                break;

            default:
                throw new ArgumentException($"Statistique inconnue : {ressourceName}");
        }

        if (ressourceName == eResourcesName.Health && GetResourceValue(ressourceName) <= 0)
        {
            OnHealthDepleted?.Invoke();
        }
    }

    public void ResetRessourceToMax(eResourcesName resourceName, int amount)
    {
        switch (resourceName)
        {
            case eResourcesName.Health:
                health.ResetToMax();
                break;
            case eResourcesName.Energy:
                energy.ResetToMax();
                break;
            case eResourcesName.Integrity:
                integrity.ResetToMax();
                break;
            default:
                throw new ArgumentException($"Unknown Stat: {resourceName}");
        }
    }
    #endregion

    public void RollResources(HandlerStats primaryStatsHolder)
    {
        if (primaryStatsHolder == null)
        {
            Debug.LogError("HandlerStats is null in RollResources!");
            return;
        }
        //health.SetNewMaxAndBaseValue(healthNewValue);
        //energy.SetNewMaxAndBaseValue(energyNewValue);
        //integrity.SetNewMaxAndBaseValue(99);
    }
    private float Calculate(Func<int> calculationMethod)
    {
        return calculationMethod();
    }
    public void ResetAllResourcesToMax()
    {
        foreach (var ressource in allRessources)
        {
            ressource.ResetToMax();
        }
        Debug.Log("Toutes les ressources ont été réinitialisées à leurs valeurs maximales.");
    }
    public bool CheckIfDepleted(eResourcesName resourceName)
    {
        if (resourceName == eResourcesName.Health && GetResourceValue(resourceName) <= 0)
        {
            return true;
        }
        return false;
    }
    public float GetResourceValue(eResourcesName resourceName)
    {
        switch (resourceName)
        {
            case eResourcesName.Health:
                return health.value;
            case eResourcesName.Energy:
                return energy.value;
            case eResourcesName.Integrity:
                return integrity.value;
            default:
                throw new ArgumentException($"Unknown Stat: {resourceName}");
        }
    }
}
