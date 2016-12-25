using UnityEngine;
using System.Collections;
using System;

public class PlayUnityAds : MonoBehaviour {

	// 補充をしてもOKの場合真
	public bool m_bCheckOk;

	// これが真だと、回転後に何か揃っていたらムービーを流す
	public bool m_bPlayStandby;

	public string m_strNextCheckTime;

	public GameObject m_goWindowRoot;

	private CSFramework.GameInfo m_gameInfo;
	// Use this for initialization
	void Start () {

		TimeSpan ts = TimeManager.Instance.GetDiffNow(DataManager.Instance.user.nextUnityadsCheckTime);
		//Debug.LogError(user.recoveryTime);
		//Debug.LogError(ts.TotalSeconds);
		if (ts.TotalSeconds < 0)
		{
			m_bCheckOk = true;
		}
		else
		{
			m_bCheckOk = false;
			Invoke("invokeCheckOk", (float)ts.TotalSeconds);
		}
		m_goWindowRoot.SetActive(false);

		m_strNextCheckTime = DataManager.Instance.user.nextUnityadsCheckTime;
	}

	private void invokeCheckOk()
	{
		Debug.LogError("call invokeOK");
		m_bCheckOk = true;
	}

	public void Show(CSFramework.GameInfo _gameInfo )
	{
		m_bCheckOk = false;
		m_goWindowRoot.SetActive(true);
		m_gameInfo = _gameInfo;
		DataManager.Instance.user.nextUnityadsCheckTime = TimeManager.StrGetTime(60 * 60);
		m_strNextCheckTime = DataManager.Instance.user.nextUnityadsCheckTime;
		Invoke("invokeCheckOk", (float)(60 * 60));
		//Invoke("invokeCheckOk", (float)(10));
	}

	public void OnYes()
	{
		m_goWindowRoot.SetActive(false);
		m_gameInfo.AddBalance(3000);
		m_bPlayStandby = true;
	}

	public void OnNo()
	{
		m_goWindowRoot.SetActive(false);
	}



}
