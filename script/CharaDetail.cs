using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaDetail : MonoBehaviour {

	[SerializeField]
	Image m_imgSymbol;

	[SerializeField]
	Text m_txtName;

	[SerializeField]
	Text m_txtDesc;

	public void Initialize( Sprite _sprSymbol , string _strName , string _strDesc)
	{
		m_imgSymbol.sprite = _sprSymbol;
		m_txtName.text = _strName;
		m_txtDesc.text = _strDesc;
	}

	public void OnPushClose()
	{
		gameObject.SetActive(false);
	}
	


}
