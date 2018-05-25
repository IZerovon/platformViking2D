﻿using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;

namespace MoreMountains.CorgiEngine
{	
	[Serializable]
	public class SoundSettings
	{
		public bool MusicOn = true;
		public bool SfxOn = true;
	}

	/// <summary>
	/// This persistent singleton handles sound playing
	/// </summary>
	[AddComponentMenu("Corgi Engine/Managers/Sound Manager")]
	public class SoundManager : PersistentSingleton<SoundManager>, MMEventListener<MMSfxEvent>, MMEventListener<CorgiEngineEvent>, MMEventListener<MMGameEvent>
	{	
		[Header("Settings")]
		public SoundSettings Settings;

		[Header("Music")]
		/// true if the music is enabled	
		//public bool MusicOn=true;
		/// the music volume
		[Range(0,1)]
		public float MusicVolume=0.3f;

		[Header("Sound Effects")]
		/// true if the sound fx are enabled
		//public bool SfxOn=true;
		/// the sound fx volume
		[Range(0,1)]
		public float SfxVolume=1f;

		[Header("Pause")]
		public bool MuteSfxOnPause = true;

		protected const string _saveFolderName = "CorgiEngine/";
		protected const string _saveFileName = "sound.settings";

	    protected AudioSource _backgroundMusic;	

		protected List<AudioSource> _loopingSounds;
			
		/// <summary>
		/// Plays a background music.
		/// Only one background music can be active at a time.
		/// </summary>
		/// <param name="Clip">Your audio clip.</param>
		public virtual void PlayBackgroundMusic(AudioSource Music)
		{
			// if the music's been turned off, we do nothing and exit
			if (!Settings.MusicOn)
				return;
			// if we already had a background music playing, we stop it
			if (_backgroundMusic!=null)
				_backgroundMusic.Stop();
			// we set the background music clip
			_backgroundMusic=Music;
			// we set the music's volume
			_backgroundMusic.volume=MusicVolume;
			// we set the loop setting to true, the music will loop forever
			_backgroundMusic.loop=true;
			// we start playing the background music
			_backgroundMusic.Play();		
		}	
		
		/// <summary>
		/// Plays a sound
		/// </summary>
		/// <returns>An audiosource</returns>
		/// <param name="sfx">The sound clip you want to play.</param>
		/// <param name="location">The location of the sound.</param>
		/// <param name="loop">If set to true, the sound will loop.</param>
		public virtual AudioSource PlaySound(AudioClip sfx, Vector3 location, bool loop=false)
		{
			if (!Settings.SfxOn)
				return null;
			// we create a temporary game object to host our audio source
			GameObject temporaryAudioHost = new GameObject("TempAudio");
			// we set the temp audio's position
			temporaryAudioHost.transform.position = location;
			// we add an audio source to that host
			AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource; 
			// we set that audio source clip to the one in paramaters
			audioSource.clip = sfx; 
			// we set the audio source volume to the one in parameters
			audioSource.volume = SfxVolume;
			// we set our loop setting
			audioSource.loop = loop;
			// we start playing the sound
			audioSource.Play(); 

			if (!loop)
			{
				// we destroy the host after the clip has played
				Destroy(temporaryAudioHost, sfx.length);
			}
			else
			{
				_loopingSounds.Add (audioSource);
			}

			// we return the audiosource reference
			return audioSource;
		}

		/// <summary>
		/// Stops the looping sounds if there are any
		/// </summary>
		/// <param name="source">Source.</param>
		public virtual void StopLoopingSound(AudioSource source)
		{
			if (source != null)
			{
				_loopingSounds.Remove (source);
				Destroy(source.gameObject);
			}
		}

		protected virtual void SetMusic(bool status)
		{
			Settings.MusicOn = status;
			SaveSoundSettings ();
		}

		protected virtual void SetSfx(bool status)
		{
			Settings.SfxOn = status;
			SaveSoundSettings ();
		}

		public virtual void MusicOn() { SetMusic (true); }
		public virtual void MusicOff() { SetMusic (false); }
		public virtual void SfxOn() { SetSfx (true); }
		public virtual void SfxOff() { SetSfx (false); }

		protected virtual void SaveSoundSettings()
		{
			SaveLoadManager.Save(Settings, _saveFileName, _saveFolderName);
		}

		protected virtual void LoadSoundSettings()
		{
			SoundSettings settings = (SoundSettings)SaveLoadManager.Load(_saveFileName, _saveFolderName);
			if (settings != null)
			{
				Settings = settings;
			}
		}

		protected virtual void ResetSoundSettings()
		{
			SaveLoadManager.DeleteSave(_saveFileName, _saveFolderName);
		}

		public virtual void OnMMEvent(MMSfxEvent sfxEvent)
		{
			PlaySound (sfxEvent.ClipToPlay, this.transform.position);
		} 

		public virtual void OnMMEvent(MMGameEvent gameEvent)
		{
			if (MuteSfxOnPause)
			{
				if (gameEvent.EventName == "inventoryOpens")
				{
					MuteAllSfx ();
				}
				if (gameEvent.EventName == "inventoryCloses")
				{
					UnmuteAllSfx ();
				}
			}
		} 

		public virtual void OnMMEvent(CorgiEngineEvent engineEvent)
		{
			if (engineEvent.EventType == CorgiEngineEventTypes.Pause)
			{
				if (MuteSfxOnPause)
				{
					MuteAllSfx ();
				}
			}
			if (engineEvent.EventType == CorgiEngineEventTypes.UnPause)
			{
				if (MuteSfxOnPause)
				{
					UnmuteAllSfx ();
				}
			}
		} 

		protected virtual void MuteAllSfx()
		{
			foreach(AudioSource source in _loopingSounds)
			{
				if (source != null)
				{
					source.mute = true;	
				}
			}
		}

		protected virtual void UnmuteAllSfx()
		{
			foreach(AudioSource source in _loopingSounds)
			{
				if (source != null)
				{
					source.mute = false;	
				}
			}
		}

		protected virtual void OnEnable()
		{
			this.MMEventStartListening<MMSfxEvent>();
			this.MMEventStartListening<MMGameEvent>();	
			this.MMEventStartListening<CorgiEngineEvent>();
			LoadSoundSettings ();
			_loopingSounds = new List<AudioSource> ();
		}

		protected virtual void OnDisable()
		{
			if (_enabled)
			{
				this.MMEventStopListening<MMSfxEvent>();	
				this.MMEventStopListening<MMGameEvent>();	
				this.MMEventStopListening<CorgiEngineEvent>();	
			}
		}
	}
}