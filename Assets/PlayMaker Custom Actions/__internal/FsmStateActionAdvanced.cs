// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
// ORIGINAL VERSION: 
// https://github.com/jeanfabre/PlayMakerCustomActions_U3/tree/master/Assets/PlayMaker%20Custom%20Actions/__Internal

using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
	public abstract class FsmStateActionAdvanced : FsmStateAction
	{
		
		public enum FrameUpdateSelector {OnUpdate,OnLateUpdate,OnFixedUpdate};

		[ActionSection("Update type")]
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public FrameUpdateSelector updateType;
		
		
		public abstract void OnActionUpdate();
		
		public override void Reset()
		{
			everyFrame = false;
			updateType = FrameUpdateSelector.OnUpdate;
		}
		
		public override void Awake()
		{
			if (updateType == FrameUpdateSelector.OnFixedUpdate)
			{
				   Fsm.HandleFixedUpdate = true;
			}
		}
		
		public override void OnUpdate()
		{
			if (updateType == FrameUpdateSelector.OnUpdate)
			{
				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnLateUpdate()
		{
			if (updateType == FrameUpdateSelector.OnLateUpdate)
			{
				OnActionUpdate();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == FrameUpdateSelector.OnFixedUpdate)
			{
				OnActionUpdate();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
	}
}