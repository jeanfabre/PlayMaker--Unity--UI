using UnityEngine;
using UnityEditor;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	/// <summary>
	/// Playmaker property drawer base class. Extend this class for all PlayMaker related property drawer
	/// </summary>
	public class PlayMakerPropertyDrawerBaseClass : PropertyDrawer {

		protected int rowCount;

		//protected int rowCount;
		/// <summary>
		/// The default height of one property.
		/// </summary>
		protected const int ControlHeight = 16;

		/// <summary>
		/// The GameObject owner of the component being inspected. 
		/// Keep it cached to avoid reflecting on every OnGUI
		/// </summary>
		public GameObject ownerGameObject
		{
			get{
				return _ownerGameObject;
			}
		}
		GameObject _ownerGameObject;
		
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return rowCount*ControlHeight;
		}

		/// <summary>
		/// Compute the Rect for a one line property field at a given row index.
		/// </summary>
		/// <returns>The rect.</returns>
		/// <param name="position">The Rect Position given by the PropertyDrawer itself</param>
		/// <param name="row">The row index for that rect. Index starts at 0</param>
		public Rect GetRectforRow(Rect position,int row)
		{
			return new Rect (position.x, position.y + row*ControlHeight, position.width, ControlHeight);
		}

		/// <summary>
		/// Caches the owner GameObject. Call this if you want to reference automatically the owner
		/// for example for an event target.
		/// note: It's really odd, cause I can't get serializedObject.targetObject to work...
		/// </summary>
		public void CacheOwnerGameObject(SerializedObject serializedObject)
		{
			if (_ownerGameObject==null)
			{
				_ownerGameObject = PlayMakerInspectorUtils.GetGameObject(serializedObject);
			}
		}
	}
}