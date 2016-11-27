using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WindowShowCollection : CPanel {

	public enum DISP_LIST
	{
		NONE		= 0,
		COLLECTED	,
		NOT			,
		ALL			,
		MAX			,
	}

	[SerializeField]
	private GameObject m_goContent;

	[SerializeField]
	private List<CollectionBanner> m_CollectionBannerList = new List<CollectionBanner>();

	[SerializeField]
	private CollectionDetail m_collectionDetail;

	[SerializeField]
	private Button m_btnCollected;

	[SerializeField]
	private Button m_btnNotCollected;

	[SerializeField]
	private Button m_btnAll;

	private DISP_LIST m_eDispList;

	private void updateList(DISP_LIST _eDispList )
	{
		if( _eDispList != m_eDispList)
		{
			m_eDispList = _eDispList;
		}
		else
		{
			// 同じものはここではじく
			return;
		}

		foreach (CollectionBanner banner in m_CollectionBannerList)
		{
			Destroy(banner.gameObject);
		}
		m_CollectionBannerList.Clear();

		foreach (MasterCollectionParam param in DataManager.Instance.masterCollection.list)
		{
			Debug.LogError(string.Format("name:{0}", param.name));
			switch ( _eDispList)
			{
				case DISP_LIST.COLLECTED:
					if(DataManager.Instance.masterCollection.Collected(param.collection_id))
					{
						break;
					}
					else
					{
						continue;
					}
				case DISP_LIST.NOT:
					if (DataManager.Instance.masterCollection.NotCollected(param.collection_id))
					{
						break;
					}
					else
					{
						continue;
					}
				case DISP_LIST.ALL:
					break;
				default:
					continue;
			}


			CollectionBanner banner = PrefabManager.Instance.MakeScript<CollectionBanner>("prefab/PrefCollectionBanner", m_goContent);
			banner.Initialize(param);

			banner.OnSelect.AddListener(m_collectionDetail.Initialize);
			m_CollectionBannerList.Add(banner);
		}
		m_collectionDetail.Initialize(DataManager.Instance.masterCollection.list[0].collection_id);



	}

	public void onCollected()
	{
		updateList(DISP_LIST.COLLECTED);
	}
	public void onNotCollected()
	{
		updateList(DISP_LIST.NOT);
	}
	public void onAll()
	{
		updateList(DISP_LIST.ALL);
	}

	protected override void panelStart ()
	{
		base.panelStart ();

		// 職階表示用にNONEを代入
		m_eDispList = DISP_LIST.NONE;

		updateList(DISP_LIST.COLLECTED);

	}

}
