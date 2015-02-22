using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;

public class PlayMakerUGuiDragEventsProxy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

	public PlayMakerEventTarget eventTarget = new PlayMakerEventTarget(true);

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onBeginDragEvent= new PlayMakerEvent("UGUI / ON BEGIN DRAG");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onDragEvent = new PlayMakerEvent("UGUI / ON DRAG");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onEndDragEvent= new PlayMakerEvent("UGUI / ON END DRAG");
	
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
