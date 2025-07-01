using System.Threading.Tasks;
using Firebase.Firestore;

public static class DatabaseService
{
    private const string USERS_COLLECTION = "users";
    private const string SCORES_COLLECTION = "scores";

    public static async Task CreateUserDocument(string userId, User userData)
    {
        DocumentReference docRef = FirebaseManager.Instance.Db
            .Collection(USERS_COLLECTION)
            .Document(userId);

        await docRef.SetAsync(userData.ToDictionary());
    }

    public static async Task<User> GetUserDocument(string userId)
    {
        DocumentReference docRef = FirebaseManager.Instance.Db
            .Collection(USERS_COLLECTION)
            .Document(userId);

        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        return snapshot.Exists ? User.FromDictionary(snapshot.ToDictionary()) : null;
    }
}
