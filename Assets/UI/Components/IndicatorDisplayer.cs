using TMPro;
using UnityEngine;

public class IndicatorDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI idcLabelTextMP;

    [SerializeField]
    private TextMeshProUGUI idcValueTextMP;





    private void Awake()
    {
        idcValueTextMP = transform.Find("IdcValueTextMP").GetComponent<TextMeshProUGUI>();
        idcLabelTextMP = transform.Find("IdcLabelTextMP").GetComponent<TextMeshProUGUI>();

        // Vérification de sécurité
        if (idcValueTextMP == null)
            Debug.LogError("IdcValueTextMP non trouvé !", this);
        if (idcLabelTextMP == null)
            Debug.LogError("IdcLabelTextMP non trouvé !", this);
    }

    public void Setup(string idcKey, float value)
    {
        if (idcLabelTextMP != null && idcValueTextMP != null)
        {
            Debug.Log($"{idcKey} : {value}");
            idcLabelTextMP.SetText(idcKey.ToUpper());
            idcValueTextMP.SetText(value.ToString());
            idcLabelTextMP.color = PickColor(idcKey.ToUpper());
        }
        else
        {
            Debug.LogError("Composants TextMeshPro manquants !", this);
        }
    }

    public Color PickColor(string idcKey)
    {
        string hexColor;
        switch (idcKey)
        {
            case "POW":
                hexColor = "#FF0000";
                break;
            case "LVL":
                hexColor = "#00FF00";
                break;
            case "RAR":
                hexColor = "#0000FF";
                break;
            case "QUA":
                hexColor = "#FFFF00";
                break;
            case "VAL":
                hexColor = "#FF00FF";
                break;
            default:
                hexColor = "#FFFFFF";
                break;
        }

        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
            return color;
        else
            return Color.white;
    }
}

