using TMPro;
using UnityEngine;

public class UI_RewardDisplayer : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI moneyDisplayer;
    [SerializeField]
    public TextMeshProUGUI experienceDisplayer;
    [SerializeField]
    public TextMeshProUGUI rankDisplayer;

    public void Setup(float money, float bonusExperience, float rank)
    {
        moneyDisplayer.text = money.ToString();
        experienceDisplayer.text = bonusExperience.ToString();
        rankDisplayer.text = rank.ToString();
    }
}
