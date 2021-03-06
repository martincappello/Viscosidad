﻿using Dialogues;
using UnityEngine;

/**
 * @class: Cuadro
 * Un film se constituye de una secuencia de cuadros.
 */
namespace Film
{
	public abstract class Cuadro : MonoBehaviour
	{	
		protected DialogueManager DialogueManager;

		public TextAsset Subtitle;
		public AudioClip AudioClip;

		private bool _playing;

		public virtual void Play()
		{
			if (DialogueManager != null)
			{
				DialogueManager.AudioClip = AudioClip;
				DialogueManager.Subtitle = Subtitle;
			}
			_playing = true;
		}

		public virtual void Stop()
		{
			if (DialogueManager != null)
			{
				DialogueManager.Stop ();
			}
			_playing = false;
		}

		protected virtual void Start()
		{
			DialogueManager = GetComponent<DialogueManager>();
		}

		public void togglePlay() {
			if (_playing) {
				Stop();
			} else {
				Play();
			}
		}

		public abstract void Setup();
	}
}

