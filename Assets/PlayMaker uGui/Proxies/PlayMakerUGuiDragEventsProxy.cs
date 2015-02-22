using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;

public class PlayMakerUGuiDragEventsProxy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

	public PlayMakerEventTarget eventTarget;

	[EventTargetVariable("eventTarget")]
	[DefaultEvent("UGUI / ON BEGIN DRAG")]
	[ShowOptions]
	public PlayMakerEvent onBeginDragEvent = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[DefaultEvent("UGUI / ON DRAG")]
	[ShowOptions]
	public PlayMakerEvent onDragEvent = new PlayMakerEvent();

	[EventTargetVariable("eventTarget")]
	[DefaultEvent("UGUI / ON END DRAG")]
	[ShowOptions]
	public PlayMakerEvent onEndDragEvent = new PlayMakerEvent();
	
	public void OnBeginDrag (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onBeginDragEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}

	public void OnDrag (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onDragEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}
	
	public void OnEndDrag (PointerEventData data) {
		GetLastPointerDataInfo.lastPointeEventData = data;
		onEndDragEvent.SendEvent(PlayMakerUGuiSceneProxy.fsm,eventTarget);
	}
}
