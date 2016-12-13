using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataUserParam : CsvDataParam{

}

public class SymbolCount
{
	public SymbolCount(string _strSymbolName,DataKvs _dataKvs)
	{
		m_strSymbolName = _strSymbolName;
		m_iNum = _dataKvs.ReadInt(m_strSymbolName);
	}

	public bool AddCount(string _strSymbolName , int _iAddNum)
	{
		if(m_strSymbolName.Equals(_strSymbolName))
		{
			m_iNum += _iAddNum;
		}
		else
		{
			return false;
		}

		// 対象シンボルのアチーブメントを調べる

		return true;
	}
	public void Save(DataKvs _dataKvs)
	{
		_dataKvs.WriteInt(m_strSymbolName, m_iNum);
	}

	public string m_strSymbolName;
	public int m_iNum;

}

public class DataUser : DataKvs {

	public const string FILENAME = "data/user";

	public const string KEY_COIN = "coin";
	public const string KEY_TICKET = "ticket";
	public const string KEY_RECOVERY_TIME = "recovery_time";

	public const string KEY_SYMBOL_CHERRY = "Cherry";
	public const string KEY_SYMBOL_JACK = "J";
	public const string KEY_SYMBOL_QUEEN = "Queen";
	public const string KEY_SYMBOL_KING = "King";
	public const string KEY_SYMBOL_BAR = "Bar";
	public const string KEY_SYMBOL_SEVEN = "7";
	public const string KEY_SYMBOL_ACE = "A";

	public const int DefaultCoin = 1000;

	protected override void preSave()
	{
		WriteInt(KEY_COIN, coin);
		WriteInt(KEY_TICKET, ticket);
		Write(KEY_RECOVERY_TIME, recoveryTime);
		foreach (SymbolCount symCount in m_symbolCountList)
		{
			symCount.Save(this);
		}
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

		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_CHERRY,this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_JACK,this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_QUEEN, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_KING, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_SEVEN, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_ACE, this));

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

	public List<SymbolCount> m_symbolCountList = new List<SymbolCount>();
	public void AddSymbolCount(string _strSymbolName)
	{
		foreach (SymbolCount symCount in m_symbolCountList)
		{
			if( symCount.AddCount(_strSymbolName , 1))
			{
				break;
			}
		}
	}



}
