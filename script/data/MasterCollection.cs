using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterCollectionParam : CsvDataParam{

	public int collection_id{ get; set ;}
	public string type { get; set ;}
	public string type_sub{ get; set ;}
	public string name{ get; set ;}
	public string name_en{ get; set ; }
	public string filename{ get; set ; }
	public int rarity{ get; set ;}
	public string price_type { get; set ;}
	public int price{ get; set ;}
	public string flavor{ get; set; }
	public string flavor_en { get; set; }
}

public class MasterCollection : CsvData<MasterCollectionParam> {
	public const string FILENAME = "master/collection";
	Dictionary<int, MasterCollectionParam> dict = new Dictionary<int, MasterCollectionParam> ();

	public List<MasterCollectionParam> GetShopCollection( int _iNum)
	{
		List<MasterCollectionParam> retList = new List<MasterCollectionParam>();
		List<MasterCollectionParam> tempList = GetNotCollected();
		int iCount = tempList.Count;
		int[] iArr = new int[iCount];
		for( int i = 0; i < iCount; i++)
		{
			iArr[i] = 100;
		}

		int iLoopCount = iCount < _iNum ? iCount : _iNum;
		for( int i = 0; i < iLoopCount; i++)
		{
			int iIndex = UtilRand.GetIndex(iArr);
			retList.Add(tempList[iIndex]);
			iArr[iIndex] = 0;
		}

		return retList;
	}
	public List<MasterCollectionParam> GetNotCollected()
	{
		List<MasterCollectionParam> retList = new List<MasterCollectionParam>();

		foreach( MasterCollectionParam param in list)
		{
			if (DataManager.Instance.dataCollection.NotCollected(param.collection_id))
			{
				retList.Add(param);
			}
		}
		return retList;
	}

	public bool Collected(int _collectionId)
	{
		if (DataManager.Instance.dataCollection.Collected(_collectionId))
		{
			return true;
		}
		return false;
	}

	public bool NotCollected( int _collectionId)
	{
		if (DataManager.Instance.dataCollection.NotCollected(_collectionId))
		{
			return true;
		}
		return false;
	}

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

	static public string GetSpriteName( string _filename ){
		return string.Format ("texture/collection/{0:D5}", _filename);
	}

}
