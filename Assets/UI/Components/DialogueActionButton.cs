using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelText;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (labelText == null)
            labelText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(string label, UnityEngine.Events.UnityAction onClick)
    {
        labelText.text = label;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onClick);
    }
}
