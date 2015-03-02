
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using HutongGames.Editor;
using HutongGames.PlayMakerEditor;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	[CustomPropertyDrawer (typeof (PlayMakerEvent))]
	public class PlayMakerEventDrawer : PlayMakerPropertyDrawerBaseClass 
	{

		/// <summary>
		/// Flag to know that we have checked for the attributes
		/// </summary>
		bool attributeScanned;

		/// <summary>
		/// Use the attribute "EventTargetVariable" to point to that variable
		/// </summary>
		SerializedProperty eventTargetVariable;

		bool showOptions;

		// these three property are external, and comes from the attribution of a PlayMakerEventTarget Variable via
		// the custom Attribute EventTargetVariable
		// if no eventTarget is defined, then this EventDrawer will show Global Events
		SerializedProperty eventTarget;
		SerializedProperty includeChildren;
		SerializedProperty gameObject;
		SerializedProperty fsmComponent;

		SerializedProperty eventName;
		SerializedProperty allowLocalEvents;
		SerializedProperty defaultEventName;

		string defaultEventNameValue;

		/// <summary>
		/// The row count. Computed and set by inheriting class
		/// </summary>
		int rowCount;
		
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property,label) * (rowCount);
		}

		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {

			if (!attributeScanned)
			{
				attributeScanned = true;

				// check for EventTargetVariable Attribute
				object[] _evenTargets = this.fieldInfo.GetCustomAttributes(typeof(EventTargetVariable),true);
				
				if (_evenTargets.Length>0)
				{
					string variableName = (_evenTargets[0] as EventTargetVariable).variable;
					eventTargetVariable = prop.serializedObject.FindProperty(variableName);
				}

				/// check for the ShowOptions attribute
				object[] _showOptions = this.fieldInfo.GetCustomAttributes(typeof(ShowOptions),true);
				
				if (_showOptions.Length>0)
				{
					showOptions = true;
				}
			}


			if (eventTargetVariable!=null)
			{
				eventTarget = eventTargetVariable.FindPropertyRelative("eventTarget");
				includeChildren = eventTargetVariable.FindPropertyRelative("includeChildren");
				gameObject = eventTargetVariable.FindPropertyRelative("gameObject");
				fsmComponent = eventTargetVariable.FindPropertyRelative("fsmComponent");
			}

			eventName = prop.FindPropertyRelative("eventName");
			string _eventName = eventName.stringValue;

			allowLocalEvents = prop.FindPropertyRelative("allowLocalEvents");
			bool _allowEvent = allowLocalEvents.boolValue;

			defaultEventName = prop.FindPropertyRelative("defaultEventName");
			defaultEventNameValue = defaultEventName.stringValue;

			CacheOwnerGameObject(prop.serializedObject);

			int row =0;

			string[] _eventList = new string[0];

			bool isEventImplemented = false;



			// Get the list of events
			if (eventTarget==null || eventTarget.enumValueIndex<3) //undefined || Owner || GameObject || broadcastALL
			{
				_eventList = PlayMakerInspectorUtils.GetGlobalEvents(true);

				if (eventTarget.enumValueIndex==0 || eventTarget.enumValueIndex==1) // Owner || GameObject
				{
					isEventImplemented = PlayMakerInspectorUtils.DoesTargetImplementsEvent((GameObject)gameObject.objectReferenceValue,_eventName,true);
				}
			}else if (eventTarget.enumValueIndex ==3 ) // FsmComponent
			{
				PlayMakerFSM _fsm = (PlayMakerFSM)fsmComponent.objectReferenceValue;
				_eventList = PlayMakerInspectorUtils.GetImplementedGlobalEvents(_fsm,true);

				isEventImplemented =  PlayMakerInspectorUtils.DoesTargetImplementsEvent(_fsm,_eventName);
			}

			// find the index of the serialized event name in the list of events
			int selected = 0;
			if (! string.IsNullOrEmpty(_eventName))
			{
				selected = ArrayUtility.IndexOf<string>(_eventList,_eventName);
			}

			Rect _rect= GetRectforRow(pos,++row -1);

			if(showOptions)
			{
				_rect.width -= 18;
			}

			string _popupLabel = label.text;

			if(selected!=0 && eventTarget.enumValueIndex!=2) // not none and not broadcasting
			{
				if ((selected>0 && !isEventImplemented )|| selected ==-1)
				{
					//_popup = GUI.skin.GetStyle("ErrorLabel");
					GUIStyle labelStyle = GUI.skin.GetStyle("controlLabel");
					labelStyle.richText = true;

					_popupLabel = "<color=red>"+_popupLabel+"</color>";
				}
			}

			// Event Popup 
			Rect _contentRect = EditorGUI.PrefixLabel(_rect,label);
			//_contentRect.width -= 0;
			if (GUI.Button(
					_contentRect,
					string.IsNullOrEmpty(_eventName)?"none":_eventName, 
					EditorStyles.popup))
			{
				GenericMenu menu = GenerateEventMenu(_eventList,_eventName);
				menu.DropDown(_rect);

			}

			/*
			 _rect.x =_rect.xMax-10;
			 _rect.width = 10;

			if (GUI.Button(_rect,"?","label"))
			{
				//buttonRect.x += FsmEditor.Window.position.x + FsmEditor.Window.position.width - FsmEditor.InspectorPanelWidth;
				//buttonRect.y += FsmEditor.Window.position.y + StateInspector.ActionsPanelRect.y + 3 - FsmEditor.StateInspector.scrollPosition.y;
				//var newVariableWindow = PlayMakerEditor.  NewEventWindow.CreateDropdown("New Global Event", _contentRect, eventName);
				//newVariableWindow.EditCommited += DoNewGlobalEvent;
			}
*/
			_rect.x =_rect.xMax +2 ;
			_rect.width = 18;

			if (showOptions)
			{
				if (GUI.Button(_rect,FsmEditorContent.SettingsButton,"label"))
				{
					GenericMenu menu = new GenericMenu();

					if (eventTarget!=null && eventTarget.enumValueIndex != 2 ) // not a broadcast call
					{
						menu.AddItem (new GUIContent ("Show All global Events"), false, ShowAllEvents);
						menu.AddItem (new GUIContent ("Show local Event"), false, ShowImplementedEvents);
					
						menu.AddSeparator ("");
					}

					menu.AddItem(new GUIContent ("Reset"), false, ResetToDefault);

					menu.ShowAsContext ();
				}

			}

			// feedback
			if (selected ==-1)
			{
				EditorGUI.LabelField(
					GetRectforRow(pos,++row -1),
					"<color=red>missing event</color>",
					"<color=red>"+_eventName+"</color>"
					);
			}
			
			if(selected!=0 && eventTarget.enumValueIndex!=2) // not none and not broadcasting
			{
				if (selected>0 && !isEventImplemented)
				{
					EditorGUI.LabelField(
						GetRectforRow(pos,++row -1),
						" ",
						"<color=red>Not implemented on target</color>"
						);
				}
			}




			// attempt to refresh UI and avoid glitch
			if (row!=rowCount)
			{
				prop.serializedObject.ApplyModifiedProperties();
				prop.serializedObject.Update();
			}
			rowCount = row;
		}

		void ResetToDefault()
		{
			eventName.stringValue = defaultEventNameValue;
			eventName.serializedObject.ApplyModifiedProperties();
		}

		void ShowImplementedEvents()
		{

		}

		void ShowAllEvents()
		{

		}

		void EventMenuSelectionCallBack(object userdata)
		{

			if (userdata==null) // none
			{
				eventName.stringValue = "";
			}else{
				eventName.stringValue = (string)userdata;
			}

			eventName.serializedObject.ApplyModifiedProperties();
		
		}

		GenericMenu GenerateEventMenu(string[] _eventList,string currentSelection)
		{
			var menu = new GenericMenu();
			menu.AddItem(new GUIContent("none"), currentSelection.Equals("none"), EventMenuSelectionCallBack, null);
	
			foreach(string _event in _eventList)
			{
				menu.AddItem(new GUIContent(_event), currentSelection.Equals(_event), EventMenuSelectionCallBack,_event);
			}

			return menu;
		}
	}
}