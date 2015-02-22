using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

using System.Collections;

[CustomEditor(typeof(PlayMakerUGuiPointerEventsProxy))]
public class PlayMakerUGuiPointerEventsProxyInspector : Editor {
	
	public override void OnInspectorGUI()
	{

		SerializedProperty eventTarget = serializedObject.FindProperty("eventTarget");
		EditorGUILayout.PropertyField(eventTarget);

		SerializedProperty onClickEvent = serializedObject.FindProperty("onClickEvent");
		EditorGUILayout.PropertyField(onClickEvent);

		SerializedProperty onDownEvent = serializedObject.FindProperty("onDownEvent");
		EditorGUILayout.PropertyField(onDownEvent);

		SerializedProperty onEnterEvent = serializedObject.FindProperty("onEnterEvent");
		EditorGUILayout.PropertyField(onEnterEvent);

		SerializedProperty onExitEvent = serializedObject.FindProperty("onExitEvent");
		EditorGUILayout.PropertyField(onExitEvent);

		SerializedProperty onUpEvent = serializedObject.FindProperty("onUpEvent");
		EditorGUILayout.PropertyField(onUpEvent);


		serializedObject.ApplyModifiedProperties();
	}


}
