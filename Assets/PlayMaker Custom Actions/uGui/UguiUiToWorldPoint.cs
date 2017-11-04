// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("uGui")]
    [Tooltip("Sets the Main Camera.")]
    public class UguiUiToWorldPoint : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Camera))]
        [Tooltip("The GameObject to set as the main camera (should have a Camera component).")]
        public FsmOwnerDefault gameObjectCamera;

        [RequiredField]
        [Tooltip("this should be a ui element else you will get strange behaviour")]
        public FsmGameObject uiElement;

        [ActionSection("Set Target position OR Target Object")]
        public FsmVector3 targetPosition;

        public FsmGameObject target;

        public FsmVector3 offset;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        private Vector3 tar;

        public override void Reset()
        {
            gameObjectCamera = null;
            uiElement = null;
            everyFrame = false;
            target = new FsmGameObject() { UseVariable = true };
            targetPosition = new FsmVector3() { UseVariable = true };
            offset = new FsmVector3() { UseVariable = true };
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

            if (offset == null || offset.Value == Vector3.zero)
            {
                if (!targetPosition.IsNone)
                {
                    tar = targetPosition.Value;
                }
                else
                {
                    if (!target.IsNone)
                    {
                        tar = target.Value.transform.position;
                    }
                    else return;
                }


            }
            else
            {
                if (!targetPosition.IsNone)
                {
                    tar = targetPosition.Value + offset.Value;
                }
                else
                {
                    if (!target.IsNone)
                    {
                        tar = target.Value.transform.position + offset.Value;
                    }
                    else return;
                }
            }

            Camera _camera = go.GetComponent<Camera>();
            Vector3 objectPos = _camera.WorldToScreenPoint(tar);
            uiElement.Value.transform.position = objectPos;
        }
    }
}