using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
public class AdsManager : Singleton<AdsManager> {

	private string m_strSceneNow = "";
	private string m_strScenePre = "";
	public string nowScene
	{
		get { return m_strSceneNow; }
		set
		{
			m_strSceneNow = value;
			if(!m_strScenePre.Equals(m_strSceneNow))
			{
				refreshAds(m_strSceneNow);
				m_strScenePre = m_strSceneNow;
			}
		}
	}

	BannerView view1 = null;		// 画面上部
	BannerView view2 = null;		// 画面左下

	private void refreshAds(string _strSceneName)
	{
#if UNITY_EDITOR
		string adUnitId1 = "unused";
		string adUnitId2 = "unused";
#elif UNITY_ANDROID
        string adUnitId1 = "ca-app-pub-5869235725006697/4481993169";
        string adUnitId2 = "ca-app-pub-5869235725006697/2917861565";
#elif UNITY_IPHONE
        string adUnitId1 = "ca-app-pub-5869235725006697/5958726364";
        string adUnitId2 = "ca-app-pub-5869235725006697/1301527568";
#else
        string adUnitId1 = "unexpected_platform";
        string adUnitId2 = "unexpected_platform";
#endif
		switch (_strSceneName)
		{
			case "Startup":
				if (view1 == null)
				{
					BannerView bannerView1 = new BannerView(adUnitId1, AdSize.Banner, AdPosition.Top);
					// Create an empty ad request.
					AdRequest request1 = new AdRequest.Builder().Build();
					// Load the banner with the request.
					bannerView1.LoadAd(request1);
					view1 = bannerView1;
				}
				else
				{
					view1.Show();
				}
				if (view2 != null)
				{
					view2.Hide();
				}
				break;
			case "SlotGame":
				if (view1 != null)
				{
					view1.Show();
				}
				if(view2 == null)
				{
					BannerView bannerView1 = new BannerView(adUnitId2, AdSize.Banner, AdPosition.BottomLeft);
					// Create an empty ad request.
					AdRequest request1 = new AdRequest.Builder().Build();
					// Load the banner with the request.
					bannerView1.LoadAd(request1);
					view2 = bannerView1;
				}
				else
				{
					view2.Show();
				}
				break;
			default:
				break;
		}
	}

	void loadedScene(Scene scenename, LoadSceneMode SceneMode)
	{
		//Debug.LogError("今のSceneの名前 = " +scenename.name);
		nowScene = scenename.name;
	}
	public override void Initialize()
	{
		SceneManager.sceneLoaded += loadedScene;

		base.Initialize();
		SetDontDestroy(true);
		/*
		// Create a 320x50 banner at the top of the screen.
		BannerView bannerView1 = new BannerView(adUnitId1, AdSize.Banner, AdPosition.Top);
		BannerView bannerView2 = new BannerView(adUnitId2, AdSize.Banner, AdPosition.BottomLeft);
		// Create an empty ad request.
		AdRequest request1 = new AdRequest.Builder().Build();
		AdRequest request2 = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView1.LoadAd(request1);
		bannerView2.LoadAd(request2);
		view1 = bannerView1;
		view2 = bannerView2;
		view1.OnAdLoaded += View_OnAdLoaded;
		view2.OnAdLoaded += View_OnAdLoaded;
		*/
	}

	private void View_OnAdLoaded(object sender, System.EventArgs e)
	{
		//refreshAds(m_strSceneNow);
		// 何かしたいならここ
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void HideAll()
	{
		if(view1 != null)
		{
			view1.Hide();
		}
		if(view2 != null)
		{
			view2.Hide();
		}
	}
	public void ShowAll()
	{
		refreshAds(m_strSceneNow);
	}
}
