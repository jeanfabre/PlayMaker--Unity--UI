// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// based on Sebastio work: http://hutonggames.com/playmakerforum/index.php?topic=8452.msg42858#msg42858
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set Graphic Color.")]
	public class uGuiSetGraphicColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.FsmColor)]
		[Tooltip("Color value to set.")]
		public FsmColor color;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		private uUI.Graphic _component;
		
		public override void Reset()
		{
			gameObject = null;
			color = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_component = _go.GetComponent<uUI.Graphic>();
			}
			
			DoSetColorValue();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetColorValue();
		}

		void DoSetColorValue()
		{
			if (_component!=null)
			{
				_component.color = color.Value;
			}
		}
		
	}
}