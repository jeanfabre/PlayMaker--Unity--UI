//	(c) Jean Fabre, 2015 All rights reserved.
//	http://www.fabrejean.net
//  contact: http://www.fabrejean.net/contact.htm

// source: whydoidoit : http://answers.unity3d.com/questions/425012/get-the-instance-the-serializedproperty-belongs-to.html

using System.Linq;
using System;
using System.Collections;
using System.Reflection;

using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	public partial class PlayMakerInspectorUtils {


		public static GameObject GetGameObject(SerializedObject serializedObject)
		{
			Component _comp = GetComponent<Component>(serializedObject);
			if (_comp!=null)
			{
				return _comp.gameObject;
			}
			return null;
		}

		public static T GetComponent<T>(SerializedObject serializedObject) 
		{
			if (serializedObject==null)
			{
				return default(T);
			}

			var iterator = serializedObject.GetIterator();
			while(iterator.NextVisible(true))
			{
				T parent = (T)GetParent(iterator);
				if(parent!=null)
				{
					return parent;
				}
			}

			return default(T);
		}



		static object GetParent(SerializedProperty prop)
		{
			var path = prop.propertyPath.Replace(".Array.data[", "[");
			object obj = prop.serializedObject.targetObject;
			var elements = path.Split('.');
			foreach(var element in path.Split('.').Take(elements.Length-1))
			{
				if(element.Contains("["))
				{
					var elementName = element.Substring(0, element.IndexOf("["));
					var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[","").Replace("]",""));
					obj = GetValue(obj, elementName, index);
				}
				else
				{
					obj = GetValue(obj, element);
				}
			}
			return obj;
		}
		
		static object GetValue(object source, string name)
		{
			if(source == null)
				return null;
			var type = source.GetType();
			var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			if(f == null)
			{
				var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
				if(p == null)
					return null;
				return p.GetValue(source, null);
			}
			return f.GetValue(source);
		}
		
		static object GetValue(object source, string name, int index)
		{
			var enumerable = GetValue(source, name) as IEnumerable;
			var enm = enumerable.GetEnumerator();
			while(index-- >= 0)
				enm.MoveNext();
			return enm.Current;
		}

	}
}