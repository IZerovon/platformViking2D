using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.UI;

namespace MoreMountains.CorgiEngine
{	
	/// <summary>
	/// A Retro adventure dedicated class that will load the next level
	/// </summary>
	public class RetroAdventureFinishLevel : FinishLevel 
	{
		/// <summary>
		/// Loads the next level
		/// </summary>
		public override void GoToNextLevel()
		{
			MMEventManager.TriggerEvent (new CorgiEngineEvent (CorgiEngineEventTypes.LevelComplete));
			MMEventManager.TriggerEvent(new MMGameEvent("Save"));
			LevelManager.Instance.SetNextLevel (LevelName);
		}	
	}
}
