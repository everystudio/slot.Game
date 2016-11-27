using UnityEngine;
using System.Collections;

public class MasterAchievementParam : CsvDataParam
{

	public int id { get; set; }
	public string title { get; set; }
	public string key { get; set; }
	public int point { get; set; }
	public int count { get; set; }
	public string symbol { get; set; }
	public int line_num { get; set; }
	public int collection_num { get; set; }


}

public class MasterAchievement : CsvData<MasterAchievementParam> {
#if UNITY_ANDROID
	public const string FILENAME = "master/achievement_and";
#elif UNITY_IOS
#else
#endif

}
