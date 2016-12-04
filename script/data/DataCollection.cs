using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCollectionParam : CsvDataParam{
	public int collection_id{ get; set ;}
	public int status{ get; set;}
	public int num { get; set; }

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

	public bool GetCollection(int _iCollectionId , out DataCollectionParam _param)
	{
		bool bRet = false;
		bRet = dict.TryGetValue(_iCollectionId, out _param);
		return bRet;
	}

	public void AddCollection( int _iCollectionId , int _iAddNum)
	{
		DataCollectionParam param;
		
		if( GetCollection(_iCollectionId,out param))
		{
			param.num += _iAddNum;
			int iStatus = (int)DataCollectionParam.STATUS.COLLECTED;
			if( 0 < param.num)
			{
				iStatus = (int)DataCollectionParam.STATUS.COLLECTED;
			}
			else
			{
				iStatus = (int)DataCollectionParam.STATUS.NOT_COLLECTED;
			}
			param.status = iStatus;
		}
		else
		{
			param = new DataCollectionParam();
			param.collection_id = _iCollectionId;
			param.num = _iAddNum;
			param.status = (int)DataCollectionParam.STATUS.COLLECTED;
			list.Add(param);
			dict.Add(_iCollectionId, param);
		}
	}

	public override bool LoadMulti (string _strFilename)
	{
		bool bRet = base.LoadMulti (_strFilename);
		dict.Clear ();
		if (bRet) {
			foreach (DataCollectionParam param in list) {
				dict.Add (param.collection_id, param);
			}
		}
		Debug.LogError(string.Format("{0}:{1}", list.Count, dict.Count));
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
