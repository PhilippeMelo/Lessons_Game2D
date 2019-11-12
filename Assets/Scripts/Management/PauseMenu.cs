using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseScreen;
    public GameObject finishedLevelScreen;
    private SelectScene selectScene;
    public GameObject blackoutBack;
	private bool paused;
    private bool resign;
    public bool finishedLevel;
    private GameObject defaultSelectedButton;
    public GameObject finishedSelectedButton;

	void Start () {
		paused = resign = false;
        selectScene = GameObject.Find("Canvas").GetComponent<SelectScene>();
        defaultSelectedButton = GetComponent<SelectEvent>().selectedObject;
	}
	
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape) && !finishedLevel && !resign) {
            paused = !paused;
		}

        if (!resign)
        {
            if (paused || finishedLevel)
            {
                blackoutBack.SetActive(true);
                Pause();
            } else {
                blackoutBack.SetActive(false);
                UnPause();
            }

            if (paused) pauseScreen.SetActive(true); else pauseScreen.SetActive(false);

            if (finishedLevel)
            {
                GetComponent<SelectEvent>().selectedObject = finishedSelectedButton;
                finishedLevelScreen.SetActive(true);
            }  else {
                GetComponent<SelectEvent>().selectedObject = defaultSelectedButton;
                finishedLevelScreen.SetActive(false);
            }

        } else {
            blackoutBack.SetActive(true);
        }

	}

    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0.3f;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

	public void Return()
    {
		paused = !paused;
	}

	public void Exit()
    {
        resign = true;
        finishedLevel = false;
        Time.timeScale = 1f;
        Invoke("UnPause", 0.29f);
		selectScene.Menu();
	}

}
