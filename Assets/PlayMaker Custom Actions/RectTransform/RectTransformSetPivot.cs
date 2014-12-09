// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in this RectTransform that it rotates around.")]
	public class RectTransformSetPivot : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Use a stored Vector2 position, and/or set individual axis below.")]
		public FsmVector2 pivot;

		[HasFloatSlider(0f,1f)]
		public FsmFloat x;
		[HasFloatSlider(0f,1f)]
		public FsmFloat y;
		

		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			pivot = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };

		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoSetPivotPosition();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetPivotPosition();
		}
		
		void DoSetPivotPosition()
		{
			// init position	
			Vector2 _position = pivot.Value;
			
			// override any axis
			
			if (!x.IsNone) _position.x = x.Value;
			if (!y.IsNone) _position.y = y.Value;
			
			// apply

			_rt.pivot = _position;
		}
	}
}