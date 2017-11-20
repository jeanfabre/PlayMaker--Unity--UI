// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui Text component.")]
	public class uGuiTextSetFont : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
        [ObjectType(typeof(Font))]
		[Tooltip("The text of the UGui Text component.")]
		public FsmObject font;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;



		private UnityEngine.UI.Text _text;
		UnityEngine.Font _originalFont;

		public override void Reset()
		{
			gameObject = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{
            GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
            if(_go != null)
            {
                _text = _go.GetComponent<UnityEngine.UI.Text>();
                if(resetOnExit.Value)
                {
                    _originalFont = _text.font;
                }
                _text.font = font.Value as Font;
            }
		}

		public override void OnExit()
		{
			if (_text==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_text.font = _originalFont;
			}
		}
	}
}