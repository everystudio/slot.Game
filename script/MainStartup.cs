using UnityEngine;
using System.Collections;

public class MainStartup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SoundManager.Instance.PlayBGM("game_maoudamashii_5_casino04", "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/5_4/bgm/casino");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
