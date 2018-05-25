using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
	[RequireComponent(typeof(Collider2D))]
	/// <summary>
	/// Add this component to an object with a 2D collider and it'll be grippable by any Character equipped with a CharacterGrip
	/// </summary>
	[AddComponentMenu("Corgi Engine/Environment/Grip")]
	public class Grip : MonoBehaviour 
	{
		/// <summary>
		/// When an object collides with the grip, we check to see if it's a compatible character, and if yes, we change its state to Gripping
		/// </summary>
		/// <param name="collider">Collider.</param>
	    protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
			CharacterGrip characterGrip = collider.gameObject.GetComponentNoAlloc<CharacterGrip>();
			if (characterGrip == null)	{	return;	}

			characterGrip.StartGripping (this.transform);
		}
	}
}