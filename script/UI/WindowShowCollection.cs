using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowShowCollection : CPanel {

	[SerializeField]
	private GameObject m_goContent;

	[SerializeField]
	private List<CollectionBanner> m_CollectionBannerList = new List<CollectionBanner>();

	[SerializeField]
	private CollectionDetail m_collectionDetail;

	protected override void panelStart ()
	{
		base.panelStart ();

		foreach (CollectionBanner banner in m_CollectionBannerList) {
			Destroy (banner.gameObject);
		}
		m_CollectionBannerList.Clear ();

		foreach (MasterCollectionParam param in DataManager.Instance.masterCollection.list) {

			CollectionBanner banner = PrefabManager.Instance.MakeScript<CollectionBanner> ("prefab/PrefCollectionBanner", m_goContent);
			banner.Initialize (param);

			banner.OnSelect.AddListener (m_collectionDetail.Initialize);
		}
		m_collectionDetail.Initialize (DataManager.Instance.masterCollection.list [0].collection_id);
	}

}
