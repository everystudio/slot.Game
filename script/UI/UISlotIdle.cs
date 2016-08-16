using UnityEngine;
using System.Collections;

public class UISlotIdle : CPanel {
	protected override void panelStart ()
	{
		base.panelStart ();
	}

	public void onMainMenu(){
		UIAssistant.main.ShowPage ("MainMenu");
	}

}
