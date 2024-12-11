using System;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using TMPro;
using Google;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Networking;
using System.Linq;

using HatchyverseAPI.Api;
using HatchyverseAPI.Client;

public class FirebaseManager : MonoBehaviour
{
    private GoogleSignInConfiguration googleConfiguration;
    private const string USERS_COLLECTION = "users";

    private FirebaseAuth auth;
    private FirebaseFirestore db;
    private FirebaseUser user;
    private bool isProcessing = false;

    [Header("UI Elements")]
    public GameObject loginPanel;
    public GameObject registrationPanel;
    public GameObject profilePanel;
    public GameObject passwordResetPanel;

    [Header("Login UI")]
    public InputField loginEmailInput;
    public InputField loginPasswordInput;
    public Button loginButton;
    public TextMeshProUGUI loginButtonText;
    public Button gotoRegisterButton;
    public Button gotoPasswordResetButton;

    [Header("Registration UI")]
    public InputField registerEmailInput;
    public InputField registerPasswordInput;
    public InputField registerUsernameInput;
    public Button registerButton;
    public TextMeshProUGUI registerButtonText;
    public Button gotoLoginButton;
    public Button googleSignInButton;
    public TextMeshProUGUI googleSignInButtonText;

    [Header("Profile UI")]
    public Text usernameText;
    public Text emailText;
    public Text userIdText;
    public Button signOutButton;
    public Button changeSceneButton;
    public Image profilePicture;
    public InputField userInfoInput;

    [Header("Password Reset UI")]
    public InputField resetEmailInput;
    public Button sendPasswordResetButton;
    public TextMeshProUGUI resetButtonText;
    public Button backToLoginButton;

    [Header("Error Handling")]
    public Text errorText;
    public Text successText;
    private UsersApi usersApi;
    private AuthApi authApi;

    private void ResetAllButtonStates()
    {
        isProcessing = false;

        loginButton.interactable = true;
        registerButton.interactable = true;
        googleSignInButton.interactable = true;
        sendPasswordResetButton.interactable = true;

        loginButtonText.text = "Login";
        registerButtonText.text = "Register";
        googleSignInButtonText.text = "Sign In with Google";
        resetButtonText.text = "Send Reset Link";
    }

    private void SetLoadingState(Button button, TextMeshProUGUI buttonText, bool isLoading)
    {
        button.interactable = !isLoading;
        buttonText.text = isLoading ? "Loading..." : buttonText.gameObject.name;
    }

    private void Awake()
    {
        googleConfiguration = HatchyverseConfig.DefaultConfig.googleConfiguration;
        GoogleSignIn.Configuration = googleConfiguration;
        var apiConfiguration = HatchyverseConfig.DefaultConfig.apiConfiguration;
        usersApi = new UsersApi(apiConfiguration);
        authApi = new AuthApi(apiConfiguration);
    }

    private void Start()
    {
        InitializeFirebase();
        SetupButtonListeners();
        ShowLoginPanel();
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
        ResetAllButtonStates();
    }

    private void SetupButtonListeners()
    {
        loginButton.onClick.AddListener(EmailPasswordSignIn);
        registerButton.onClick.AddListener(EmailPasswordRegister);
        googleSignInButton.onClick.AddListener(GoogleSignInClick);
        gotoRegisterButton.onClick.AddListener(ShowRegistrationPanel);
        gotoLoginButton.onClick.AddListener(ShowLoginPanel);
        signOutButton.onClick.AddListener(SignOut);
        changeSceneButton.onClick.AddListener(ChangeScene);
        gotoPasswordResetButton.onClick.AddListener(ShowPasswordResetPanel);
        sendPasswordResetButton.onClick.AddListener(SendPasswordResetEmail);
        backToLoginButton.onClick.AddListener(ShowLoginPanel);
    }

    public void ShowLoginPanel()
    {
        loginPanel.SetActive(true);
        registrationPanel.SetActive(false);
        profilePanel.SetActive(false);
        passwordResetPanel.SetActive(false);
        ClearMessages();
        ResetAllButtonStates();
    }

    public void ShowRegistrationPanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(true);
        profilePanel.SetActive(false);
        passwordResetPanel.SetActive(false);
        ClearMessages();
        ResetAllButtonStates();
    }

    public void ShowProfilePanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        profilePanel.SetActive(true);
        passwordResetPanel.SetActive(false);
        ClearMessages();
    }

    public void ShowPasswordResetPanel()
    {
        loginPanel.SetActive(false);
        registrationPanel.SetActive(false);
        profilePanel.SetActive(false);
        passwordResetPanel.SetActive(true);
        ClearMessages();
        ResetAllButtonStates();
    }

    public void GoogleSignInClick()
    {
        if (isProcessing) return;
        isProcessing = true;
        SetLoadingState(googleSignInButton, googleSignInButtonText, true);

        // GoogleSignIn.Configuration.UseGameSignIn = false;
        // GoogleSignIn.Configuration.RequestIdToken = true;
        // GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Google Sign-In Faulted: " + task.Exception);
            DisplayErrorMessage("Google Sign-In failed. Please try again.");
            SetLoadingState(googleSignInButton, googleSignInButtonText, false);
            isProcessing = false;
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Google Sign-In Cancelled");
            DisplayErrorMessage("Google Sign-In was cancelled.");
            SetLoadingState(googleSignInButton, googleSignInButtonText, false);
            isProcessing = false;
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
            SignInWithCredential(credential);
        }
    }

    void SignInWithCredential(Firebase.Auth.Credential credential)
    {
        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(async task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                DisplayErrorMessage("Sign-in was cancelled. Please try again.");
                SetLoadingState(googleSignInButton, googleSignInButtonText, false);
                isProcessing = false;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                DisplayErrorMessage("Sign-in failed. Please try again.");
                SetLoadingState(googleSignInButton, googleSignInButtonText, false);
                isProcessing = false;
                return;
            }

            user = auth.CurrentUser;
            await SaveUserToFirestore(user);
        });
    }

    public void EmailPasswordSignIn()
    {
        if (isProcessing) return;

        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            DisplayErrorMessage("Please enter both email and password.");
            return;
        }

        isProcessing = true;
        SetLoadingState(loginButton, loginButtonText, true);

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(async task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sign-in failed: " + task.Exception);
                DisplayErrorMessage("Sign-in failed. Please check your credentials.");
                SetLoadingState(loginButton, loginButtonText, false);
                isProcessing = false;
                return;
            }

            user = task.Result.User;
            await SaveUserToFirestore(user);
        });
    }

    #region Registeration Logic
    public void EmailPasswordRegister()
    {
        if (isProcessing) return;

        string email = registerEmailInput.text;
        string password = registerPasswordInput.text;
        string username = registerUsernameInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
        {
            DisplayErrorMessage("Please fill in all fields: email, password, and username.");
            return;
        }

        // Validate email
        var emailValidation = ValidateEmail(email);
        if (!emailValidation.IsValid)
        {
            DisplayErrorMessage(emailValidation.ErrorMessage);
            SetLoadingState(registerButton, registerButtonText, false);
            isProcessing = false;
            return;
        }

        // Validate password
        var passwordValidation = ValidatePassword(password);
        if (!passwordValidation.IsValid)
        {
            DisplayErrorMessage(passwordValidation.ErrorMessage);
            SetLoadingState(registerButton, registerButtonText, false);
            isProcessing = false;
            return;
        }

        isProcessing = true;
        SetLoadingState(registerButton, registerButtonText, true);

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(async task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Registration failed: " + task.Exception);
                string errorMessage = "Registration failed. ";

                // Get the specific Firebase error message
                if (task.Exception?.InnerException is Firebase.FirebaseException firebaseEx)
                {
                    switch (firebaseEx.ErrorCode)
                    {
                        case 17026: // Invalid password
                            errorMessage += "Password must be at least 6 characters long.";
                            break;
                        case 17007: // Email already exists
                            errorMessage += "This email is already in use.";
                            break;
                        case 17008: // Invalid email
                            errorMessage += "Please enter a valid email address.";
                            break;
                        default:
                            errorMessage += "Please try again with different credentials.";
                            break;
                    }
                }

                DisplayErrorMessage(errorMessage);
                SetLoadingState(registerButton, registerButtonText, false);
                isProcessing = false;
                return;
            }

            user = task.Result.User;

            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = username
            };

            await user.UpdateUserProfileAsync(profile);
            await SaveUserToFirestore(user);
        });
    }
    #region Registeration Validation
    private enum ValidationError
    {
        None,
        // Password errors
        PasswordTooShort,
        PasswordNoUppercase,
        PasswordNoLowercase,
        PasswordNoNumber,
        PasswordNoSpecialChar,
        PasswordInvalid,
        PasswordTooWeak,
        // Email errors
        EmailEmpty,
        EmailInvalidFormat,
        EmailMissingAt,
        EmailInvalidDomain,
        EmailAlreadyExists,
        // Other
        Unknown
    }

    private class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public ValidationError ErrorCode { get; set; }

        public static ValidationResult Success()
        {
            return new ValidationResult
            {
                IsValid = true,
                ErrorCode = ValidationError.None,
                ErrorMessage = string.Empty
            };
        }
    }

    private ValidationResult ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Password cannot be empty.",
                ErrorCode = ValidationError.PasswordInvalid
            };
        }

        if (password.Length < 8)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Password must be at least 8 characters long.",
                ErrorCode = ValidationError.PasswordTooShort
            };
        }

        if (!password.Any(char.IsUpper))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Password must contain at least one uppercase letter.",
                ErrorCode = ValidationError.PasswordNoUppercase
            };
        }

        if (!password.Any(char.IsLower))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Password must contain at least one lowercase letter.",
                ErrorCode = ValidationError.PasswordNoLowercase
            };
        }

        if (!password.Any(char.IsDigit))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Password must contain at least one number.",
                ErrorCode = ValidationError.PasswordNoNumber
            };
        }

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Password must contain at least one special character.",
                ErrorCode = ValidationError.PasswordNoSpecialChar
            };
        }

        return ValidationResult.Success();
    }

    private ValidationResult ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Email address cannot be empty.",
                ErrorCode = ValidationError.EmailEmpty
            };
        }

        // Basic format check
        if (!email.Contains("@"))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Email address must contain '@' symbol.",
                ErrorCode = ValidationError.EmailMissingAt
            };
        }

        // Split email into local and domain parts
        string[] parts = email.Split('@');
        if (parts.Length != 2)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Email address format is invalid.",
                ErrorCode = ValidationError.EmailInvalidFormat
            };
        }

        string localPart = parts[0];
        string domain = parts[1];

        // Check local part
        if (string.IsNullOrEmpty(localPart) || localPart.Length > 64)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Invalid email address format.",
                ErrorCode = ValidationError.EmailInvalidFormat
            };
        }

        // Check domain
        if (string.IsNullOrEmpty(domain) || !domain.Contains(".") ||
            domain.StartsWith(".") || domain.EndsWith("."))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Invalid email domain format.",
                ErrorCode = ValidationError.EmailInvalidDomain
            };
        }

        // More comprehensive email regex check
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Please enter a valid email address.",
                ErrorCode = ValidationError.EmailInvalidFormat
            };
        }

        return ValidationResult.Success();
    }

    #endregion Registeration Validation   

    #endregion Registeration Logic

    private async Task SaveUserToFirestore(FirebaseUser firebaseUser)
    {
        try
        {
            await authApi.CreateUserAsync();
            /*
            DocumentReference userDoc = db.Collection(USERS_COLLECTION).Document(firebaseUser.UserId);

            Dictionary<string, object> userData = new Dictionary<string, object>
            {
                { "uid", firebaseUser.UserId },
                { "email", firebaseUser.Email },
                { "displayName", firebaseUser.DisplayName ?? "Anonymous" },
                { "photoUrl", firebaseUser.PhotoUrl?.ToString() },
                { "createdAt", Timestamp.FromDateTime(DateTime.UtcNow) },
                { "updatedAt", Timestamp.FromDateTime(DateTime.UtcNow) }
            };

            await userDoc.SetAsync(userData, SetOptions.MergeAll);
            */

            Debug.Log("User data successfully saved to Firestore");
            DisplaySuccessMessage("Account successfully synchronized");
            UpdateUserInfo();
            isProcessing = false;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving user data to Firestore: {ex.Message}");
            DisplayErrorMessage("Failed to save user data");
            ResetAllButtonStates();
            isProcessing = false;
        }
    }

    private async void UpdateUserInfo()
    {
        usernameText.text = user.DisplayName ?? "N/A";
        emailText.text = user.Email;
        userIdText.text = user.UserId;

        // var configuration = GetApiConfiguration();
        // var api = new UsersApi(configuration);
        var userInfo = await usersApi.GetUserAsync();
        userInfoInput.text = userInfo.ToString();

        if (!string.IsNullOrEmpty(user.PhotoUrl?.ToString()))
        {
            StartCoroutine(LoadImage(user.PhotoUrl.ToString()));
        }
        else
        {
            profilePicture.gameObject.SetActive(false);
        }
        ShowProfilePanel();
    }

    public void SendPasswordResetEmail()
    {
        if (isProcessing) return;

        string email = resetEmailInput.text;

        if (string.IsNullOrEmpty(email))
        {
            DisplayErrorMessage("Please enter your email address.");
            return;
        }

        isProcessing = true;
        SetLoadingState(sendPasswordResetButton, resetButtonText, true);

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to send password reset email: " + task.Exception);
                DisplayErrorMessage("Failed to send password reset email. Please try again.");
                SetLoadingState(sendPasswordResetButton, resetButtonText, false);
                isProcessing = false;
                return;
            }

            DisplaySuccessMessage("Password reset email sent. Please check your inbox.");
            SetLoadingState(sendPasswordResetButton, resetButtonText, false);
            isProcessing = false;
        });
    }

    public void SignOut()
    {
        user = null;
        auth.SignOut();
        ResetAllButtonStates();
        ShowLoginPanel();
    }

    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("AuthenticatedScene");
    }

    private void DisplayErrorMessage(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
        successText.gameObject.SetActive(false);
    }

    private void DisplaySuccessMessage(string message)
    {
        successText.text = message;
        successText.gameObject.SetActive(true);
        errorText.gameObject.SetActive(false);
    }

    private void ClearMessages()
    {
        errorText.gameObject.SetActive(false);
        successText.gameObject.SetActive(false);
    }

    IEnumerator LoadImage(string imageUri)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUri))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                profilePicture.gameObject.SetActive(true);
                profilePicture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogError("Failed to load profile image: " + www.error);
                profilePicture.gameObject.SetActive(false);
            }
        }
    }
}