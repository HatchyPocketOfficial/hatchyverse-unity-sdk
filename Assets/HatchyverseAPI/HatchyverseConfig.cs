using System.IO;
using UnityEngine;
using Google;
using UnityEngine.Networking;

namespace HatchyverseAPI.Client
{

  [System.Serializable]
  public class HatchyverseConfigJson
  {
    public string hatchyverseAppId;
    public string googleWebClientId;
    public string apiBaseURL;
  }

  public class HatchyverseConfig
  {
    public Configuration apiConfiguration;
    public GoogleSignInConfiguration googleConfiguration;
    public string appId;

    private static HatchyverseConfig _defaultConfig;

    public static HatchyverseConfig DefaultConfig
    {
      get
      {
        if (_defaultConfig == null)
        {
          string tempFolderPath = Application.persistentDataPath;
          var jsonConfig = LoadDefaultConfig();
          _defaultConfig = new HatchyverseConfig
          {
            apiConfiguration = new Configuration
            {
              BasePath = jsonConfig.apiBaseURL,
              TempFolderPath = tempFolderPath
            },
            googleConfiguration = new GoogleSignInConfiguration
            {
              WebClientId = jsonConfig.googleWebClientId,
              RequestIdToken = true,
              UseGameSignIn = false,
              RequestEmail = true
            },
            appId = jsonConfig.hatchyverseAppId
          };

        }
        return _defaultConfig;
      }
    }

    private static HatchyverseConfigJson LoadDefaultConfig()
    {
      string path = Path.Combine(Application.streamingAssetsPath, "hatchyverse-config.json");
      string json;
      if (Application.platform == RuntimePlatform.Android)
      {
        UnityWebRequest www = UnityWebRequest.Get(path);
        www.SendWebRequest();
        while (!www.isDone) ;
        json = www.downloadHandler.text;
      }
      else json = File.ReadAllText(path);
      return JsonUtility.FromJson<HatchyverseConfigJson>(json);
    }
  }
}