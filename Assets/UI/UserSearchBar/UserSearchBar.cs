using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using TMPro;
using UnityEngine;

public class UserSearchBar : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField searchInput;

    [SerializeField]
    public Transform resultsDisplayer;
    public GameObject uiUserDisplayer;

    private FirebaseFirestore db;
    private List<GameObject> spawnedResults = new List<GameObject>();
    private HashSet<string> displayedUsernames = new HashSet<string>(); // Pour éviter les doublons

    private string currentUserId;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        searchInput.onValueChanged.AddListener(OnSearchChanged);
        currentUserId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        SearchUsers(string.Empty);
    }

    private void OnSearchChanged(string searchText)
    {
        SearchUsers(searchText);
    }

    private void ClearResults()
    {
        foreach (var go in spawnedResults)
            Destroy(go);
        spawnedResults.Clear();
        displayedUsernames.Clear(); // Reset la liste des noms affichés
    }

    private async void SearchUsers(string searchText)
    {
        ClearResults();

        Query query;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            query = db.Collection("users")
                      .OrderBy("username")
                      .Limit(10);
        }
        else
        {
            // Cas 2 : Recherche par préfixe avec normalisation
            string normalizedSearch = searchText.Trim().ToLower();
            query = db.Collection("users")
                      .OrderBy("username")
                      .StartAt(normalizedSearch)
                      .EndAt(normalizedSearch + "\uf8ff")
                      .Limit(10);
        }

        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        foreach (DocumentSnapshot doc in snapshot.Documents)
        {
            string userId = doc.Id;

            // Skipp current User
            if (userId == currentUserId)
                continue;

            var data = doc.ToDictionary();
            string username = data.ContainsKey("username")
                ? data["username"].ToString()
                : "Unknown";

            // Anti doubles
            if (displayedUsernames.Contains(username))
                continue;

            //Search filter 
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (!username.ToLower().StartsWith(searchText.Trim().ToLower()))
                    continue;
            }

            displayedUsernames.Add(username);


            //Component Setup
            GameObject go = Instantiate(uiUserDisplayer, resultsDisplayer);
            Transform usernameTransform = go.transform.Find("PlayerUsernameTMP");
            TextMeshProUGUI tmp = usernameTransform.GetComponent<TextMeshProUGUI>();
            tmp.text = username;

            spawnedResults.Add(go);
        }
    }
}
