// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the text color value of a UGui component. With reset on exit option ")]
	public class uGuiSetTextColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The new color of the UGui component.")]
		public FsmColor normalColor;
		
		[Tooltip("The former color of the UGui component.")]
		public FsmColor storedOldColor;
		
		
		
		[Tooltip("Reset the color when exiting this state.")]
		public bool resetOnExit;

		public bool everyFrame;
		
		[Tooltip("Bypass button to drive the action by bool")]
		public FsmBool doThisAction = true;

		private uUI.Text _text;

		

		public override void Reset()
		{
			normalColor = null;
			storedOldColor = null;
			resetOnExit = false;
			everyFrame = false;
			doThisAction = true;
		}
		
		public override void OnEnter()
		{
	
			if (doThisAction.Value == false)
			{
				Finish();
			}
			else
			{
				GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
				if (_go!=null)
				{
					_text = _go.GetComponent<uUI.Text>();
					
					
	
				
				}
				DoGetOldColorValue();
	
				DoSetColor();
				
				if (!everyFrame)
					Finish();
			}
		}
		
		
		public override void OnUpdate()
		{
			DoSetColor();
		}
		
		
		
		public override void OnExit()
		{
			if (doThisAction.Value)
			{
				if (resetOnExit)
				{
					DoSetOldColorValue();
				}
			}
		
		}
		

		
		void DoSetColor()
		{
			
			
			if (_text!=null)
			{
				_text.color = normalColor.Value;
			}
		}
		
		void DoGetOldColorValue()
		{
			
			if (_text!=null)
			{
				storedOldColor.Value = _text.color;
			}
		}
		
		void DoSetOldColorValue()
		{
			if (_text!=null)
			{
			_text.color = storedOldColor.Value;
			
			}
		}
		
	}
}