using UnityEngine;
using UnityEngine.UI;
using System;

public class RecoveryTime : MonoBehaviour {

	private bool m_bShow;
	public Text m_txtShow;
	public void Show(bool _bFlag)
	{
		m_bShow = _bFlag;
		if (_bFlag)
		{
			DispUpdate();
		}
		else
		{
			CancelInvoke("DispUpdate");

		}
	}

	public void DispUpdate()
	{
		TimeSpan ts = TimeManager.Instance.GetDiffNow(DataManager.Instance.user.recoveryTime);

		bool bShow = DataManager.Instance.user.coin < DataUser.DefaultCoin;
		if (0 < ts.TotalSeconds && bShow )
		{
			m_txtShow.text = string.Format("回復まで\n{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
		}
		else
		{
			m_txtShow.text = "";
		}
		Invoke("DispUpdate", 1);
	}

	void Start()
	{
		Show(true);
	}
}
