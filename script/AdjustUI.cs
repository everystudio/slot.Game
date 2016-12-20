using UnityEngine;
using System.Collections;

public class AdjustUI : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if( 2000 < Screen.width)
		{
			gameObject.transform.localScale *= 0.5f;
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
