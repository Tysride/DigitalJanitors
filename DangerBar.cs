using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class DangerBar : MonoBehaviour
{
    #region Singleton
    public static DangerBar instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    #endregion

    // Unity UI References
    public Slider slider;

    // Slider's value
    public static float currentValue = 0f;
    public Animator anim;
    public Animator VFXAnim;
    public Animator endAnim;
    public AudioSource sound;
    public AudioSource music;
    public bool GameOver = false;
    public static bool gameOver;

    [Header("Payload")]
    public float maxCapacity = 1;
    public Image redSlider;

    void Start()
    {
        anim.SetTrigger("NoWarn");
        currentValue = 0;
        slider.value = 0;
        maxCapacity = 1;
        redSlider = ReferenceManager.instance.redSliderVisual;
    }

    public void FolderCountChanged()
    {
        // Updates the Disk Space based on Folder Count
        currentValue = (FolderManager.instance.allFolders.Count * .05f);
        slider.value = currentValue;
        if (slider.value < .5)
        {
            anim.SetTrigger("NoWarn");
            VFXAnim.SetTrigger("LVL0");
            if (music.pitch > 1)
            {
                music.pitch -= .25f;
            }
        }
        else if (slider.value < .75f)
        {
            anim.SetTrigger("NoWarn");
            VFXAnim.SetTrigger("LVL1");
        }
        else if (slider.value < .85f)
        {
            anim.SetTrigger("Warn01");
            VFXAnim.SetTrigger("LVL2");
            if (music.pitch > 1)
            {
                music.pitch -= .25f;
            }
        }
        else
        {
            anim.SetTrigger("Warn02");
            VFXAnim.SetTrigger("LVL3");
            if (music.pitch < 1.25f)
            {
                music.pitch += .25f;
            }
        }
        if (slider.value >= maxCapacity && GameOver == false)
        {
            // Script to end game 
            sound.Play();
            endAnim.SetTrigger("End");
            StartCoroutine(EndGameCoroutine());
            GameOver = true;
        }
    }

    public void BossHealthCheck()
    {
        if (slider.value >= 1 && !GameOver)
        {
            // Script to end game 
            GameOver = true;
            gameOver = true;
            sound.Play();
            endAnim.SetTrigger("End");
            StartCoroutine(EndGameCoroutine());
        }
    }

    public IEnumerator CorruptedSpaceDecayCoroutine()
    {
        float desiredAmount = 0;
        while (redSlider.fillAmount > desiredAmount)
        {
            redSlider.fillAmount -= Time.deltaTime / 200;
            maxCapacity += Time.deltaTime / 200;
            yield return null;
        }
    }

    IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        gameOver = false;
        SceneManager.LoadScene("Final");
    }
}
