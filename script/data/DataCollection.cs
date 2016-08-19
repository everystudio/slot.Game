using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCollectionParam : CsvDataParam{
	public int collection_id{ get; set ;}
	public int status{ get; set;}

	public enum STATUS
	{
		UNKNOWN			= 0,
		NOT_COLLECTED	,
		COLLECTED		,
		MAX				,
	}
}

public class DataCollection : CsvData<DataCollectionParam> {

	public const string FILENAME = "data/collection";
	Dictionary<int, DataCollectionParam> dict = new Dictionary<int, DataCollectionParam> ();

	public override bool LoadMulti (string _strFilename)
	{
		bool bRet = base.LoadMulti (_strFilename);
		dict.Clear ();
		if (bRet) {
			foreach (DataCollectionParam param in list) {
				dict.Add (param.collection_id, param);
			}
		}
		return bRet;
	}

	public bool Collected( int _iCollectionId)
	{
		DataCollectionParam param;
		if( dict.TryGetValue( _iCollectionId , out param))
		{
			if( param.status == (int)DataCollectionParam.STATUS.COLLECTED)
			{
				return true;
			}
		}
		return false;
	}

	public bool NotCollected(int _iCollectionId)
	{
		DataCollectionParam param;
		if (dict.TryGetValue(_iCollectionId, out param))
		{
			if (param.status != (int)DataCollectionParam.STATUS.COLLECTED)
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
