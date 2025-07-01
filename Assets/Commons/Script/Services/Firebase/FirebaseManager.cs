using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }
    public FirebaseFirestore Db { get; private set; }
    public FirebaseAuth Auth { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Db = FirebaseFirestore.DefaultInstance;
        Auth = FirebaseAuth.DefaultInstance;
        DontDestroyOnLoad(gameObject);
        // Initialisation Firebase Analytics
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Activer la collecte Analytics
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }
}