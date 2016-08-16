using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class CollectionBanner : MonoBehaviour {

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private Text m_txtName;

	[SerializeField]
	private CtrlRareStars m_ctrlRarestars;

	[SerializeField]
	private CtrlUserParam m_ctrlUserParam;

	public MasterCollectionParam m_masterParam;

	public void Initialize( MasterCollectionParam _param ){
		m_masterParam = _param;

		m_txtName.text = _param.name;
		m_imgIcon.sprite = SpriteManager.Instance.LoadSprite (MasterCollection.GetSpriteName (_param.collection_id));
		m_ctrlRarestars.Initialize (_param.rarity);
		m_ctrlUserParam.SetNum (_param.price_type, _param.price);

		gameObject.GetComponent<Button> ().onClick.AddListener (onClick);

	}

	public UnityEventInt OnSelect;// (int _iCollectionId);

	private void onClick(){
		OnSelect.Invoke (m_masterParam.collection_id);
	}



}
