using UnityEngine;
using System.Collections;
using System;

public class DataUserParam : CsvDataParam{

}

public class DataUser : DataKvs {

	public const string FILENAME = "data/user";

	public const string KEY_COIN = "coin";
	public const string KEY_TICKET = "ticket";
	public const string KEY_RECOVERY_TIME = "recovery_time";

	public const int DefaultCoin = 1000;

	protected override void preSave()
	{
		WriteInt(KEY_COIN,coin);
		WriteInt(KEY_TICKET,ticket);
		Write(KEY_RECOVERY_TIME, recoveryTime);
	}

	public string recoveryTime
	{
		get { return m_strRecoveryTime; }
		set
		{
			m_strRecoveryTime = value;
		}
	}
	private string m_strRecoveryTime;
	public bool m_bRecoveryWait = false;

	public override bool Load (string _strFilename)
	{
		bool bRet = base.Load (_strFilename);
		if (HasKey(KEY_COIN)) {
			m_iCoin = ReadInt(KEY_COIN);
		}
		else
		{
			m_iCoin = DefaultCoin;
		}
		//Debug.LogError( string.Format("coin={0}", m_iCoin));
		m_iTicket = ReadInt (KEY_TICKET);

		if (HasKey(KEY_RECOVERY_TIME))
		{
			m_strRecoveryTime = Read(KEY_RECOVERY_TIME);
		}
		else
		{
			m_strRecoveryTime = TimeManager.StrGetTime();
		}

		return bRet;
	}
	
	public string uid{ get; set; }
	public UnityEventInt UpdateCoin = new UnityEventInt ();

	protected int m_iCoin;
	public int coin{
		get{
			return m_iCoin;
		}
		set{
			m_iCoin = value;
			UpdateCoin.Invoke (m_iCoin);
		}
	}

	public UnityEventInt UpdateTicket = new UnityEventInt ();
	protected int m_iTicket;
	public int ticket{
		get{
			return m_iTicket;
		}
		set{
			m_iTicket = value;
			UpdateTicket.Invoke (m_iTicket);
		}
	}



}
