using Firebase.Auth;
using TMPro;
using UnityEngine;

public class UI_PlayerDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI usernameDisplayer;
    [SerializeField]
    private TextMeshProUGUI moneyDisplayer;
    [SerializeField]
    private TextMeshProUGUI rankPointsDisplayer;


    public string Username => FirebaseAuth.DefaultInstance.CurrentUser?.DisplayName;

    private void Awake()
    {
        usernameDisplayer.text = Username;
        moneyDisplayer.text = GameManager.I.Player.Money.ToString();
        moneyDisplayer.text = GameManager.I.Player.Level.ToString();
    }

    public void Setup()
    {

    }

}
