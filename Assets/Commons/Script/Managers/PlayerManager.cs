using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager I { get; private set; }
    private void SingletonImplementation()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
    }

    #endregion

    public Player player;
    public Player enemyPlayer;

    void Awake()
    {
        //SingletonImplementation();
        //Player = new Player(playerBase);
        //enemyPlayer = new Player(enemyPlayerBase);
    }

    public void SetPlayers(Player playerToSet, Player enemyPlayerToSet)
    {
        player = playerToSet;
        enemyPlayer = enemyPlayerToSet;
    }

    public void Setup()
    {

    }
}
