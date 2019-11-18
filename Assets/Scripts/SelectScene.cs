using UnityEngine;
using System.Collections;

public class SelectScene : MonoBehaviour {

    AudioSource audio;
	public AudioClip[] audioClip;

	void Start(){
		audio = GetComponent<AudioSource> ();
	}

    public void PlaySound(int index) {
        audio.clip = audioClip[index];
		audio.Play();
    }

    public static void OpenScene(string scene)
    {
        if (scene == "exit")
            ExitGame();
        else {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }

    public void Play()
    {
        //Debug.Log("Apertou");
        StartCoroutine(waitPlay());
    }

    public void Menu()
    {
        //Debug.Log("Apertou");
        StartCoroutine(waitMenu());
    }

    public void Exit()
    {
        StartCoroutine(waitExit());
    }

    IEnumerator waitPlay()
    {
        yield return new WaitForSeconds(0.3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("level1");
    }

    IEnumerator waitMenu()
    {
        yield return new WaitForSeconds(0.3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
    }

    IEnumerator waitExit()
    {
        yield return new WaitForSeconds(0.3f);

    }

    public static void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
