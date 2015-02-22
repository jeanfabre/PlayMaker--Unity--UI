
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

		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {


			SerializedProperty eventTarget = prop.FindPropertyRelative("eventTarget");
			SerializedProperty gameObject = prop.FindPropertyRelative("gameObject");
			SerializedProperty includeChildren = prop.FindPropertyRelative("includeChildren");
			SerializedProperty fsmComponent = prop.FindPropertyRelative("fsmComponent");

			CacheOwnerGameObject(prop.serializedObject);


			int row = 0;

			//
			//
			// draw the enum popup Field
			int oldEnumIndex = eventTarget.enumValueIndex;

			if (oldEnumIndex==0 && gameObject.objectReferenceValue!=ownerGameObject)
			{
				gameObject.objectReferenceValue = ownerGameObject;
			}

			EditorGUI.PropertyField(
				GetRectforRow(pos,++row -1),
				eventTarget,label,true);

			if (oldEnumIndex !=eventTarget.enumValueIndex)
			{
				if (eventTarget.enumValueIndex==0)
				{
					gameObject.objectReferenceValue = ownerGameObject;
				}
			}

			// Owner || broadcastALL don't require an additional field for the target definition
			if (eventTarget.enumValueIndex==0)
			{
				EditorGUI.indentLevel++;
				if (eventTarget.enumValueIndex==0)
				{
					EditorGUI.PropertyField(
						GetRectforRow(pos,++row -1),
						includeChildren,new GUIContent("Include Children"),true);
				}
				EditorGUI.indentLevel--;
			}else if(eventTarget.enumValueIndex==2)
			{
				//nothing

			}else{

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

			rowCount = row;
		}


	}
}