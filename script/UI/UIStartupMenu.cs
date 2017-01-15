using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using GoogleMobileAds.Api;

public class UIStartupMenu : CPanel {
	public InterstitialAd interstitial;

	public CtrlUserParam m_userParam;

	public Button m_btnStart;
	protected override void awake ()
	{
		base.awake ();
		m_userParam.Initialize (DataManager.USER_PARAM.COIN);

		AchievementManager.Instance.Login();

		SpriteManager.Instance.LoadAtlas("texture/collection/kimodameshi");
		SpriteManager.Instance.LoadAtlas("texture/collection/zoo");

#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-5869235725006697/6964344360";
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-5869235725006697/8441077569";
#endif

		// Create an interstitial.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.Build();

		// Load the interstitial with the request.
		m_btnStart.interactable = false;
		interstitial.LoadAd(request);
		interstitial.OnAdLoaded += ViewInterstitial_OnAdLoaded;
		interstitial.OnAdFailedToLoad += ViewInterstitial_OnAdFailedToLoad;
		interstitial.OnAdClosed += ViewInterstitial_OnAdClosed;

		Invoke("autoStart", 5);
	}
	private void autoStart()
	{
		m_btnStart.interactable = true;
	}

	private void ViewInterstitial_OnAdLoaded(object sender, System.EventArgs e)
	{
		InterstitialAd inter = (InterstitialAd)sender;
		inter.Show();
		m_btnStart.interactable = true;
	}
	private void ViewInterstitial_OnAdFailedToLoad(object sender, System.EventArgs e)
	{
		Debug.LogError("fail");
		m_btnStart.interactable = true;
	}
	private void ViewInterstitial_OnAdClosed(object sender, System.EventArgs e)
	{
		InterstitialAd inter = (InterstitialAd)sender;
		inter.Destroy();
	}


	public bool m_bLoaded;
	protected override void panelStart ()
	{
		m_bLoaded = false;
		base.panelStart ();
	}

	public void pushButton(){
		if (m_bLoaded == false) {
			SceneManager.LoadSceneAsync ("SlotGame");
			m_bLoaded = true;
		}
	}

	public void OnShowRanking()
	{
		AchievementManager.Instance.ShowRanking(DataManager.LEADERBOARD_ID_30GAME);
	}

	public void OnShowAchievement()
	{
		//UtilPlayService.Instance.showAchievementsUI();
		AchievementManager.Instance.ShowAchievement();
	}



	
}
