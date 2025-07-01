using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Not working anymore
/// implémentation => https://www.youtube.com/watch?v=0QU0yV0CYT4&t=980s&ab_channel=CodeMonkey
/// </summary>
public class BattleHandler : MonoBehaviour
{
    [Header("Player Data")]
    [SerializeField]
    //private _BasePlayer _player;
    private Player player;

    [Header("Player Data")]
    [SerializeField]
    //private _BasePlayer _enemyplayer;
    private IHaveCreatures enemyPlayer;

    [Header("Creature Data")]
    [SerializeField]
    private _CreatureBase _creatureBasePlayer;
    [SerializeField]
    private _CreatureBase _creatureBaseEnemy;

    [Header("Creature Prefab")]
    [SerializeField] private GameObject prefabCreature;

    [Header("Creature UI Displayer")]
    [SerializeField] private BattleHandler_UI battleHandler_UI;

    //Internal References
    private Creature playerCreature;
    private Creature enemyCreature;

    //Internal References
    private CreatureController playerCreatureController;
    private CreatureController enemyCreatureController;

    private CreatureBattler playerCreatureBattler;
    private CreatureBattler enemyCreatureBattler;
    private CreatureBattler activeCreatureBattler;

    [SerializeField]
    private BattleEndHandler battleEndManager;

    [SerializeField]
    private PlayerManager playerManager;

    //Keep a reference 
    List<Creature> creatures = new List<Creature>();

    #region Singletone_Implementation

    // Singleton
    public static BattleHandler i { get; private set; }

    private void InitSingleton()
    {
        if (i != null && i != this)
        {
            Destroy(gameObject);
            return;
        }
        i = this;
    }

    #endregion

    #region States //
    private State state;
    private enum State
    {
        WaitingForPlayer, Busy, WaitingForEnemy, Ended, Paused
    }
    #endregion

    #region Modality // 
    public Transform creaAPosition;
    public Transform creaBPosition;

    #endregion


    void Awake()
    {
        InitSingleton();
        InitFromGameManager();

        battleHandler_UI.SetCreaturesToDisplay(playerCreature, enemyCreature);
    }
    public void InitFromGameManager()
    {
        if (GameManager.I == null)
        {
            Debug.LogError("GameManager non initialisé !");
            return;
        }

        player = GameManager.I.Player;
        enemyPlayer = GameManager.I.EnemyToBattle;

        // Vérification du joueur
        if (player?.Creatures == null || player.Creatures.Count == 0)
        {
            Debug.LogError("Créatures du joueur non initialisées !");
            return;
        }

        // Vérification de l'ennemi
        if (enemyPlayer?.Creatures == null || enemyPlayer.Creatures.Count == 0)
        {
            Debug.LogError("Créatures de l'ennemi non initialisées !");
            return;
        }

        if (player.Creatures[0].Resources.Health.value <= 0)
        {
            playerCreature = player.Creatures[1];
        }
        else
        {
            playerCreature = player.Creatures[0];
        }
        enemyCreature = enemyPlayer.Creatures[0];
    }


    void Start()
    {
        battleHandler_UI.SetItemsToDisplay(player);

        playerCreatureController = SpawnCharacter(true);
        playerCreatureBattler = playerCreatureController.GetCreatureBattler();

        enemyCreatureController = SpawnCharacter(false);
        enemyCreatureBattler = enemyCreatureController.GetCreatureBattler();

        creatures.Add(playerCreature);
        creatures.Add(enemyCreature);

        SetActiveCreatureController(playerCreatureBattler);

        MovesUI.I.ShowMoves(playerCreature);


        #region Helper
        //foreach (Move move in playerCreature.Moves)
        //{
        //    Debug.Log("Move trouvé à BattleHandler " + move.Name);

        //}
        #endregion

        playerCreature.OnDeath += HandleCreatureDeath;
        enemyCreature.OnDeath += HandleCreatureDeath;

        state = State.WaitingForPlayer;
    }


    private Move nextPlayerMove;
    public void PlayerUse(Move move)
    {
        nextPlayerMove = move;
    }

    void Update()
    {

        if (state == State.Paused)
        {

        }
        if (state == State.Ended)
        {
            HandleCreatureDeath();
            state = State.Paused;
            Debug.Log("Ended");
        }
        if (state != State.Ended)
        {

            if (state == State.WaitingForPlayer)
            {
                if (playerCreature.Resources.Health.value <= 0)
                {
                    state = State.Ended;
                }

                //Debug.Log(state);
                if (nextPlayerMove != null)
                {
                    state = State.Busy;

                    playerCreatureBattler.Attack(enemyCreatureBattler, nextPlayerMove, () =>
                    {
                        if (state == State.Busy)
                        {
                            state = State.WaitingForEnemy;
                        }
                    });
                    nextPlayerMove = null;
                }
            }
            if (state == State.WaitingForEnemy)
            {
                if (enemyCreature.Resources.Health.value <= 0)
                {

                    state = State.Ended;
                }

                state = State.Busy;
                enemyCreatureBattler.Attack(playerCreatureBattler, NextEnemyMove(), () =>
                {
                    if (state == State.Busy)
                    {
                        state = State.WaitingForPlayer;
                    }
                });
            }
        }
    }


    private Move NextEnemyMove()
    {
        if (enemyCreature.Moves == null || enemyCreature.Moves.Count == 0)
        {
            Debug.LogWarning("No Move on enemy");
            return null;
        }

        int index = UnityEngine.Random.Range(0, enemyCreature.Moves.Count);
        return enemyCreature.Moves[index];
    }


    /// <summary>
    /// To spawn a Creature in the world
    /// </summary>
    /// <param name="isPlayer"></param>
    /// <returns></returns>
    private CreatureController SpawnCharacter(bool isPlayer)
    {
        Creature creatureData = isPlayer ? playerCreature : enemyCreature;
        Transform spawnPoint = isPlayer ? creaAPosition : creaBPosition;
        return CreatureController.InstantiateCreature(
          creatureData,
          spawnPoint,
          prefabCreature
        );
    }

    private void SetActiveCreatureController(CreatureBattler creatureBattler = null)
    {
        if (creatureBattler != null)
        {
            activeCreatureBattler = creatureBattler;
        }

        switch (activeCreatureBattler)
        {
            case var battler when battler == enemyCreatureBattler:
                activeCreatureBattler = playerCreatureBattler;
                break;
            case var battler when battler == playerCreatureBattler:
                activeCreatureBattler = enemyCreatureBattler;
                break;
            default:
                Debug.LogWarning("Edge case when it should not", this);
                break;
        }
    }

    public void Flee()
    {
        CombatEnd();
    }

    public void CombatEnd()
    {

    }

    private void HandleCreatureDeath()
    {
        this.state = State.Ended;
        StartCoroutine(HandleCreatureDeathCoroutine());
    }

    private IEnumerator HandleCreatureDeathCoroutine()
    {

        if (playerCreature.Health <= 0)
        {
            playerCreatureController.TriggerDeath();
        }
        else
        {
            enemyCreatureController.TriggerDeath();
        }

        yield return new WaitForSeconds(2f);
        battleEndManager.TriggerEnd(playerCreature, enemyCreature, true, player, enemyPlayer, 0, 0);

    }
}
