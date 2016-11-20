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
		GooglePlayConnection.Instance.Connect();
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




	
}
