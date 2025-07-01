using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_CompanionsDisplayer : MonoBehaviour
{

    [SerializeField]
    private GameObject uiCreatureDisplayer;

    [SerializeField]
    private Transform companionResultsDisplayer;



    [SerializeField]
    private List<GameObject> displayedCreature;

    void Start()
    {
        Setup();
        StartCoroutine(SetupLoop());
    }

    IEnumerator SetupLoop()
    {
        while (true)
        {
            Setup();
            yield return new WaitForSeconds(1.5f);
        }
    }
    private void ClearResults()
    {
        foreach (GameObject go in displayedCreature)
        { Destroy(go); }

        displayedCreature.Clear();
    }
    public void Setup()
    {
        ClearResults();
        List<Creature> creatureResults = GameManager.I.Player.Creatures;

        foreach (Creature creature in creatureResults)
        {
            GameObject go = Instantiate(uiCreatureDisplayer, companionResultsDisplayer);
            UI_CreatureDisplayer creatureDisplayer = go.GetComponent<UI_CreatureDisplayer>();
            creatureDisplayer.SetCreature(creature);

            displayedCreature.Add(go);

        }
    }
}
