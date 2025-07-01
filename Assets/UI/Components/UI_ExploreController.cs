using System.Collections.Generic;
using Firebase.Auth;
using TMPro;
using UnityEngine;

public class UI_ExploreController : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> usernameDisplayers;
    public string Username => FirebaseAuth.DefaultInstance.CurrentUser?.DisplayName;

    [SerializeField]
    private GameObject uiPauseMenu;
    [SerializeField]
    private GameObject uiPlayerMenu;

    [SerializeField]
    private Ui_CompanionsDisplayer companionDisplayer;


    private GameObject activeUiMenu;

    private void Awake()
    {
        foreach (var usernameDisplayer in usernameDisplayers)
        {
            usernameDisplayer.text = Username;
        }
    }

    private void Start()
    {

        InitializeUI();
    }

    private void InitializeUI()
    {
        uiPauseMenu.SetActive(false);
        uiPlayerMenu.SetActive(false);
        activeUiMenu = null;
        GameManager.I?.SetInMenuState(false);
        companionDisplayer.Setup();
    }

    public void TogglePauseMenu()
    {
        bool newState = !uiPauseMenu.activeSelf;

        if (newState)
        {
            CloseCurrentMenu();
            OpenMenu(uiPauseMenu);
        }
        else
        {
            CloseCurrentMenu();
        }

        GameManager.I.SetInMenuState(newState);
        // Time.timeScale = newState ? 0f : 1f;
    }

    public void OpenPlayerMenu()
    {
        if (activeUiMenu == uiPlayerMenu)
        {
            CloseAllMenus();
            return;
        }

        CloseCurrentMenu();
        OpenMenu(uiPlayerMenu);
        GameManager.I.SetInMenuState(true);
    }

    private void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        activeUiMenu = menu;
    }

    private void CloseCurrentMenu()
    {
        if (activeUiMenu != null)
        {
            activeUiMenu.SetActive(false);
            activeUiMenu = null;
        }
        GameManager.I.SetInMenuState(false);
    }

    public void SetActiveUiMenu(GameObject menuToSet)
    {
        if (activeUiMenu != null && activeUiMenu != menuToSet)
        {
            activeUiMenu.SetActive(false);
        }

        activeUiMenu = menuToSet;

        if (menuToSet != null)
        {
            menuToSet.SetActive(true);
            GameManager.I.SetInMenuState(true);
        }
        else
        {
            GameManager.I.SetInMenuState(false);
        }
    }

    public void CloseAllMenus()
    {
        CloseCurrentMenu();
        uiPlayerMenu.SetActive(false);
        uiPauseMenu.SetActive(false);
    }
}
