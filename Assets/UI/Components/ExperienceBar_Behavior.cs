using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar_Behavior : MonoBehaviour
{

    [SerializeField] private Slider slider;

    private void Awake()
    {
    }

    public void UpdateBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    void Update()
    {

    }
}
