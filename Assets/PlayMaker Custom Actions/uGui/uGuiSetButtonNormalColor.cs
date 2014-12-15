// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.


using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the button normal color value of a UGui button component. With reset on exit option ")]
	public class uGuiSetButtonNormalColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Button))]
		[Tooltip("The GameObject with the button ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The new color of the UGui Button component.")]
		public FsmColor normalColor;
		
		
		// MW this could be private: but in some cases it could be usefull to save the old value in a pm var or for debugging
		[Tooltip("The former color of the UGui component.")]
		public FsmColor storedOldColor;
		
		
		
		[Tooltip("Reset when exiting this state.")]
		public bool resetOnExit;
		
		[Tooltip("Bypass button to drive the action by bool")]
		public FsmBool enabled = true;

		public bool everyFrame;
		
		

		private uUI.Button _Button;
		private uUI.ColorBlock _CB;

		

		public override void Reset()
		{
			normalColor = null;
			storedOldColor = null;
			resetOnExit = false;
			everyFrame = false;
			enabled = true;
		}
		
		public override void OnEnter()
		{
			Initilize(Fsm.GetOwnerDefaultTarget(gameObject));

			DoSetButtonColor();
			
			if (!everyFrame)
				Finish();
			
		}
		
		
		public override void OnUpdate()
		{
			DoSetButtonColor();
		}
		
		
		
		public override void OnExit()
		{
				if (resetOnExit)
				{
					DoSetOldColorValue();
				}
		}
		

		void Initilize(GameObject go)
		{
			if (go == null)
			{
				LogError("Missing Button Component!");
				Finish();
			}
			
			// this might be usefull for checking for the right component  but i dont know the right type
			/* 
			if (go.collider == null)
			{   LogError("Missing Collider!");
				Finish();
			}
			*/
			
			// get the component
			_Button = go.GetComponent<uUI.Button>();
			_CB = _Button.colors;
		}
		
		void DoSetButtonColor()
		{
			if (enabled.Value == false)
			{
				Finish();
			}
			else
			{
				if (_Button!=null)
				{	
				
					// store old data for reset.
					storedOldColor.Value = _CB.normalColor;
					
					// Do the actual action stuff here.	
					_CB.normalColor = normalColor.Value;
					_Button.colors = _CB;
				}
				else
				{   LogError("Missing Button Component!");
					return; 
				}
			}
		}
		
		void DoSetOldColorValue()
		{
			if (_Button!=null)
			{
			// reset
				_CB.normalColor = storedOldColor.Value;
				_Button.colors = _CB;
			}
		}
		
	}
}