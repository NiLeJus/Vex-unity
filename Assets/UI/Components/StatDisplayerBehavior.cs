using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class StatDisplayerBehavior : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI statNameTMP;
    [SerializeField]
    private TextMeshProUGUI statLevelTMP;
    [SerializeField]
    private TextMeshProUGUI statValueTMP;

    [SerializeField]
    private Slider xpSlider;

    [SerializeField]
    private ExperienceBar_Behavior xpBar;

    public void Setup(StatBase stat)
    {
        statNameTMP.text = stat.Name;
        Debug.Log(stat.Name);
        statValueTMP.text = stat.Value.ToString();
        statLevelTMP.text = stat.Level.ToString();

        xpBar.UpdateBar(stat.Experience, stat.ExperienceMax);

    }
    // Update is called once per frame
    void Update()
    {

    }


}
