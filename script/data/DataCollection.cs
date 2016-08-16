using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCollectionParam : CsvDataParam{
	public int collection_id{ get; set ;}
	public int status{ get; set;}
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
}
