using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataAchievementParam : CsvDataParam
{
	public int achievement_id { get; set; }
	public int status { get; set; }
	public enum STATUS
	{
		UNKNOWN = 0,
		NOT_COLLECTED,
		COLLECTED,
		MAX,
	}
}

public class DataAchievement : CsvData<DataAchievementParam> {

	public const string FILENAME = "data/achievement";
	Dictionary<int, DataAchievementParam> dict = new Dictionary<int, DataAchievementParam>();

	public override bool LoadMulti(string _strFilename)
	{
		bool bRet = base.LoadMulti(_strFilename);
		dict.Clear();
		if (bRet)
		{
			foreach (DataAchievementParam param in list)
			{
				dict.Add(param.achievement_id, param);
			}
		}
		return bRet;
	}

	public bool Collected(int _iAchevementId)
	{
		DataAchievementParam param;
		if (dict.TryGetValue(_iAchevementId, out param))
		{
			if (param.status == (int)DataAchievementParam.STATUS.COLLECTED)
			{
				return true;
			}
		}
		return false;
	}

	public bool NotCollected(int _iAchevementId)
	{
		DataAchievementParam param;
		if (dict.TryGetValue(_iAchevementId, out param))
		{
			if (param.status != (int)DataAchievementParam.STATUS.COLLECTED)
			{
				return true;
			}
		}
		else
		{
			return true;
		}
		return false;
	}

}
