using TMPro;
using UnityEngine;

public class BattleEndHandler : MonoBehaviour
{

    [SerializeField]
    public UI_CreatureDisplayer playerCreaturestatsDisplayer;

    [SerializeField]
    public UI_CreatureDisplayer enemyCreaturestatsDisplayer;

    [SerializeField]
    public TextMeshProUGUI matchStatusDisplayer;

    [SerializeField]
    public UI_RewardDisplayer uiRewardDisplayer;

    public bool isActive = false;

    [SerializeField]
    private GameObject combatEndedCanvas;

    public void TriggerEnd(Creature playerCreature, Creature enemyCreature, bool isPlayerVictorious, Player player, IHaveCreatures enemyPlayer, int enemyCreatureXpAcc, int playerCreatureXpAcc)
    {
        BattleResults results = BattleResult(playerCreature, enemyCreature, isPlayerVictorious, player, enemyPlayer, enemyCreatureXpAcc, playerCreatureXpAcc);
        float rank = 0;
        if (isPlayerVictorious)
        {
            rank = 10;
        }

        else
        {
            rank = 1;
        }

        GameManager.I.Player.Money = (int)results.Money;
        GameManager.I.Player.Level = (int)rank;

        uiRewardDisplayer.Setup(results.Money, results.BonusExperience, rank);

        combatEndedCanvas.SetActive(true);
        playerCreaturestatsDisplayer.SetCreature(playerCreature);
        enemyCreaturestatsDisplayer.SetCreature(enemyCreature);

        if (isPlayerVictorious == true)
        {
            matchStatusDisplayer.text = "Victory !";
            matchStatusDisplayer.color = Color.yellow;
        }
        else
        {
            matchStatusDisplayer.text = "Loooser !";
            matchStatusDisplayer.color = Color.blue;

        }

    }

    public void OnContinue()
    {
        GameManager.I.GoToScene("ExploreScene");
    }


    public BattleResults BattleResult(Creature playerCreature, Creature enemyCreature, bool isPlayerVictorious, Player player, IHaveCreatures enemyPlayer, int enemyCreatureXpAcc, int playerCreatureXpAcc)
    {
        float money;
        float bonusExperience;

        if (isPlayerVictorious)
        {
            money = (27f);
            bonusExperience = 20f;
        }
        else
        {
            money = (5f);
            //money = (5f) * enemyPlayer.Level;
            bonusExperience = 0;
        }
        return new BattleResults(money, bonusExperience);
    }
}

public class BattleResults
{

    public float Money { get; private set; }
    public float BonusExperience { get; private set; }

    public BattleResults(float money, float bonusExperience)
    {
        this.Money = money;
        this.BonusExperience = bonusExperience;
    }


}
