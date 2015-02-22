using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

using System.Collections;

[CustomEditor(typeof(PlayMakerUGuiDragEventsProxy))]
public class PlayMakerUGuiDragEventsProxyInspector : Editor {
	
	public override void OnInspectorGUI()
	{

		SerializedProperty eventTarget = serializedObject.FindProperty("eventTarget");
		EditorGUILayout.PropertyField(eventTarget);

		SerializedProperty onBeginDragEvent = serializedObject.FindProperty("onBeginDragEvent");
		EditorGUILayout.PropertyField(onBeginDragEvent);

		SerializedProperty onDragEvent = serializedObject.FindProperty("onDragEvent");
		EditorGUILayout.PropertyField(onDragEvent);

		SerializedProperty onEndDragEvent = serializedObject.FindProperty("onEndDragEvent");
		EditorGUILayout.PropertyField(onEndDragEvent);


		serializedObject.ApplyModifiedProperties();
	}


}
