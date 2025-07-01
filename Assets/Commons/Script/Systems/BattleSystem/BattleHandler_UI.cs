using UnityEngine;

public class BattleHandler_UI : MonoBehaviour
{

    [SerializeField]
    public ItemsUI itemsUI;

    [SerializeField]
    public BattleUICreatureDisplayer enemyUIDisplayer;

    [SerializeField]
    BattleUICreatureDisplayer playerUIDisplayer;

    private void Awake()
    {
    }

    public void SetItemsToDisplay(Player player)
    {
        itemsUI.ShowItems(player);
    }
    public void SetCreaturesToDisplay(Creature playerCreature, Creature enemyCreature)
    {
        enemyUIDisplayer.SetCreature(enemyCreature);
        playerUIDisplayer.SetCreature(playerCreature);
    }

    void Update()
    {

    }
}
