# Hatchyverse Unity SDK Quickstart

The Hatchyverse Unity Sample demonstrates user authentication and
user profile operations using
[Firebase Authentication](https://firebase.google.com/docs/auth/)
with the
[Firebase Unity SDK](https://firebase.google.com/docs/unity/setup).

## Requirements

* [Unity](http://unity3d.com/) The quickstart project requires 2022 or higher.
* [Android SDK](https://developer.android.com/studio/index.html#downloads)
  (when developing for Android).
* Minimum Android API level 31.

## Configure Hatchyverse in your Unity project

### Android

* Provide your App Bundle along with the SHA1 to Hatchyverse Team (<hatchypocket@gmail.com>)
  * Provide your App Bundle (e.g. `com.hatchyverse.rampage.dev`) to Hatchyverse Team .
    * Select the **File > Build Settings** menu option.
    * Select **Android** in the **Platform** list.
    * Click **Player Settings**.
    * In the **Settings for Android** panel scroll down to **Other Settings** and to **Identification** and you will see the Android package name or if it is empty you can fill it with your desired package name. (e.g. `com.hatchyverse.packagetest`)
  * Android apps must be signed by a key, in order to get it you will need to set the keystore in the Unity project.
    * Locate the **Publishing Settings** under **Player Settings** in the
      Unity editor.
    * Select an existing keystore, or create a new keystore using the
      toggle.
    * Select an existing key, or create a new key using
      **Create a new key**.
    * After setting the keystore and key, you can generate a SHA1 by
      running this command:

        ```bash
        keytool -list -v -keystore <path_to_keystore> -alias <key_name>
        ```

* Obtain your app configuration files from Hatchyverse Team
  * `google-services.json` for Android.
    * Place the `google-services.json` file in the `Assets` directory.
    * NOTE: `google-services.json` can be placed anywhere under the `Assets`
      folder.
  * `hatchyverse-config.json`
    * Place the `hatchyverse-config.json` file in the `Assets/StreamingAssets` directory. If the directory does not exist, create it.
* Download the
  [Firebase Unity SDK](https://firebase.google.com/download/unity)
  and unzip it somewhere convenient.
* Import the Firebase Auth plugin.
  * Select the **Assets > Import Package > Custom Package** menu item.
  * From the [Firebase Unity SDK](https://firebase.google.com/download/unity)
    downloaded previously, import `FirebaseAuth.unitypackage`.
  * When the **Import Unity Package** window appears, click the **Import**
    button.
* Download the Google Sign-in Unity SDK (version 1.0.4) from the
  [Google Sign in SDK](https://github.com/googlesamples/google-signin-unity/releases)
  website.
  * Import the `google-signin-plugin-1.0.4.unitypackage` package. **Important** Do not import the `Parse` folder.
  * Disable Android Auto-Resolution and Resolution on Build.
  * Click Assets -> External Dependency Manager -> Android Resolver -> Resolve
* Download the Newtonsoft.Json.dll from the
  [Newtonsoft.Json](https://www.newtonsoft.com/json) website.
  * Place the dll located in `lib/netstandard2.0/Newtonsoft.Json.dll` file in the `Assets` directory.
* Download the hatchyverse-unity-sdk unity package from the repository and import it as custom package on Unity
* Now you can add the AllAuthDemo Sample Scene located in `Assets/Hatchyverse/Scenes/AllAuthDemo.unity` and build for android.

### iOS (WIP)

  *`GoogleService-Info.plist` for iOS.

## Using the Auth

### Google Sign In

The package provides a HatchyverseConfig instance that can be used to configure both the Google Sign In and api services.
Here is an example of how to configure the Google Sign In.

```csharp
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using HatchyverseAPI.Api;
using HatchyverseAPI.Client;

public class SomeScript : MonoBehaviour
{
    private GoogleSignInConfiguration googleConfiguration;
    private FirebaseAuth auth;
    private FirebaseUser user;
    private bool isProcessing = false;
    private AuthApi authApi;

    private void Awake()
    {
        googleConfiguration = HatchyverseConfig.DefaultConfig.googleConfiguration;
        GoogleSignIn.Configuration = googleConfiguration;

        var apiConfiguration = HatchyverseConfig.DefaultConfig.apiConfiguration;
        authApi = new AuthApi(apiConfiguration);
    }
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void GoogleSignInClick()
    {
        if (isProcessing) return;
        isProcessing = true;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Google Sign-In Faulted: " + task.Exception);
            isProcessing = false;
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Google Sign-In Cancelled");
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
                isProcessing = false;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                isProcessing = false;
                return;
            }

            user = auth.CurrentUser;
            await authApi.CreateUserAsync();
            isProcessing = false;
        });
    }
}
```

### Using Api Services

You can use the api services in any scene or script. The Api Client handles the authentication token so you don't need to worry about it.
Here are some examples of how to use an authenticated call and get calls.

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HatchyverseAPI.Api;
using HatchyverseAPI.Client;
using UnityEngine.UI;

public class AuthenticatedSceneScript : MonoBehaviour
{
    private GameApi gameApi;
    private UsersApi usersApi;
    void Start()
    {
        var apiConfiguration = HatchyverseConfig.DefaultConfig.apiConfiguration;
        gameApi = new GameApi(apiConfiguration);
        usersApi = new UsersApi(apiConfiguration);
    }

    public async void OnGetGames()
    {
        Debug.Log("OnGetGames");
        var list = await gameApi.GetGamesAsync();
        foreach (var game in list)
        {
            Debug.Log(game.ToString());
        }
    }

    public async void OnGetUserInfo()
    {
        Debug.Log("OnGetUserInfo");
        var userInfo = await usersApi.GetUserAsync();
        Debug.Log(userInfo.ToString());
    }
}
```

## Troubleshooting (WIP)

* Import Google Sign In
  * Apply Google Version Handler changes when prompted.
  * Enable Android Gradle Templates when prompted.
  * If you are targeting just Android Select `Assets/ExternalDependencyManager/Editor/1.2.183/Google.IOSResolver.dll` and disable Validate References in the inspector. Select `Assets/Firebase/Editor/Firebase.Editor.dll` and disable Validate References in the inspector.
  * Change .srcaar to .aar in the google sign in support files in Assets\GoogleSignIn\Editor\m2repository\com\google\signin\google-signin-support\1.0.4\
  * Click Assets -> External Dependency Manager -> Android Resolver -> Delete Resolved Libraries
  * Click Assets -> External Dependency Manager -> Android Resolver -> Resolve
* The type "task" exist in both Unity.Tasks and mscorelib
  * goto: youProjectFolder\Assets\Parse\Plugins and remove all files outside of the dotNet45 Folder
* Unable to find native-googlesignin
  * navigate to ..\Assets\GoogleSignIn\Editor\m2repository\com\google\signin\google-signin-support\1.0.4 and change .srcaar by .aar
  * After that, go to your project in Unity and select the .aar file. You'll need to include the platforms you are using the app on.

* When upgrading to a new Firebase release: import the new firebase
    unity package through **Assets > Import Package > Custom Package** as above.
    After the import is complete you may need to run the **Assets > Play
    Services Resolver** for the changes to be reflected in the editor. If
    issues persist, delete the plugin and install it again.
* **Android:** After exiting the editor and returning you will need to
    reconfigure the **Project Keystore** in **Player Settings > Publishing
    Settings**.  Select your **Custom Keystore** from the dropdown list and
    enter its password.  Then, select your **Project Key** alias and enter
    your key's password.
* Please see the
    [Known Issues](https://firebase.google.com/docs/unity/setup#known-issues)
    section of the
    [Unity Setup Guide](https://firebase.google.com/docs/unity/setup) for other
    troubleshooting topics.
* When running the app, if all that you see is a blue horizon, then please
    ensure that you followed the steps to **Open the scene `MainScene`**
    above.

## Docs

For more information about Firebase SDK usage, see the [Firebase Unity SDK Doc](https://firebase.google.com/docs/unity/setup).

For more information about Hatchyverse API usage, see the [Hatchyverse API Doc](https://api.hatchyverse.com/docs).

## Create App (Hatchyverse Team Only)

* Register the app with Firebase.
  * Create a Unity project in the
      [Firebase console](https://firebase.google.com/console/).
  * Associate your project to an app by clicking the **Add app** button,
      and selecting the **Unity** icon.
    * Check the box labeled **Register as Android app**.
    * Provide the **Android package name** of the app you're going to add.
    * Download the `google-services.json` file associated with your
          Firebase project from the console.
          This file identifies your Android app to the Firebase backend, and will
          need to be provided to the dev in order to complete its configuration.
      * For further details please refer to the
          [general instructions](https://firebase.google.com/docs/android/setup)
          page which describes how to configure a Firebase application for
          Android.
  * Android apps must be signed by a key, and the key's signature must
      be registered to your project in the Firebase Console
    * Copy the SHA1 digest string provided by the developers.
    * Navigate to your Android App in your firebase console.
      * From the main console view, click on your Android App at the top,
          then click the gear to open the settings page.
    * Scroll down to your apps at the bottom of the page and click on
        **Add Fingerprint**.
    * Paste the SHA1 digest of your key into the form. The SHA1 box
        will illuminate if the string is valid. If it's not valid, check
        that you have copied the entire SHA1 digest string.

## Support

[hatchypocket@gmail.com](<hatchypocket@gmail.com>)
