using System.Collections.Generic;
using UnityEngine;



//[RequireComponent(typeof(CreatureBattler))] //Garanti presence du composent 

/// <summary>
/// Class that take in a _CreatureBase to use it's data
///! Important is not Monobehavior and should not be !
/// Other logics (like battle) should be implemented elsewhere (a handler).
/// </summary>


[System.Serializable]
public class Creature
{
    [SerializeField] public _CreatureBase Base { get; set; }
    public Texture2D portrait => Base.portrait;

    #region Forwarding Base Attributes //
    public string Name => Base.Name;
    public int Worth => Base.Worth;
    public int Rarity => Base.Rarity;
    public int level = 0;
    #endregion

    #region Declaration Abstraction Handlers //
    private HandlerIndicators handlerIndicators;
    private HandlerResources handlerResources;
    private HandlerStats handlerStats;
    private HandlerEffects handlerEffects;

    public HandlerIndicators Indicators => handlerIndicators;
    public HandlerResources Resources => handlerResources;
    public HandlerStats Stats => handlerStats;
    //public HandlerEffects Effects => handlerEffects;
    #endregion

    #region Actions Events //

    public event System.Action OnDeath;
    private void HandleCreatureDeath()
    {
        creatureState = eCreatureState.Dead;
        OnDeath?.Invoke();
    }

    #endregion

    #region Controller //
    public CreatureBattler Battler { get; private set; }
    #endregion

    public GameObject _creatureVisualPrefab { get; private set; }
    public List<Move> Moves { get; private set; }
    public eCreatureState creatureState = eCreatureState.Alive;

    #region Constructors //

    public Creature(_CreatureBase _CreatureBase)
    {
        //Get base stuff
        Base = _CreatureBase;
        _creatureVisualPrefab = Base.pfCreatureVisual;

        //Handler Stat implementation
        handlerStats = new HandlerStats(
        Base.Physic,
        Base.Strength,
        Base.Agility,
        Base.Dexterity,
        Base.Focus,
        Base.Intel);
        handlerStats.OnStatLevelChanged += HandleStatLevelChanged;


        handlerResources = new HandlerResources(handlerStats);
        handlerResources.OnHealthDepleted += HandleCreatureDeath;


        handlerIndicators = new HandlerIndicators(handlerStats);

        //portrait = PortraitGenerator.GeneratePortrait(_creatureVisualPrefab);

        //Generate the moves
        Moves = new List<Move>();

        if (Base.LearnableMoves == null)
        {
            Debug.LogError("LearnableMoves est null pour " + Base.Name);
            return;
        }

        foreach (LearnableMove move in Base.LearnableMoves)
        {
            Moves.Add(new Move(move.Base));

            //if (move.level <= level)
            //{
            //}
        }
    }
    #endregion

    #region Combat related //
    public void TakeDamages()
    {

    }
    public float Attack(Move moveToUse)
    {
        Debug.Log($"Has used {moveToUse.Name}");
        float calculatedDamages = moveToUse.ProcessDamages(handlerStats);
        return calculatedDamages;
    }
    public void Damage(float damage)
    {
        handlerResources.SubAmountToRessource(eResourcesName.Health, damage);
    }
    #endregion

    public float Health
    {
        get => handlerResources.Health.Value;
        set => handlerResources.Health.value = value;
    }


    public GameObject GetVisualPrefab()
    {
        return _creatureVisualPrefab;
    }
    public void TurnPassed()
    {

    }
    private void Start()
    {
        SpawnCreature();
    }
    public void AddEffect(IEffect effect)
    {

    }
    public void PassExperience(StatsName primaryStats, int experienceAmount)
    {
        handlerStats.AddExperienceToStat(primaryStats, experienceAmount);
    }
    public int GetStrength()
    {
        return handlerStats.Strength.Value;
    }

    public int GetStats()
    {
        return handlerStats.Strength.Value;
    }

    public List<int> GetQualities()
    {
        return new List<int> { 80, 90, 75 };
    }

    public List<KeyValuePair<string, int>> GetIndicators()
    {
        //Debug.Log(handlerStats.GetAllLevelsAmount());
        //Debug.Log(handlerStats.GetAllQualitiesAmount());
        return handlerIndicators.GetIndicators(GetQualities(), handlerStats.GetAllLevelsAmount(), handlerStats.GetAllLevelsAmount(), Rarity, Worth);
    }

    //Maybe needed to init ressources
    void InitializeStats()
    {
        //Base.HandlerResources.RollResources(handlerStats);
    }
    private void HandleStatLevelChanged()
    {
        Debug.Log($"le niveau de stat a up !");
    }

    public void SpawnCreature()
    {
        //GameObject instance = Instantiate(Base.pfCreatureVisual, this.transform.position, transform.rotation);
        //instance.transform.SetParent(transform);
    }

    //Handle 
    public void Cleanup()
    {
        handlerResources.OnHealthDepleted -= HandleCreatureDeath;
    }

}

[System.Serializable]
public struct CreatureSaveData
{
    public float HealthValue;
    //public HandlerSaveValue

}

