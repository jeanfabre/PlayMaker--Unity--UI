// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;
using UnityEngine.EventSystems;

// UNITY_5_6_OR_NEWER
#if FALSE   
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Returns the EventSystem's currently select GameObject.")]
	public class uGuiOnDragEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Event when the selected GameObject changes")]
		public FsmEvent onDragEvent;

		public bool finish
		GameObject _go;
		EventTrigger _trigger;

		public override void Reset()
		{
			gameObject = null;
			onDragEvent = null;
		}
		
		public override void OnEnter()
		{
			_go =  Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go==null)
			{
				return;
			}

			_trigger = _go.GetComponent<EventTrigger>();

			if (_trigger == null)
			{
				_trigger = _go.AddComponent<EventTrigger>();
			}

			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
			_trigger.triggers.Add(entry);
		}

		void OnDragDelegate( PointerEventData data)
		{
			GetLastPointerDataInfo.lastPointeEventData = data;
			Fsm.Event(onDragEvent);
		}

	
	}
}

#endif