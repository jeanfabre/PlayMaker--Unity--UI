
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;


namespace HutongGames.PlayMaker.Ecosystem.Utils
{

	[CustomPropertyDrawer (typeof (PlayMakerEventTarget))]
	public class PlayMakerEventTargetDrawer : PlayMakerPropertyDrawerBaseClass 
	{

		/// <summary>
		/// The row count. Computed and set by inheriting class
		/// </summary>
		int rowCount;

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property,label) * rowCount;
		}

		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {


			SerializedProperty eventTarget = prop.FindPropertyRelative("eventTarget");
			SerializedProperty gameObject = prop.FindPropertyRelative("gameObject");
			SerializedProperty includeChildren = prop.FindPropertyRelative("includeChildren");
			SerializedProperty fsmComponent = prop.FindPropertyRelative("fsmComponent");

			CacheOwnerGameObject(prop.serializedObject);

			int row = 0;

			// draw the enum popup Field
			int oldEnumIndex = eventTarget.enumValueIndex;

			// force the GameObject value to be the owner when switching to it
			// this is just to fall back nicely on a preset that is the expected one, as opposed to target nothing
			if (oldEnumIndex==0 && gameObject.objectReferenceValue!=ownerGameObject)
			{
				gameObject.objectReferenceValue = ownerGameObject;
			}

			EditorGUI.PropertyField(
				GetRectforRow(pos,++row -1),
				eventTarget,label,true);

			// Properly set the GameObjectObject to the owner if this is the choice
			if (oldEnumIndex !=eventTarget.enumValueIndex)
			{
				if (eventTarget.enumValueIndex==0)
				{
					gameObject.objectReferenceValue = ownerGameObject;
				}
			}

			// Additional fields
			if (eventTarget.enumValueIndex==0) // targeting Owner: needs only the include children field
			{
				EditorGUI.indentLevel++;
				if (eventTarget.enumValueIndex==0)
				{
					EditorGUI.PropertyField(
						GetRectforRow(pos,++row -1),
						includeChildren,new GUIContent("Include Children"),true);
				}
				EditorGUI.indentLevel--;
			}else if(eventTarget.enumValueIndex==2) // targeting Broadcasting: needs no additional fields
			{
				//nothing
			}else{ // targeting GameObject or FsmComponent

				EditorGUI.indentLevel++;

				if (eventTarget.enumValueIndex==1) // GameObject target
				{
					EditorGUI.PropertyField(
						GetRectforRow(pos,++row -1),
						gameObject,new GUIContent("Game Object"),true);

					EditorGUI.PropertyField(
						GetRectforRow(pos,++row -1),
						includeChildren,new GUIContent("Include Children"),true);


				}else if (eventTarget.enumValueIndex==3) // FsmComponent target
				{
					EditorGUI.PropertyField(
						GetRectforRow(pos,++row -1),
						fsmComponent,new GUIContent("Fsm Component"),true);
				}
				EditorGUI.indentLevel--;
			}

			// attempt to refresh UI and avoid glitch
			if (row!=rowCount)
			{
				prop.serializedObject.ApplyModifiedProperties();
				prop.serializedObject.Update();
			}
			// update the rowCount to compute the right interface height
			rowCount = row;
		}


	}
}