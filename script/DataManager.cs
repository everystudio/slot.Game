using UnityEngine;
using System.Collections;
using System;

public class DataManager : DataManagerBase<DataManager> {
	public enum USER_PARAM{
		NONE		= 0,
		COIN		,		// コイン
		TICKET		,		// チケット
		POPULARITY	,		// 人気

		MONEY		,		// 実際のお金
		MAX			,
	}

	private DataUser m_dataUser = new DataUser();
	public DataUser user {
		get{ return m_dataUser; }
		set{ m_dataUser = value; }
	}

	private MasterCollection m_masterCollection = new MasterCollection ();
	public MasterCollection masterCollection{
		get{ return m_masterCollection; }
		set{ m_masterCollection = value; }
	}

	private DataCollection m_dataCollection = new DataCollection();
	public DataCollection dataCollection
	{
		get { return m_dataCollection; }
		set { m_dataCollection = value; }
	}

	private MasterAchievement m_masterAchievement = new MasterAchievement();
	public MasterAchievement masterAchevement
	{
		get { return m_masterAchievement; }
		set { m_masterAchievement = value; }
	}
	private DataAchievement m_dataAchievement = new DataAchievement();
	public DataAchievement dataAchievement
	{
		get{ return m_dataAchievement; }
		set { m_dataAchievement = value; }
	}

	public bool m_bRecoveryWait;
	public void initialRecoveryCoin()
	{
		TimeSpan ts = TimeManager.Instance.GetDiffNow(user.recoveryTime);
		//Debug.LogError(user.recoveryTime);
		//Debug.LogError(ts.TotalSeconds);
		if (ts.TotalSeconds < 0)
		{
			if (user.coin < DataUser.DefaultCoin)
			{
				user.coin = DataUser.DefaultCoin;
			}
			m_bRecoveryWait = false;
		}
		else
		{
			m_bRecoveryWait = true;
			Invoke("invokeRecoveryCoin", (float)ts.TotalSeconds);
		}
	}

	public void updateUserCoin( int _iCoin)
	{
		//Debug.LogError(string.Format("updateUserCoin:{0}", _iCoin));
		if (m_bRecoveryWait)
		{
			if(DataUser.DefaultCoin < _iCoin)
			{
				CancelInvoke("invokeRecoveryCoin");
				m_bRecoveryWait = false;
			}
		}
		else
		{
			if( _iCoin < DataUser.DefaultCoin)
			{
				float fAddSeconds = (float)(60 * 60);
				user.recoveryTime = TimeManager.StrGetTime((int)fAddSeconds);

				Debug.LogError(user.recoveryTime);
				Invoke("invokeRecoveryCoin", fAddSeconds);
				m_bRecoveryWait = true;
			}
		}

	}
	
	private void invokeRecoveryCoin()
	{
		if( user.coin < DataUser.DefaultCoin)
		{
			user.coin = DataUser.DefaultCoin;
		}
		m_bRecoveryWait = false;
	}

	public override void Initialize ()
	{
		base.Initialize ();
		m_dataKvs.Load(DataKvs.FILE_NAME);
		if( m_dataUser.Load (DataUser.FILENAME) == false )
		{
			;// 一度データを作ったりした方がいいかも
		}
		user.UpdateCoin.AddListener(updateUserCoin);
		initialRecoveryCoin();
		m_dataAchievement.LoadMulti(DataAchievement.FILENAME);
		m_dataCollection.LoadMulti(DataCollection.FILENAME);
		m_masterCollection.LoadMulti (MasterCollection.FILENAME);
		m_masterAchievement.LoadMulti(MasterAchievement.FILENAME);

		SpriteManager.Instance.LoadAtlas("texture/collection/kimodameshi");
		SpriteManager.Instance.LoadAtlas("texture/collection/zoo");

	}

	private void dataSave()
	{
#if UNITY_EDITOR
		Debug.LogError ("save");
#endif
		dataAchievement.Save(DataAchievement.FILENAME);
		dataCollection.Save(DataCollection.FILENAME);
		user.Save(DataUser.FILENAME);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		//Debug.LogError(string.Format("OnApplicationPause:{0}", pauseStatus));
		if (pauseStatus)
		{
			dataSave();
		}
		else
		{

		}
	}
#if UNITY_EDITOR
	public bool m_bSave = false;
	void Update()
	{
		if( m_bSave)
		{
			m_bSave = false;
			dataSave();
		}

	}
#endif

}






