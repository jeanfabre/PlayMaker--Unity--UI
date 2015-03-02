using System;
using UnityEngine;
using System.Text.RegularExpressions;

using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	public enum OwnerSelectionOptions {Owner,SpecifyGameObject};

	/// <summary>
	/// Defines a GameObject target. Can be the owner of the component or a specific GameObject.
	/// Use this class in your Components public interface, The Unity Inspector will use a specific PropertyDrawer is defined
	/// </summary>
	[Serializable]
	public class Owner{

		public OwnerSelectionOptions selection;
		public GameObject gameObject;

	}

}