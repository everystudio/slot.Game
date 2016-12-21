using UnityEngine;
using System.Collections;

public class OffsetUI : MonoBehaviour {

	public float offset_x;
	public float offset_y;

	// Use this for initialization
	void Start () {
		//if (2000 < Screen.width)
		{
			Vector3 pos = gameObject.transform.localPosition;
			pos = new Vector3(pos.x + offset_x, pos.y + offset_y, pos.z);
			gameObject.transform.localPosition = pos;
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
