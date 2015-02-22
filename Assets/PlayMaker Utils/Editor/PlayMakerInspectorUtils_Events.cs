//	(c) Jean Fabre, 2015 All rights reserved.
//	http://www.fabrejean.net
//  contact: http://www.fabrejean.net/contact.htm


using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using HutongGames.PlayMaker;
using HutongGames.PlayMakerEditor;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	public partial class PlayMakerInspectorUtils {


		public static bool DoesTargetImplementsEvent(PlayMakerFSM fsm, string fsmEvent)
		{

			foreach(FsmTransition _transition in fsm.FsmGlobalTransitions)
			{
				if (_transition.EventName.Equals(fsmEvent))
				{
					return true;
				}
			}
			
			foreach(FsmState _state in fsm.FsmStates)
			{
				
				foreach(FsmTransition _transition in _state.Transitions)
				{
					
					if (_transition.EventName.Equals(fsmEvent))
					{
						return true;
					}
				}
			}
			
			return false;
		}

		public static bool DoesTargetImplementsEvent(GameObject target,string fsmEvent,bool includeChildren)
		{
			PlayMakerFSM[] _list = includeChildren?target.GetComponentsInChildren<PlayMakerFSM>(true):target.GetComponents<PlayMakerFSM>();
			foreach(PlayMakerFSM _fsm in _list)
			{
				if (DoesTargetImplementsEvent(_fsm,fsmEvent))
				{
					return true;
				}
			}

			return false;
		}


		/// <summary>
		/// Gets the implemented global events.
		/// </summary>
		/// <returns>The implemented global events.</returns>
		/// <param name="fromFsm">The fsm to look for implemented global events.</param>
		/// <param name="includeNone">If set to <c>true</c> include none. Useful for popup to select an event or not</param>
		public static string[] GetImplementedGlobalEvents(PlayMakerFSM fromFsm,bool includeNone = false)
		{
			List<string> list = new List<string>();

			if (includeNone)
			{
				list.Add ("none");
			}

			if (fromFsm!=null)
			{
				// global transitions events, actually implemented in that fsm
				foreach(var _globaltransition in fromFsm.FsmGlobalTransitions)
				{
					var _event = _globaltransition.FsmEvent;
					//Debug.Log(_event.Name +", is global: "+_event.IsGlobal);
					if (_event.IsGlobal)
					{
						list.Add(_event.Name);
					}
				}
			}
			return list.ToArray();
		}

		/// <summary>
		/// Gets the global events list. 
		/// </summary>
		/// <returns>The global events.</returns>
		/// <param name="includeNone">If set to <c>true</c> include a "none" option. Useful for popup to select an event or not</param>
		public static string[] GetGlobalEvents(bool includeNone = false)
		{
			//List<string> list = new List<string>() ;
			string[] list = PlayMakerGlobals.Instance.Events.ToArray();
			/*
			foreach(string _event in PlayMakerGlobals.Instance.Events)
			{
			//	Debug.Log(_event);
				if (!string.IsNullOrEmpty(_event) &&  !string.Equals("none",_event) )
				{
					list.Add(_event);
				}
			}
*/
			if (includeNone)
			{
				//list.Insert(0,"none");
				ArrayUtility.Insert<string>(ref list,0,"none");
			}

			return list;//.ToArray();
		}

		/*
		public static void GetFsmEvents(PlayMakerFSM fromFsm,bool includeNone = false)
		{
			if (fromFsm==null)
			{
				return;
			}
			
			Debug.Log("fsm events ( found in the events tab, not necessarly used, warning");
			foreach(var _event in fromFsm.FsmEvents)
			{
				Debug.Log(_event.Name +", is global: "+_event.IsGlobal);
			}
			
			Debug.Log("global transitions events, actually implemented in that fsm");
			foreach(var _globaltransition in fromFsm.FsmGlobalTransitions)
			{
				var _event = _globaltransition.FsmEvent;
				Debug.Log(_event.Name +", is global: "+_event.IsGlobal);
			}
			
			Debug.Log("global events, within this project");
			foreach(var name in PlayMakerGlobals.Instance.Events)
			{
				Debug.Log(name);
			}
			
		}
*/
		/*
		public static void GetFsmEvents(PlayMakerFSM fromFsm)
		{
			if (fromFsm==null)
			{
				return;
			}
			
			Debug.Log("fsm events ( found in the events tab, not necessarly used, warning");
			foreach(var _event in fromFsm.FsmEvents)
			{
				Debug.Log(_event.Name +", is global: "+_event.IsGlobal);
			}

			Debug.Log("global transitions events, actually implemented in that fsm");
			foreach(var _globaltransition in fromFsm.FsmGlobalTransitions)
			{
				var _event = _globaltransition.FsmEvent;
				Debug.Log(_event.Name +", is global: "+_event.IsGlobal);
			}
			
			Debug.Log("global events, within this project");
			foreach(var name in PlayMakerGlobals.Instance.Events)
			{
				Debug.Log(name);
			}
			
		}
*/

	}
}