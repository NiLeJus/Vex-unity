using System.Collections.Generic;
using UnityEngine;

public class IndicatorsUI : MonoBehaviour
{
    [SerializeField] private GameObject idcDisplayerPrefab;
    [SerializeField] private Transform idcParent;

    public void ShowIdc(Creature creature)
    {
        ClearExistingIndicators();
        Debug.Log($"Product of Creature.GetIndicators() {creature.GetIndicators()}");
        foreach (KeyValuePair<string, int> entry in creature.GetIndicators())
        {
            GameObject indicatorObj = Instantiate(idcDisplayerPrefab, idcParent);
            IndicatorDisplayer displayer = indicatorObj.GetComponent<IndicatorDisplayer>();

            if (displayer != null)
                displayer.Setup(entry.Key, entry.Value);
            else
                Debug.LogError("Prefab n'a pas de composant IndicatorDisplayer !");
        }
    }

    private void ClearExistingIndicators()
    {
        foreach (Transform child in idcParent)
            Destroy(child.gameObject);
    }
}
