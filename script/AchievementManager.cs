using UnityEngine;
using System.Collections;

public class AchievementManager : Singleton<AchievementManager> {

	public void Login()
	{
#if UNITY_ANDROID
		GooglePlayConnection.Instance.Connect();
#elif UNITY_IOS
		IOSGameCenterManager.Auth();
#endif
	}

	public void ShowAchievement()
	{
#if UNITY_ANDROID
		GooglePlayManager.Instance. ShowAchievementsUI();
#elif UNITY_IOS
		IOSGameCenterManager.ShowAchievementsUI();
#endif
	}

	public void GetAchievement(string _strId)
	{
#if UNITY_ANDROID
		GooglePlayManager.Instance.UnlockAchievementById(_strId);
#elif UNITY_IOS
		IOSGameCenterManager.ReportProgress(_strId, 100.0f);
#endif

	}

	public void ShowRanking()
	{
#if UNITY_ANDROID
		GooglePlayManager.Instance.ShowLeaderBoardById("CgkI9Y3s2cEKEAIQHw");
#elif UNITY_IOS
		IOSGameCenterManager.ShowLeaderboardUI();
#endif

	}
	public void RegisterRanking( string _strRanking , int _iScore)
	{
#if UNITY_ANDROID
		GooglePlayManager.Instance.SubmitScoreById(_strRanking, (long)_iScore);
#elif UNITY_IOS
		IOSGameCenterManager.ReportScore(_strRanking, (long)_iScore);
#endif

	}


}
