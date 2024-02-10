using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 2f;

    public void LoadStartMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
    public void LoadMainLevel()
    {
        StartCoroutine(LoadLevel(1));
    }
    public void ExitGame()
    {
        StartCoroutine(Exit());
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator Exit()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }
}
