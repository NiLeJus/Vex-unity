using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreatureDisplayer : MonoBehaviour
{
    [SerializeField] private IndicatorsUI indicatorsUI;
    private Creature creature;
    [SerializeField] private TextMeshProUGUI nameDisplayer;
    [SerializeField] private UI_StatsDisplayerBehavior statsDisplayer;
    [SerializeField] private RawImage creaturePortrait;
    [SerializeField] private TextMeshProUGUI healthDisplayer;

    void Setup()
    {
        if (indicatorsUI != null && creature != null)
            indicatorsUI.ShowIdc(creature);
        else
            Debug.LogError("Composants manquants !", this);

        nameDisplayer.SetText(creature.Name);
        nameDisplayer.ForceMeshUpdate();
        statsDisplayer.ShowUI(creature.Stats);
        creaturePortrait.texture = creature.portrait;

        healthDisplayer.text = ProcessHealthAmount();
        if (creature.Health > 0)
        {
            healthDisplayer.color = Color.green;
        }
        else
        {
            healthDisplayer.color = Color.red;
        }

    }
    public void Start()
    {
    }

    private string ProcessHealthAmount()
    {
        ResourceBase healthResource = creature.Resources.Health;
        return string.Concat(healthResource.Value + " / " + healthResource.MaxValue);
    }
    public void SetCreature(Creature creatureToDisplay)
    {
        creature = creatureToDisplay;
        Setup();
    }
}

