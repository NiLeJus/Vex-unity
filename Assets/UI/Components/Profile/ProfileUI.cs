using UnityEngine;

public class ProfileUI : MonoBehaviour
{
    public async void LoadUserProfile()
    {
        string userId = FirebaseManager.Instance.Auth.CurrentUser.UserId;
        User user = await DatabaseService.GetUserDocument(userId);

        if (user != null)
        {

        }
    }
}
