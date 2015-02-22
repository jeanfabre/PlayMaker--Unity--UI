using System;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{

	/// <summary>
	/// Options to define an event target
	/// </summary>
	public enum ProxyEventTarget {Owner,GameObject,BroadCastAll,FsmComponent};

	/// <summary>
	/// Defines a PlayMaker Event target. Use this if you want to define several events to send for one target, else use PlayMakerEvent class.
	/// Use this class in your Components public interface, The Unity Inspector will use a specific PropertyDrawer is defined
	/// </summary>
	[Serializable]
	public class PlayMakerEventTarget{

		public ProxyEventTarget eventTarget;
		public GameObject gameObject;
		public bool includeChildren;
		public PlayMakerFSM fsmComponent;
	}

}