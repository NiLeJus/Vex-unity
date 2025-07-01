using UnityEngine;

public class GameAssets : MonoBehaviour
{
    // Singleton
    public static GameAssets i { get; private set; }

    [SerializeField] public Transform pfChatBubble;

    // Ajoute ces champs pour référencer tes assets
    [Header("Dossiers d'assets")]
    [SerializeField] private _BasePlayer[] basePlayers;
    [SerializeField] private _CreatureBase[] creatureBases;

    // Accesseurs publics si besoin
    public _BasePlayer[] BasePlayers => basePlayers;
    public _CreatureBase[] CreatureBases => creatureBases;

    private void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(gameObject);
            return;
        }
        i = this;
    }
}
