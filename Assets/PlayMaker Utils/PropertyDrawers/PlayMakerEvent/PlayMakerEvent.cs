using System;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{

	/// <summary>
	/// PlayMaker Event. Use this class in your Components public interface. The Unity Inspector will use the related PropertyDrawer.
	/// It lets user easily choose a PlayMaker Event
	/// 
	/// If there is no attribute "EventTargetVariable" define, the list of events will be all the PlayMaker global events
	/// 
	/// If the attribute "EventTargetVariable" is defined, the PlayMakerEventTarget variable will be used for the context
	///  the list of events will adapt, and warn the user if the selected event is indeed implemented on the target
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
		public bool allowLocalEvents;

		/// <summary>
		/// The default name of the event.
		/// </summary>
		public string defaultEventName;


		public PlayMakerEvent(){}

		public PlayMakerEvent(string defaultEventName)
		{
			this.defaultEventName = defaultEventName;
			this.eventName = defaultEventName;
		}

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