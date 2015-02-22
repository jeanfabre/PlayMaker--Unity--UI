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
	[ShowOptions]
	public PlayMakerEvent onClickEvent = new PlayMakerEvent("UGUI / ON POINTER CLICK");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onDownEvent = new PlayMakerEvent("UGUI / ON POINTER DOWN");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onEnterEvent = new PlayMakerEvent("UGUI / ON POINTER ENTER");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onExitEvent = new PlayMakerEvent("UGUI / ON POINTER EXIT");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onUpEvent = new PlayMakerEvent("UGUI / ON POINTER UP");
	
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
