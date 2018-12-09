using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    public GameObject startMenu;
    public Sprite secondImage;
    public Image backgroundImage;
    public float delay = 2f;
    public int level_num = 1;
    public Dialogue secondDialogue;

    public void EnableStartMenu ()
    {
        startMenu.SetActive(true);
    }

    public void DisableStartMenu()
    {
        startMenu.SetActive(false);
    }

    public void DisplaySecondImage()
    {
        backgroundImage.sprite = secondImage;
    }

    public void StartSecondDialogue()
    {
        DialogueManager.Instance.StartDialogue(secondDialogue);
    }

    public void StartSecondPhase()
    {
        StartCoroutine(secondPhase());
    }

    IEnumerator secondPhase()
    {
        //backgroundImage.GetComponent<Fade>().FadeOut();
        yield return new WaitForSeconds(delay);
        backgroundImage.sprite = secondImage;
        //backgroundImage.GetComponent<Fade>().FadeIn();
        DialogueManager.Instance.StartDialogue(secondDialogue);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(level_num);
    }
}
