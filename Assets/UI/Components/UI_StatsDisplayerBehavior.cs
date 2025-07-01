using UnityEngine;

public class UI_StatsDisplayerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject pfStatDisplayerUI;
    [SerializeField] private Transform statsContainer;
    public void ShowUI(HandlerStats handlerStats)
    {
        ClearUI();

        foreach (StatBase stat in handlerStats.GetAllStatsArray())
        {
            GameObject statInstance = Instantiate(pfStatDisplayerUI, statsContainer);
            StatDisplayerBehavior statUI = statInstance.GetComponent<StatDisplayerBehavior>();
            statUI.Setup(stat);
        }
    }

    private void ClearUI()
    {
        foreach (Transform child in statsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
