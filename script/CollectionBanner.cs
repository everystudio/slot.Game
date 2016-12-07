using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class CollectionBanner : MonoBehaviour {

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private UtilSwitchSprite utilswitchsprite;

	[SerializeField]
	private Text m_txtName;

	[SerializeField]
	private CtrlRareStars m_ctrlRarestars;

	[SerializeField]
	private CtrlUserParam m_ctrlUserParam;

	public MasterCollectionParam m_masterParam;


	protected void SpriteIconAdjust(Image _sprite)
	{
		return;
	}


	public void Initialize( MasterCollectionParam _param ){
		m_masterParam = _param;
		m_txtName.text = _param.name;


		m_imgIcon.sprite = SpriteManager.Instance.LoadSprite (MasterCollection.GetSpriteName(_param.filename));

		float fOriginSize = 100.0f;
		float fScale = 1.0f;
		if(m_imgIcon.sprite.textureRect.width < m_imgIcon.sprite.textureRect.height)
		{
			fScale = fOriginSize / m_imgIcon.sprite.textureRect.height;
		}
		else
		{
			fScale = fOriginSize / m_imgIcon.sprite.textureRect.width;
		}
		m_imgIcon.rectTransform.sizeDelta = new Vector2(m_imgIcon.sprite.textureRect.width* fScale, m_imgIcon.sprite.textureRect.height* fScale);
		m_ctrlRarestars.Initialize (_param.rarity);
		m_ctrlUserParam.SetNum (_param.price_type, _param.price);

		gameObject.GetComponent<Button> ().onClick.AddListener (onClick);
	}

	public UnityEventInt OnSelect;
	private void onClick(){
		OnSelect.Invoke (m_masterParam.collection_id);
	}



}
