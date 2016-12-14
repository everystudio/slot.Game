using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CP.ProChart;

public class UIStartupMenu : CPanel {

	public CtrlUserParam m_userParam;

	protected override void awake ()
	{
		base.awake ();
		m_userParam.Initialize (DataManager.USER_PARAM.COIN);

		AchievementManager.Instance.Login();

		SpriteManager.Instance.LoadAtlas("texture/collection/kimodameshi");
		SpriteManager.Instance.LoadAtlas("texture/collection/zoo");
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
