using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterCollectionParam : CsvDataParam{

	public int collection_id{ get; set ;}
	public string type { get; set ;}
	public string type_sub{ get; set ;}
	public string name{ get; set ;}
	public int rarity{ get; set ;}
	public string price_type { get; set ;}
	public int price{ get; set ;}
	public string flavor{ get; set; }
}

public class MasterCollection : CsvData<MasterCollectionParam> {
	public const string FILENAME = "master/collection";
	Dictionary<int, MasterCollectionParam> dict = new Dictionary<int, MasterCollectionParam> ();

	public MasterCollectionParam Get( int _iCollectionId ){
		MasterCollectionParam ret;
		if (dict.TryGetValue (_iCollectionId , out ret )) {
			return ret;
		}
		return new MasterCollectionParam ();
	}

	public override bool LoadMulti (string _strFilename)
	{
		bool bRet = base.LoadMulti (_strFilename);
		dict.Clear ();
		if (bRet) {
			foreach (MasterCollectionParam param in list) {
				dict.Add (param.collection_id, param);
			}
		}
		return bRet;
	}

	static public string GetSpriteName( int _iCollectionId ){
		return string.Format ("texture/collection/col_{0:D5}", _iCollectionId);
	}

}
