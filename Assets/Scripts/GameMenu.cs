using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameMenu : MonoBehaviour
{
	public Image black;
	public Animator anim;
	private AudioManager audioManager;
	public void PlayGame()
	{
		PlayClick();
		StartCoroutine(Fading(new Action(() => SceneManager.LoadScene("Game"))));
	}

	public void ExitGame()
	{
		PlayClick();
		StartCoroutine(Fading(new Action(() => Application.Quit())));
	}
	private void PlayClick()
	{
		if (!audioManager)
		{
			audioManager = FindObjectOfType<AudioManager>();
		}
		audioManager.Play("ButtonClick");
	}

	IEnumerator Fading(Action action)
	{
		anim.SetBool("Fade", true);
		yield return new WaitUntil(() => black.color.a == 1);
		action();
	}
}
