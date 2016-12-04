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
		//_sprite.sprite.textureRect.width = (int)_sprite.sprite2D.textureRect.width;
		/*
		_sprite.sprite.textureRect.width = (int)_sprite.sprite2D.textureRect.width;
		_sprite.height = (int)_sprite.sprite2D.textureRect.height;
		float set_size = 120.0f;

		if (_sprite.width < _sprite.height)
		{
			float rate = set_size / (float)_sprite.height;
			_sprite.width = (int)(_sprite.width * rate);
			_sprite.height = (int)set_size;
		}
		else {
			float rate = set_size / (float)_sprite.width;
			_sprite.width = (int)set_size;
			_sprite.height = (int)(_sprite.height * rate);
		}
		*/


		return;
	}


	public void Initialize( MasterCollectionParam _param ){
		m_masterParam = _param;

		m_txtName.text = _param.name;

		//utilswitchsprite.SetSprite(MasterCollection.GetSpriteName(_param.filename));
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

		/*
		Debug.LogError(string.Format("x:{0} y:{1}", m_imgIcon.sprite.rect.width , m_imgIcon.sprite.bounds.));
		Debug.LogError(string.Format("x:{0} y:{1}", m_imgIcon.sprite.textureRect.width, m_imgIcon.sprite.textureRect.height));
		*/
		m_ctrlRarestars.Initialize (_param.rarity);
		m_ctrlUserParam.SetNum (_param.price_type, _param.price);

		gameObject.GetComponent<Button> ().onClick.AddListener (onClick);

	}

	public UnityEventInt OnSelect;// (int _iCollectionId);

	private void onClick(){
		OnSelect.Invoke (m_masterParam.collection_id);
	}



}
