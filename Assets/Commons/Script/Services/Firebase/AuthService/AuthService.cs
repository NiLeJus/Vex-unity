using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthService : MonoBehaviour
{
    // Common
    private FirebaseAuth auth;
    private FirebaseFirestore db;
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
    }

    #region SignIn //
    [Header("Sign In UI")]
    [SerializeField] private TMP_InputField inputUsernameEmailSI;
    [SerializeField] private TMP_InputField inputPasswordSI;
    [SerializeField] private TMP_Text errorTextSI;
    [SerializeField] private Button onSignInBtn;
    private bool AreSignInFieldsValid()
    {
        var errors = new System.Text.StringBuilder();

        if (string.IsNullOrWhiteSpace(inputUsernameEmailSI.text))
            errors.AppendLine("Email/username is required");

        if (string.IsNullOrWhiteSpace(inputPasswordSI.text))
            errors.AppendLine("Password is required");

        errorTextSI.text = errors.ToString();
        return errors.Length == 0;
    }

    #endregion

    #region SignUp //
    [Header("Sign Up UI")]
    [SerializeField] private TMP_InputField inputUsername;
    [SerializeField] private TMP_InputField inputEmailSU;
    [SerializeField] private TMP_InputField inputPasswordSU;
    [SerializeField] private TMP_InputField inputPasswordConfirmationSU;
    [SerializeField] private TMP_Text errorTextSU;
    [SerializeField] private Button onSignUpBtn;

    public async Task CreateUser(string email, string password, string username)
    {
        try
        {
            AuthResult result = await FirebaseManager.Instance.Auth
                .CreateUserWithEmailAndPasswordAsync(email, password);

            User newUser = new User { Email = email, Username = username };
            await DatabaseService.CreateUserDocument(result.User.UserId, newUser);
        }
        catch (FirebaseException e)
        {
            Debug.LogError($"SignUp failed: {e.Message}");
        }
    }

    private bool AreSignUpFieldsValid()
    {
        var errors = new System.Text.StringBuilder();

        if (string.IsNullOrWhiteSpace(inputUsername.text))
            errors.AppendLine("Username is required");

        if (string.IsNullOrWhiteSpace(inputEmailSU.text))
            errors.AppendLine("Email is required");
        else if (!IsEmailValid(inputEmailSU.text))
            errors.AppendLine("Invalid email format");

        if (string.IsNullOrWhiteSpace(inputPasswordSU.text))
            errors.AppendLine("Password is required");
        //else if (!IsPasswordStrong(inputPasswordSU.text))
        //    errors.AppendLine("Password must contain 8 characters, one uppercase letter, and one number");

        if (!IsPasswordConfirmed())
            errors.AppendLine("Passwords do not match");

        errorTextSU.text = errors.ToString();
        return errors.Length == 0;
    }

    #endregion

    #region Guest //

    [SerializeField] private Button onGuestBtn;
    public async Task SignInAnonymously()
    {
        try
        {
            AuthResult result = await auth.SignInAnonymouslyAsync();
            FirebaseUser user = result.User;
            Debug.Log($"Guest login successful! User ID: {user.UserId}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Anonymous login failed: {e.Message}");
        }
    }

    #endregion

    public async void OnSignInBtnClicked()
    {
        if (!AreSignInFieldsValid()) return;

        try
        {
            AuthResult results = await auth.SignInWithEmailAndPasswordAsync(
                inputUsernameEmailSI.text,
                inputPasswordSI.text
            );
            Debug.Log($"Logged in as: {results.User.Email}");
            NavigateToGameScene();
        }
        catch (System.Exception e)
        {
            errorTextSI.text = GetFirebaseErrorMessage(e);
        }
    }

    public async void OnSignUpBtnClicked()
    {
        if (!AreSignUpFieldsValid()) return;

        try
        {
            // Firebase Auth
            AuthResult results = await auth.CreateUserWithEmailAndPasswordAsync(
                inputEmailSU.text,
                inputPasswordSU.text
            );

            //Update the username in FireDb
            UserProfile profile = new UserProfile { DisplayName = inputUsername.text };
            await results.User.UpdateUserProfileAsync(profile);

            // Firestore 
            User newUser = new User
            {
                Uid = results.User.UserId,
                Username = inputUsername.text.ToLower(),
                Email = inputEmailSU.text
            };
            await DatabaseService.CreateUserDocument(results.User.UserId, newUser);

            Debug.Log($"Account created: {results.User.Email}");
            NavigateToGameScene();
        }
        catch (System.Exception e)
        {
            errorTextSU.text = GetFirebaseErrorMessage(e);
        }
    }

    public async void OnGuestBtnClicked()
    {
        try
        {
            AuthResult result = await auth.SignInAnonymouslyAsync();
            Debug.Log($"Guest login successful! User ID: {result.User.UserId}");
            NavigateToGameScene();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error Guest Login");
            //guestErrorText.text = $"Guest login failed: {e.Message}";
        }
    }

    public void NavigateToGameScene()
    {
        LaunchSceneManager.i.NextScene();
    }



    #region Pure Field Check //
    private bool IsEmailValid(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    private bool IsPasswordStrong(string password)
    {
        return password.Length >= 8 &&
               System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]") &&
               System.Text.RegularExpressions.Regex.IsMatch(password, "[0-9]");
    }
    private bool IsPasswordConfirmed() =>
        inputPasswordSU.text == inputPasswordConfirmationSU.text;
    #endregion

    private string GetFirebaseErrorMessage(System.Exception e)
    {
        if (e is FirebaseException firebaseEx)
        {
            var errorCode = (AuthError)firebaseEx.ErrorCode; //!! Cast explicite 

            switch (errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    return "This email is already in use";
                case AuthError.WeakPassword:
                    return "Password is too weak";
                case AuthError.InvalidEmail:
                    return "Invalid email";
                case AuthError.MissingPassword:
                    return "Password is required";
                case AuthError.WrongPassword:
                    return "Incorrect password";
                default:
                    return $"Firebase error: {errorCode}";
            }
        }
        return $"Unexpected error: {e.Message}";
    }

}
