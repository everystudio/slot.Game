using UnityEngine;
using System.Collections;

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

	public override void Initialize ()
	{
		base.Initialize ();
		m_dataUser.Load (DataUser.FILENAME);
		m_masterCollection.LoadMulti (MasterCollection.FILENAME);
	}

}






