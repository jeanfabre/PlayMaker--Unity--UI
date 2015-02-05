// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the color of a UGui Image component.")]
	public class uGuiImageSetColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the image ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Color of the image. Leave to none and use the individual property to use the image color, for example to affect just the alpha channel")]
		public FsmColor color;

		[Tooltip("The red channel Color of the image. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;

		[Tooltip("The green channel Color of the image. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;

		[Tooltip("The blue channel Color of the image. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;

		[Tooltip("The alpha channel Color of the image. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		UnityEngine.UI.Image _image;
		Color _originalColor;

		public override void Reset()
		{
			gameObject = null;
			color = null;

			red = new FsmFloat(){UseVariable=true};
			green = new FsmFloat(){UseVariable=true};
			blue = new FsmFloat(){UseVariable=true};
			alpha = new FsmFloat(){UseVariable=true};

			resetOnExit = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_image = _go.GetComponent<UnityEngine.UI.Image>();
			}

			if (resetOnExit.Value)
			{
				_originalColor = _image.color;
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
			if (_image!=null)
			{
				Color _color = _image.color;

				if (!color.IsNone)
				{
					_color = color.Value;
				}

				if (!red.IsNone)
				{
					_color.r = red.Value;
				}
				if (!green.IsNone)
				{
					_color.g = green.Value;
				}
				if (!blue.IsNone)
				{
					_color.b = blue.Value;
				}
				if (!alpha.IsNone)
				{
					_color.a = alpha.Value;
				}

				_image.color = _color;
			}
		}

		public override void OnExit()
		{
			if (_image==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_image.color = _originalColor;
			}
		}

	}
}