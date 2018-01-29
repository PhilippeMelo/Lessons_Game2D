using UnityEngine;
using System.Collections;

public class SelectScene : MonoBehaviour {

    public void Credits()
    {
        StartCoroutine(waitCredits());
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
        Application.Quit();
    }

    IEnumerator waitCredits()
    {
        yield return new WaitForSeconds(0.3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("credits");
    }
}
