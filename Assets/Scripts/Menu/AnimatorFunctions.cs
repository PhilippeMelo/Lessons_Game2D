using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	public bool disableOnce;

	public enum Scenes { credits, exit, level1, mainMenu };
	public Scenes sceneTarget;

	void PlaySound(AudioClip whichSound)
	{
		if(!disableOnce)
		{
			menuButtonController.audioSource.PlayOneShot (whichSound);
		} else {
			disableOnce = false;
		}
	}

	void CallScene() {
		string sceneName = "";

		switch (sceneTarget)
		{
			case Scenes.credits:
				sceneName = "credits";
				break;
			case Scenes.exit:
				sceneName = "exit";
				break;
			case Scenes.level1:
				sceneName = "level1";
				break;
			case Scenes.mainMenu:
				sceneName = "mainMenu";
				break;
			default:
				break;
		}

		SelectScene.OpenScene(sceneName);
	}
}	
