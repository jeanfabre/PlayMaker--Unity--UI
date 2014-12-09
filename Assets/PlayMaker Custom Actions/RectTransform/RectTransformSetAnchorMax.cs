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
	[Tooltip("The normalized position in the parent RectTransform that the upper right corner is anchored to.")]
	public class RectTransformSetAnchorMax : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Use a stored Vector2 position, and/or set individual axis below.")]
		public FsmVector2 anchorMax;
		
		[HasFloatSlider(0f,1f)]
		public FsmFloat x;
		[HasFloatSlider(0f,1f)]
		public FsmFloat y;
		
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			
			gameObject = null;
			anchorMax = null;
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
			
			DoSetAnchorMax();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetAnchorMax();
		}
		
		void DoSetAnchorMax()
		{
			// init position	
			Vector2 _anchor = anchorMax.Value;
			
			// override any axis
			
			if (!x.IsNone) _anchor.x = x.Value;
			if (!y.IsNone) _anchor.y = y.Value;
			
			// apply
			
			_rt.anchorMax = _anchor;
		}
	}
}