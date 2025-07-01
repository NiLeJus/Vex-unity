using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton & Persistance //
    public static GameManager I { get; private set; }
    public void SingletonImplementation()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnDestroy()
    {
        if (I == this) I = null;
    }
    #endregion

    #region Player Related //
    [SerializeField]
    public _BasePlayer _basePlayer;
    public Player Player;

    [SerializeField]
    public GameObject firstSpawnPoint;
    public Vector3 playerSpawnPosition;
    public GameObject PlayerGameObject;
    #endregion

    public NPC EnemyToBattle { get; private set; }
    public bool IsInMenu { get; private set; } = false;
    public void SetInMenuState(bool inMenu) => IsInMenu = inMenu;

    #region Menus Related // 

    #endregion

    void Awake()
    {
        SingletonImplementation();
        //DEBUG 
        if (_basePlayer == null)
            Debug.LogError("_basePlayer non assigné !");
        else if (_basePlayer.Creatures.Count == 0)
            Debug.LogError("_basePlayer.Creatures est vide !");
        //END DEBUG
        Player = new Player(_basePlayer);
    }


    #region Scene Change Triggers //
    public void SetBattleBetween(NPC _enemyToBattle)
    {

        EnemyToBattle = _enemyToBattle;
        GoToScene("CombatScene");
    }

    #endregion



    public void GotToLaunchScene()
    {
        // Firebase Deconexion
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignOut();

        SceneManager.LoadScene("LaunchScene"); //Move

        //Cleaning
        Destroy(gameObject);
    }

    #region Scene Handling //
    public void GoToScene(string sceneName)
    {
        // SavePlayer Position if in scene
        if (PlayerGameObject != null)
        {
            playerSpawnPosition = PlayerGameObject.transform.position;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private GameObject FindPlayer()
    {
        return GameObject.FindWithTag("Player");
    }

    #endregion


    public void Save(ref GameManagerSaveData data)
    {
        data.PlayerPosition = PlayerGameObject.transform.position;
        //data.Creatures = Player;
        //data.PlayerSaveData = Player.Save();
    }

    public void Load(GameManagerSaveData data)
    {
    }
}

[System.Serializable]
public struct GameManagerSaveData
{
    public Vector3 PlayerPosition;
    //public List<Creature> Creatures;
}

