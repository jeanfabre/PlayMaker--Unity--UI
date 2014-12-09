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
	[Tooltip("The position of the pivot of this RectTransform relative to the anchor reference point." +
		"The anchor reference point is where the anchors are. " +
		"If the anchor are not together, the four anchor positions are interpolated according to the pivot normalized values.")]
	public class RectTransformSetAnchoredPosition : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Use a stored Vector2 position, and/or set individual axis below.")]
		public FsmVector2 position;
		
		public FsmFloat x;
		public FsmFloat y;
		


		RectTransform _rt;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			position = null;
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

			DoSetAnchoredPosition();

			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetAnchoredPosition();
		}
				
		void DoSetAnchoredPosition()
		{
			// init position	
			Vector2 _position = position.Value;

			// override any axis
			
			if (!x.IsNone) _position.x = x.Value;
			if (!y.IsNone) _position.y = y.Value;
			
			// apply

			_rt.anchoredPosition = _position;
		}
	}
}