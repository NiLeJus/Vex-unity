using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovesUI : MonoBehaviour
{
    [SerializeField] private GameObject moveButtonPrefab;
    [SerializeField] private Transform movesParent;
    private float damages;

    #region Singleton
    public static MovesUI I { get; private set; }
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
    }

    #endregion
    public void ShowMoves(Creature creature)
    {


        // Nettoyer l'ancien UI si besoin
        foreach (Transform child in movesParent)
            Destroy(child.gameObject);

        foreach (Move move in creature.Moves)
        {
            Debug.Log("Move trouvé : " + move.Name);
            GameObject buttonObj = Instantiate(moveButtonPrefab, movesParent);
            buttonObj.transform.Find("MoveNameLabelMP").GetComponent<TextMeshProUGUI>().text = move.Name;
            buttonObj.transform.Find("MoveDamagesMP").GetComponent<TextMeshProUGUI>().text = Damages(move, creature).ToString();


            // Capture la variable locale pour le delegate
            Move moveCopy = move;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => BattleHandler.i.PlayerUse(moveCopy));
        }
    }

    public float Damages(Move move, Creature creature)
    {
        return move.ProcessDamages(creature.Stats);
    }

}
