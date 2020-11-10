using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [Header("Pause Main")]
    public GameObject Panel;
    public GameObject PauseMain;
    [Header("Tips")]
    public GameObject TipsMenu;
    [Header("Options")]
    public GameObject OptionsMenu;
    public GameObject DeleteSavesButton;
    public GameObject DeleteSavesMenu;
    [Header("Quit Screen")]
    public GameObject AttemptQuitMenu;

    public static bool isPaused;
    private GameCursor gameCursor;
    private TrailRenderer cursorTrail;
    private ParticleSystem partSystem;

    public void Start()
    {
        gameCursor = ReferenceManager.instance.GetGameCursor();
        cursorTrail = gameCursor.gameObject.GetComponent<TrailRenderer>();
        partSystem = gameCursor.gameObject.GetComponent<ParticleSystem>();
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            DeleteSavesButton.SetActive(true);
        }
        else
        {
            DeleteSavesButton.SetActive(false);
        }
        StartCoroutine(PauseCheckCoroutine());
    }

    IEnumerator PauseCheckCoroutine()
    {
        while (!isPaused)
        {
            if (!FadeIn.isFade && !DangerBar.gameOver)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isPaused = true;
                }
            }
            yield return null;
        }
        Pause();
        yield return new WaitForEndOfFrame();
        StartCoroutine(UnPauseCheckCoroutine());
    }

    IEnumerator UnPauseCheckCoroutine()
    {
        while (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = false;
            }
            yield return null;
        }
        Resume();
        yield return new WaitForEndOfFrame();
        StartCoroutine(PauseCheckCoroutine());      
    }

    public void Pause()
    {
        isPaused = true;
        Panel.SetActive(true);
        Time.timeScale = 0f;
        cursorTrail.enabled = false;
        partSystem.Clear();
    }

    public void Resume()
    {
        isPaused = false;
        Panel.SetActive(false);
        Time.timeScale = 1f;
        PauseMain.SetActive(true);
        OptionsMenu.SetActive(false);
        TipsMenu.SetActive(false);
        AttemptQuitMenu.SetActive(false);
        DeleteSavesMenu.SetActive(false);
        cursorTrail.enabled = true;
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
    }

    public void OpenOptionsMenu()
    {
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
        PauseMain.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void DeleteData()
    {
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
        ToggleDeleteSaveMenu(false);
        SaveSystem.DeleteFile();
    }

    public void OpenTipsMenu()
    {
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
        PauseMain.SetActive(false);
        TipsMenu.SetActive(true);
    }

    public void ToggleDeleteSaveMenu(bool value)
    {
        DeleteSavesMenu.SetActive(value);
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
    }


    public void QuitAttempt(bool value)
    {
        if (value)
        {
            PauseMain.SetActive(false);
            AttemptQuitMenu.SetActive(true);
        }
        else
        {
            PauseMain.SetActive(true);
            AttemptQuitMenu.SetActive(false);
        }
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
    }

    public void Quit()
    {
        StartCoroutine(QuitCoroutine());
    }

    IEnumerator QuitCoroutine()
    {
        Resume();
        StartCoroutine(FadeIn.instance.Fade());
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void BackButtonPressed()
    {
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
        PauseMain.SetActive(true);
        TipsMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }
}
