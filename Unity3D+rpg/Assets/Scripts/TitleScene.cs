using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
	public GameObject startButton;
	public GameObject exitButton;

	public AudioSource clickAudio;

	private void Start()
	{
		//clickAudio = GetComponent<AudioSource>();
	}

	public void TitleSceneStart()
	{
		clickAudio.Play();
		StartCoroutine("AudioStart");
	}

	public void TitleSceneExit()
	{
		clickAudio.Play();
		StartCoroutine("AudioExit");
	}

	IEnumerator AudioStart()
	{
		yield return new WaitForSeconds(1.5f);
		LoadingSceneMgr.LoadScene("TownScene");
	}

	IEnumerator AudioExit()
	{
		yield return new WaitForSeconds(1.5f);
		Application.Quit();
	}
}
