// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
// REMOVED ECOSYSTEM FLAG TO AVOID DUPLICATES IN ECOSYSTEM BROWSER, AS IT SEARCHED THIS REP AS WELL


using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Easing Animation - Vector2")]
	public class EaseVector2 : EaseFsmAction
	{
		[RequiredField]
		public FsmVector2 fromValue;
		[RequiredField]
		public FsmVector2 toValue;
		[UIHint(UIHint.Variable)]
		public FsmVector2 vector2Variable;
		
		private bool finishInNextStep = false;
		
		public override void Reset (){
			base.Reset();
			vector2Variable = null;
			fromValue = null;
			toValue = null;
			finishInNextStep = false;
		}
		                   
		
		public override void OnEnter ()
		{
			base.OnEnter();
			fromFloats = new float[2];
			fromFloats[0] = fromValue.Value.x;
			fromFloats[1] = fromValue.Value.y;
			toFloats = new float[2];
			toFloats[0] = toValue.Value.x;
			toFloats[1] = toValue.Value.y;
			resultFloats = new float[2];
			finishInNextStep = false;
		    vector2Variable.Value = fromValue.Value;
		}
		
		public override void OnExit (){
			base.OnExit();	
		}
			
		public override void OnUpdate(){
			base.OnUpdate();
			if(!vector2Variable.IsNone && isRunning){
				vector2Variable.Value = new Vector2(resultFloats[0],resultFloats[1]);
			}
			
			if(finishInNextStep){
				Finish();
				if(finishEvent != null)	Fsm.Event(finishEvent);
			}
			
			if(finishAction && !finishInNextStep){
				if(!vector2Variable.IsNone){
					vector2Variable.Value = new Vector2(reverse.IsNone ? toValue.Value.x : reverse.Value ? fromValue.Value.x : toValue.Value.x, 
					                                    reverse.IsNone ? toValue.Value.y : reverse.Value ? fromValue.Value.y : toValue.Value.y
					                                    );
				}
				finishInNextStep = true;
			}
		}
	}
}