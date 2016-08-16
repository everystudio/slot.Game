using UnityEngine;
using System.Collections;

public class DataUserParam : CsvDataParam{

}

public class DataUser : DataKvs {

	public const string FILENAME = "data/user";

	public const string KEY_COIN = "coin";
	public const string KEY_TICKET = "ticket";

	public override bool Load (string _strFilename)
	{
		bool bRet = base.Load (_strFilename);

		m_iCoin = ReadInt (KEY_COIN);
		m_iTicket = ReadInt (KEY_TICKET);
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
