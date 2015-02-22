using System;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{

	/// <summary>
	/// Defines a PlayMaker Event and what is the target to send it.
	/// Use this class in your Components public interface, The Unity Inspector will use a specific PropertyDrawer is defined
	/// </summary>
	[Serializable]
	public class PlayMakerEvent{

		/// <summary>
		/// The name of the event.
		/// </summary>
		public string eventName;

		/// <summary>
		/// Store here a user setting, instead of in the PropertyDrawer
		/// Switch between showing global or local events to keep it as choosen by the user.
		/// </summary>
		public bool allowLocalEvents = false;

		public bool SendEvent(PlayMakerFSM fromFsm,PlayMakerEventTarget eventTarget)
		{
			//Debug.Log("Sending event <"+eventName+"> from fsm:"+fromFsm.FsmName+" "+eventTarget.eventTarget+" "+eventTarget.gameObject+" "+eventTarget.fsmComponent);

			if (eventTarget.eventTarget == ProxyEventTarget.BroadCastAll)
			{
				fromFsm.SendEvent(eventName);
			}else if (eventTarget.eventTarget == ProxyEventTarget.Owner || eventTarget.eventTarget == ProxyEventTarget.GameObject)
			{
				PlayMakerUtils.SendEventToGameObject(fromFsm,eventTarget.gameObject,eventName,eventTarget.includeChildren);
			}else if (eventTarget.eventTarget == ProxyEventTarget.FsmComponent)
			{
				eventTarget.fsmComponent.SendEvent(eventName);
			}

			return true;
		}
	}

}