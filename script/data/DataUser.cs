using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataUserParam : CsvDataParam{

}

public class ScoreHistory
{
	public ScoreHistory(int _iGame , int _iScore)
	{
		game = _iGame;
		score = _iScore;
	}
	public int game;
	public int score;
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

		// 自分に関係ある未取得の実績を取得
		List<MasterAchievementParam> checkAchievementList = DataManager.Instance.GetNoAchievementForSlot(m_strSymbolName);

		if( 0 < checkAchievementList.Count)
		{
			foreach(MasterAchievementParam param in checkAchievementList)
			{
				if( param.count <= m_iNum)
				{
					DataManager.Instance.Achieve(param.id , param.key , param.title);
				} 
			}
		}

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
	public const string KEY_TOTAL_SPIN_NUM = "total_spin_num";
	public const string KEY_HIGHSCORE_30GAME = "highscore_30games";

	public const string KEY_SYMBOL_CHERRY = "Cherry";
	public const string KEY_SYMBOL_JACK = "J";
	public const string KEY_SYMBOL_QUEEN = "Queen";
	public const string KEY_SYMBOL_KING = "King";
	public const string KEY_SYMBOL_BAR = "Bar";
	public const string KEY_SYMBOL_SEVEN = "7";
	public const string KEY_SYMBOL_ACE = "A";
	public const string KEY_SYMBOL_JURE = "Jure";
	public const string KEY_SYMBOL_WILD = "Wild";

	public const int DefaultCoin = 1000;

	protected override void preSave()
	{
		WriteInt(KEY_COIN, coin);
		WriteInt(KEY_TICKET, ticket);
		Write(KEY_RECOVERY_TIME, recoveryTime);
		WriteInt(KEY_TOTAL_SPIN_NUM, totalSpinNum);

		foreach (SymbolCount symCount in m_symbolCountList)
		{
			symCount.Save(this);
		}

		int iNum = 0;
		foreach( int iScore in m_scoreHistoryQueue)
		{
			WriteInt(GetGameScoreName(iNum), iScore);
			iNum += 1;
		}
	}

	public long m_lScore30GameHigh;
	public long m_lScore30Game;

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

	public string GetGameScoreName(int _iGame)
	{
		return string.Format("game_{0:D2}", _iGame);
	}

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
		if (HasKey(KEY_HIGHSCORE_30GAME))
		{
			m_lScore30GameHigh = long.Parse(Read(KEY_HIGHSCORE_30GAME));
		}
		else
		{
			m_lScore30GameHigh = 0;
		}

		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_CHERRY, this));

		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_CHERRY,this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_JACK,this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_QUEEN, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_KING, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_SEVEN, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_ACE, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_JURE, this));
		m_symbolCountList.Add(new SymbolCount(KEY_SYMBOL_WILD, this));

		m_lScore30Game = 0;
		for ( int i = 0; i < 30; i++)
		{
			//m_scoreHistoryStack.Push(new ScoreHistory(i, ReadInt(GetGameScoreName(i))));
			int score = ReadInt(GetGameScoreName(i));
			m_scoreHistoryQueue.Enqueue( score );
			m_lScore30Game += (long)score;
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

	public UnityEventInt UpdateTotalSpinNum = new UnityEventInt ();
	protected int m_iTotalSpinNum;
	public int totalSpinNum
	{
		get
		{
			return m_iTotalSpinNum;
		}
		set
		{
			m_iTotalSpinNum = value;
			UpdateTotalSpinNum.Invoke(m_iTotalSpinNum);
		}
	}


	public List<SymbolCount> m_symbolCountList = new List<SymbolCount>();
	public void AddSymbolCount(string _strSymbolName)
	{
		foreach (SymbolCount symCount in m_symbolCountList)
		{
			if( symCount.AddCount(_strSymbolName , 1))
			{
				return;
			}
		}
		return;
	}
	public Queue<int> m_scoreHistoryQueue = new Queue<int>();

	[SerializeField]
	private int m_iScoreThisgame;

	public void ScoreReset()
	{
		//Debug.LogError(m_iScoreThisgame);
		m_iScoreThisgame = 0;
	}
	public void ScoreUp( int _iAddScore)
	{
		m_iScoreThisgame += _iAddScore;
	}
	public void ScoreNext()
	{
		m_scoreHistoryQueue.Dequeue();
		m_scoreHistoryQueue.Enqueue(m_iScoreThisgame);

		m_lScore30Game = 0;
		foreach( int score in m_scoreHistoryQueue)
		{
			m_lScore30Game += (long)score;
		}

		if(m_lScore30GameHigh < m_lScore30Game)
		{
			Debug.LogError(string.Format("update high score:{0} pre hi{1}", m_lScore30Game, m_lScore30GameHigh));
			AchievementManager.Instance.RegisterRanking(DataManager.LEADERBOARD_ID_30GAME, m_lScore30Game);
			m_lScore30GameHigh = m_lScore30Game;
			Write(KEY_HIGHSCORE_30GAME, m_lScore30GameHigh.ToString());
		}

		//Debug.LogError(m_iScoreThisgame);
	}

}
