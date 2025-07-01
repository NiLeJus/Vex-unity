using UnityEngine;
using UnityEngine.UI;

public class TabsManager : MonoBehaviour
{

    public GameObject[] tabs;
    public Button[] tabButtons;


    void Start()
    {

    }
    public void SwitchToTab(int tabID)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i] != null)
            {
                tabs[i].SetActive(false);
            }
        }

        if (tabID >= 0 && tabID < tabs.Length && tabs[tabID] != null)
        {
            tabs[tabID].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Tab index out of range or tab destroyed: " + tabID);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
