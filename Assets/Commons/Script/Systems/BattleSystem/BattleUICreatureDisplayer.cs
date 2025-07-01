using System.Collections;
using TMPro;
using UnityEngine;

public class BattleUICreatureDisplayer : MonoBehaviour
{
    [SerializeField] private IndicatorsUI indicatorsUI;
    private Creature creature;

    [SerializeField]
    private TextMeshProUGUI nameDisplayer;

    [SerializeField]
    private TextMeshProUGUI healthDisplayer;


    void Setup()
    {
        if (indicatorsUI != null && creature != null)
            indicatorsUI.ShowIdc(creature);
        else
            Debug.LogError("Composants manquants !", this);

        nameDisplayer.SetText(creature.Name);
        nameDisplayer.ForceMeshUpdate();

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        string healthMax = creature.Resources.Health.MaxValue.ToString();
        string health = creature.Resources.Health.Value.ToString();
        healthDisplayer.SetText(string.Concat(health, " / ", healthMax));
    }

    public void Start()
    {
        StartCoroutine(LifeUpdate());
    }

    IEnumerator LifeUpdate()
    {
        while (true)
        {
            UpdateHealthBar();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void SetCreature(Creature creatureToDisplay)
    {
        creature = creatureToDisplay;
        Setup();
    }
}
