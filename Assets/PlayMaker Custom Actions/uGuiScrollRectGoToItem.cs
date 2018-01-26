// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Move the scrollRect to show a particular item")]
	public class uGuiScrollRectGoToItem : FsmStateAction
	{
	
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.ScrollRect))]
		[Tooltip("The GameObject with the ScrollRect UGui component.")]
		public FsmOwnerDefault gameObject;

		public FsmOwnerDefault itemTarget;


		[Tooltip("The process may take a frame or two if the layout hasn't been updated, and if Everyframe is check will be called everytime the scroll recahed the target")]
		public FsmEvent done;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

	//	public bool debug;

		ScrollRect _scrollRect;
		RectTransform _contentPanel;
		RectTransform _target;
		GameObject _item;

		public override void Reset()
		{
			gameObject = null;
			itemTarget = new FsmOwnerDefault();
			itemTarget.OwnerOption = HutongGames.PlayMaker.OwnerDefaultOption.SpecifyGameObject;

			done = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_scrollRect = _go.GetComponent<ScrollRect>();
			}

			DoSetValue();
		}

		public override void OnUpdate()
		{
			DoSetValue();

		}
	
		void DoSetValue()
		{

			if (_scrollRect == null) {
				return;
			}

			bool _done 	= false;


			_item = Fsm.GetOwnerDefaultTarget(itemTarget);

			if (_item!=null)
			{
				_target = _item.GetComponent<RectTransform>();
			}

			float objHeight = _target.rect.height;
			if (objHeight == 0f) {
				return;
			}

			_contentPanel = _target.parent as RectTransform;

			// Vertical Setup
			float _contentHeight = _contentPanel.rect.height;
			//if (debug) Debug.Log ("_contentHeight " + _contentHeight);
			float _itemY = _target.localPosition.y; 
		//	if (debug) Debug.Log ("_itemY " + _itemY);

			float _sizeYOffset = 0f;

			if (_itemY < _contentHeight / 2f) {
				_sizeYOffset = - _target.rect.height / 2f;
			} else {
				_sizeYOffset = _target.rect.height / 2f;
			}
		//	if (debug) Debug.Log ("_sizeYOffset " + _sizeYOffset);

			float _normalizedScrollHeight =  (_itemY + _sizeYOffset) / _contentHeight;
		//	if (debug) Debug.Log ("_normalizedScrollHeight raw " + _normalizedScrollHeight);

			_normalizedScrollHeight = Mathf.Clamp01 (_normalizedScrollHeight);
	//		if (debug) Debug.Log ("_normalizedScrollHeight " + _normalizedScrollHeight);

			if (_scrollRect.verticalNormalizedPosition != _normalizedScrollHeight) {
				_scrollRect.verticalNormalizedPosition = _normalizedScrollHeight;
				_done =true;
			}

			// Horizontal setup
			float _contentwidth = _contentPanel.rect.width;

			float _itemX = _target.localPosition.x; 
		

			float _sizeXOffset = 0f;
			
			if (_itemX < _contentwidth / 2f) {
				_sizeXOffset = - _target.rect.width / 2f;
			} else {
				_sizeXOffset = _target.rect.width / 2f;
			}

			float _normalizedScrollWidth = (_itemX + _sizeXOffset) / _contentwidth;

			_normalizedScrollWidth = Mathf.Clamp01 (_normalizedScrollWidth);

			
			if (_scrollRect.horizontalNormalizedPosition != _normalizedScrollWidth) {
				_scrollRect.horizontalNormalizedPosition = _normalizedScrollWidth;
				_done = true;
			}

			if (_done) {
				Fsm.Event(done);
			}

			if (!everyFrame)
			{
				Finish();
			}

		}
	}
}