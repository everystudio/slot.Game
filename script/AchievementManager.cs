using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_ANDROID

using GooglePlayGames.BasicApi;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
#endif
using System;
using UnityEngine.SocialPlatforms;

public class AchievementManager : Singleton<AchievementManager> {

	#if UNITY_ANDROID
	void OnReceivedInvitation(Invitation invitation, bool shouldAutoAccept)
	{

	}
	private void OnMatch(TurnBasedMatch match, bool shouldAutoLaunch)
	{
		throw new NotImplementedException();
	}
	#endif
	public void Login()
	{
#if UNITY_ANDROID
		//GooglePlayConnection.Instance.Connect();
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		/*PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			.EnableSavedGames()
			.WithInvitationDelegate(OnReceivedInvitation)
			.WithMatchDelegate(OnMatch)
			.RequireGooglePlus()
			.Build();
			*/
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			if ( success == true)
			{
				//SceneManager.LoadSceneAsync("SlotGame");
			}
		});
#elif UNITY_IOS
		IOSGameCenterManager.Auth();
#endif
	}

	public void ShowAchievement()
	{
#if UNITY_ANDROID
		//GooglePlayManager.Instance. ShowAchievementsUI();
		Social.ShowAchievementsUI();
#elif UNITY_IOS
		IOSGameCenterManager.ShowAchievementsUI();
#endif
	}

	public void GetAchievement(string _strId)
	{
#if UNITY_ANDROID
		//GooglePlayManager.Instance.UnlockAchievementById(_strId);
		Social.ReportProgress(_strId, 100.0f, (bool success) => {
			// handle success or failure
		});
#elif UNITY_IOS
		IOSGameCenterManager.ReportProgress(_strId, 100.0f);
#endif

	}

	public void ShowRanking(string _strRanking)
	{
#if UNITY_ANDROID
		//GooglePlayManager.Instance.ShowLeaderBoardById(_strRanking);
		Social.ShowLeaderboardUI();
		/*
		ILeaderboard lb = PlayGamesPlatform.Instance.CreateLeaderboard();
		lb.id = _strRanking;
		lb.LoadScores(ok =>
		{
			if (ok)
			{
				//LoadUsersAndDisplay(lb);
			}
			else {
				Debug.Log("Error retrieving leaderboardi");
			}
		});
		*/

#elif UNITY_IOS
		IOSGameCenterManager.ShowLeaderboardUI();
#endif

	}
	public void RegisterRanking( string _strRanking , long _lScore)
	{
#if UNITY_ANDROID
		//GooglePlayManager.Instance.SubmitScoreById(_strRanking, _lScore);
		Social.ReportScore(_lScore, _strRanking, (bool success) => {
			// handle success or failure
		});

#elif UNITY_IOS
		IOSGameCenterManager.ReportScore(_strRanking, _lScore);
#endif

	}


}
