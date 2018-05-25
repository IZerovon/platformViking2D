using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

namespace MoreMountains.MMInterface
{	
	/// <summary>
	/// Add this component on an object, specify a scene name in its inspector, and call LoadScene() to load the desired scene.
	/// </summary>
	public class SceneSelector : MonoBehaviour 
	{
		/// the name of the scene that needs to be loaded when LoadScene gets called
		public string SceneName;

		/// <summary>
		/// Loads the scene specified in the inspector
		/// </summary>
		public virtual void LoadScene()
		{
			SceneManager.LoadScene (SceneName);
		}
	}
}
