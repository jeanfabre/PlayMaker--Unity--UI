﻿// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Ugui")]
	[Tooltip("Sets the Main Camera.")]
	public class UguiUiToWorldPoint : FsmStateAction
    {
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject to set as the main camera (should have a Camera component).")]
		public FsmOwnerDefault gameObjectCamera;

        public FsmGameObject uiElement;

        public FsmGameObject target;

        public FsmVector3 offset;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        public override void Reset()
		{
            gameObjectCamera = null;
            uiElement = null;
            everyFrame = false;
        }

		public override void OnEnter()
		{
            if (!everyFrame)
            {
                DoUiToWorldPoint();
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoUiToWorldPoint();

        }


        void DoUiToWorldPoint()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObjectCamera);
            if (go == null) return;

            if(offset == null)
            {
                Vector3 tar = target.Value.transform.position;
            }
            else
            {
                Vector3 tar = target.Value.transform.position + offset.Value;
            }
           



            Camera _camera = go.GetComponent<Camera>();
            Vector3 objectPos = _camera.WorldToScreenPoint(target.Value.transform.position);
            uiElement.Value.transform.position = objectPos;
        }
	}
}