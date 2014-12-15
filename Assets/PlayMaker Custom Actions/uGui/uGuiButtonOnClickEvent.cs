// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Fires an event on click.")]
	public class uGuiButtonOnClickEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Button))]
		[Tooltip("The GameObject with the button ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;

		private uUI.Button button;
		
		public override void Reset()
		{
			gameObject = null;
			sendEvent = null;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go!=null)
			{
				button = go.GetComponent<uUI.Button>();
				if (button!=null)
				{
					button.onClick.AddListener(DoOnClick);
				}else{
					LogError("Missing UI.Button on "+go.name);
				}
			}else{
				LogError("Missing GameObject ");
			}
		}

		public override void OnExit()
		{
			if (button!=null)
			{
				button.onClick.RemoveListener(DoOnClick);
			}
		}

		public void DoOnClick()
		{
			Fsm.Event(sendEvent);
		}
	}
}