using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HatchyverseAPI.Api;
using HatchyverseAPI.Client;
using HatchyverseAPI.Model;
using UnityEngine.UI;

public class AuthenticatedSceneScript : MonoBehaviour
{
    public InputField testInput;
    private GameApi gameApi;
    private UsersApi usersApi;
    private LeaderboardApi leaderboardApi;
    void Start()
    {
        var apiConfiguration = HatchyverseConfig.DefaultConfig.apiConfiguration;
        gameApi = new GameApi(apiConfiguration);
        usersApi = new UsersApi(apiConfiguration);
        leaderboardApi = new LeaderboardApi(apiConfiguration);
    }

    public async void OnGetGames()
    {
        Debug.Log("OnGetGames");
        var list = await gameApi.GetGamesAsync();
        foreach (var game in list)
        {
            testInput.text = game.ToString();
        }
    }

    public async void OnGetUserInfo()
    {
        Debug.Log("OnGetUserInfo");
        var userInfo = await usersApi.GetUserAsync();
        testInput.text = userInfo.ToString();
    }

    public async void PostScore()
    {
        Debug.Log("OnGetUserInfo");
        Debug.Log(HatchyverseConfig.DefaultConfig.appId);
        await leaderboardApi.AddScoreAsync(new AddScoreRequest("", "", 10, HatchyverseConfig.DefaultConfig.appId));
    }

    public async void PostRank()
    {
        Debug.Log("OnGetUserInfo");
        Debug.Log(HatchyverseConfig.DefaultConfig.appId);
        var userInfo = await usersApi.GetUserAsync();
        await leaderboardApi.UpdateRankAsync(new UpdateRankRequest(userInfo.Uid, 10, HatchyverseConfig.DefaultConfig.appId));
    }
}
