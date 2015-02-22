using UnityEngine;
using UnityEditor;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	[CustomPropertyDrawer (typeof (Owner))]
	public class OwnerDrawer : PlayMakerPropertyDrawerBaseClass 
	{

		public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {


			SerializedProperty selection = prop.FindPropertyRelative("selection");
			SerializedProperty gameObject = prop.FindPropertyRelative("gameObject");

			CacheOwnerGameObject(prop.serializedObject);


			// draw the enum popup Field
			int oldEnumIndex = selection.enumValueIndex;

			EditorGUI.PropertyField(
				GetRectforRow(pos,0),
				selection,new GUIContent("Target"),true);

			if (oldEnumIndex !=selection.enumValueIndex)
			{
				if (selection.enumValueIndex==1)
				{
					gameObject.objectReferenceValue = ownerGameObject;
				}
			}

			if (selection.enumValueIndex==1)
			{
				EditorGUI.indentLevel++;

				EditorGUI.PropertyField(
					GetRectforRow(pos,1),
					gameObject,new GUIContent("Game Object"),true);
			}
	
		}

	}
}