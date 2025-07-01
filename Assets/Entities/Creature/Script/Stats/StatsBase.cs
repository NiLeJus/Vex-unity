using UnityEngine;

[System.Serializable]
public class StatBase
{

    [SerializeField] private StatsName statName;
    [SerializeField] private int baseValue = 0;
    [SerializeField] private int value = 0;
    [SerializeField] private float experience = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private int quality = 32;

    private float experiencePerLevel = 100f;

    public StatBase(string name, int baseValue)
    {
        this.baseValue = baseValue;
        this.value = baseValue;
        this.experience = 0;
        this.level = 1;
        this.quality = 0;
    }
    public string Name => statName.ToString();
    public StatsName StatType => statName;
    public int BaseValue => baseValue;
    public int Value => value;
    public float Experience => experience;
    public int Level => level;
    public int Quality => quality;
    public float ExperienceMax => experiencePerLevel;

    public void AddExperience(float amount)
    {
        experience += amount;
        while (experience >= experiencePerLevel)
        {
            experience -= experiencePerLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        value += baseValue + 120;
        experiencePerLevel *= 1.6f;
        Debug.Log($"{Name} a atteint le niveau {level} !");
    }

    public void ResetToBase()
    {
        value = baseValue;
        experience = 0;
        level = 1;
        experiencePerLevel = 100f;
    }
}
