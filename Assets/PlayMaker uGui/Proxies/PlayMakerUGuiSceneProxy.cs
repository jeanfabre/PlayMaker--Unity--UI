using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;

public class PlayMakerUGuiSceneProxy : MonoBehaviour {


	public static PlayMakerFSM fsm;

	// Use this for initialization
	void Start () {

		if (fsm != null)
		{
			Destroy (this.gameObject);
			return;
		}

		PlayMakerUGuiSceneProxy.fsm = GetComponent<PlayMakerFSM>();
	}

}
