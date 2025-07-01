using TMPro;
using UnityEngine;

public class ItemsUI : MonoBehaviour
{
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private Transform itemsParent;
    private float damages;

    #region Singleton
    public static ItemsUI i { get; private set; }
    private void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(gameObject);
            return;
        }
        i = this;
    }

    #endregion
    public void ShowItems(Player player)
    {


        // Nettoyer l'ancien UI si besoin
        foreach (Transform child in itemsParent)
            Destroy(child.gameObject);

        foreach (_BaseItem item in player.Items)
        {
            Debug.Log("Item Found: " + item.itemName);
            GameObject buttonObj = Instantiate(itemButtonPrefab, itemsParent);
            buttonObj.transform.Find("ItemNameLabelMP").GetComponent<TextMeshProUGUI>().text = item.itemName;

            // Capture la variable locale pour le delegate
            _BaseItem itemCopy = item;
            //buttonObj.GetComponent<Button>().onClick.AddListener(() => BattleHandler.I.PlayerUseItem(itemCopy));
        }
    }

    public float Damages(Move move, Creature creature)
    {
        return move.ProcessDamages(creature.Stats);
    }
}
