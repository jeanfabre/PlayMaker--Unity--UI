using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

using HutongGames.PlayMaker.Ecosystem.Utils;

public class PlayMakerUGuiPointerEventsProxy : MonoBehaviour, 
				IPointerClickHandler, 
				IPointerDownHandler, 
				IPointerEnterHandler,
				IPointerExitHandler,
				IPointerUpHandler
{

	public PlayMakerEventTarget eventTarget;

	[EventTargetVariable("eventTarget")]
	public PlayMakerEvent onClickEvent;

	[EventTargetVariable("eventTarget")]
	public PlayMakerEvent onDownEvent;

	[EventTargetVariable("eventTarget")]
	public PlayMakerEvent onEnterEvent;

	[EventTargetVariable("eventTarget")]
	public PlayMakerEvent onExitEvent;

	[EventTargetVariable("eventTarget")]
	public PlayMakerEvent onUpEvent;
	
	public void OnPointerClick (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onClickEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}
	public void OnPointerDown (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onDownEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}
	public void OnPointerEnter (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onEnterEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}
	public void OnPointerExit (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onExitEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}
	public void OnPointerUp (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onUpEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}
}
