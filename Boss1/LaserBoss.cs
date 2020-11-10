using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LaserBoss : MonoBehaviour
{
    #region Singleton
    public static LaserBoss instance;
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

    #region Variables
    public int PhaseCount = 1;
    public int lasersFired;
    public GameObject Folder;
    public GameObject CurrentPhaseLaser;
    public GameObject Laser;
    public GameObject SecureDriveOBJ;
    public MessengerScript messenger;
    public bool PhaseActive = true;
    public static bool phaseActive = true;
    public int timeBetweenFire;

    [Header("Pushy")]
    public Animator pushyAnim;
    public Animator pushyTextAnim;
    public GameObject pushyOBJ;
    public GameObject pushyCanvasOBJ;
    public string pushySpeech;
    public TextMeshProUGUI pushyText;
    public AudioSource pushyAudio;

    public List<Vector2> DriveLocations = new List<Vector2>();
    public List <GameObject> SpawnObjects = new List<GameObject>();
    public List<string> Phase1SpawnCombinations = new List<string>();
    public List<string> SpawnCombinations = new List<string>();
    public string DoNotUse;
    #endregion

    void Start()
    {
        CurrentPhaseLaser = Laser;
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        DataManager.Instance.reachedBossFight = true;
        DataManager.Instance.SaveData();
        SpawnFolders(3);
        // Pushy Enters
        pushyAnim.SetTrigger("Enter");
        yield return new WaitForSeconds(2);
        // Pushy Greeting
        pushyCanvasOBJ.SetActive(true);
        pushyAnim.SetTrigger("TalkOnce");
        pushyTextAnim.SetTrigger("Once");
        pushyAudio.Play();
        pushyText.text = "";
        pushySpeech = "Don't let his lasers touch those files, they'll be destroyed immediately!!";
        foreach (char letter in pushySpeech.ToCharArray())
        {
            pushyText.text += letter;
            yield return new WaitForSeconds(.015f);
        }
        pushyAudio.Stop();
        yield return new WaitForSeconds(2);
        pushyCanvasOBJ.SetActive(false);
        pushyAnim.SetTrigger("Leave");
        yield return new WaitForSeconds(1);
        pushyOBJ.SetActive(false);
        yield return new WaitForSeconds(2);
        SpawnLasers();
    }

    public void SpawnFolders(int numFolders)
    {
        for (int i = 0; i < numFolders; i++)
        {
            Vector3 spawnPosition = new Vector3();
            int random = Random.Range(1, 5);
            if (random == 1)
            {
                float spawnY = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(Screen.height, 0)).y);
                float spawnX = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                spawnPosition = new Vector3(spawnX, spawnY, 0);

            }
            else if (random == 2)
            {
                float spawnY = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                spawnPosition = new Vector3(spawnX, spawnY, 0);
            }
            else if (random == 3)
            {
                float spawnY = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(Screen.height, 0)).y);
                float spawnX = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(0, Screen.width)).x);
                spawnPosition = new Vector3(spawnX, spawnY, 0);
            }
            else if (random == 4)
            {
                float spawnY = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range(0, ReferenceManager.instance.spawnCam.ScreenToWorldPoint(new Vector2(0, Screen.width)).x);
                spawnPosition = new Vector3(spawnX, spawnY, 0);
            }
            GameObject obj = Instantiate(Folder, spawnPosition, Quaternion.identity);
            obj.AddComponent<MarqueeAid>();
            obj.AddComponent<MagnetAid>();
            obj.GetComponent<FolderClass>().AssignFolderType("Personal");
            FolderManager.instance.allFolders.Add(obj);
        }
    }

    public void SpawnLasers()
    {
        if (PhaseActive)
        {
            StartCoroutine(GetSpawnCombo());
            StartCoroutine(LaserCoroutine());
        }
    }


    public IEnumerator GetSpawnCombo()
    {
        if (PhaseCount == 1)
        {
            int random;
            random = Random.Range(0, Phase1SpawnCombinations.Count);
            string combo = Phase1SpawnCombinations[random];
            if (DoNotUse != "")
            {
                Phase1SpawnCombinations.Add(DoNotUse);
            }
            DoNotUse = combo;
            string[] splitCombo = combo.Split(',');
            int i = int.Parse(splitCombo[0]);
            GameObject location = SpawnObjects[i];
            GameObject laserOBJ = Instantiate(CurrentPhaseLaser, location.transform.position, location.transform.rotation);
            Destroy(laserOBJ, 10);
            yield return new WaitForSeconds(1);
            int i2 = System.Convert.ToInt32(splitCombo[1]);
            GameObject location2 = SpawnObjects[i2];
            GameObject laserOBJ2 = Instantiate(CurrentPhaseLaser, location2.transform.position, location2.transform.rotation);
            Destroy(laserOBJ2, 10);
            Phase1SpawnCombinations.Remove(DoNotUse);
        }
        else
        {
            int random;
            random = Random.Range(0, SpawnCombinations.Count);
            string combo = SpawnCombinations[random];
            string[] splitCombo = combo.Split(',');
            int i = int.Parse(splitCombo[0]);
            if (DoNotUse != "")
            {
                SpawnCombinations.Add(DoNotUse);
            }
            DoNotUse = combo;
            GameObject location = SpawnObjects[i];
            GameObject laserOBJ = Instantiate(CurrentPhaseLaser, location.transform.position, location.transform.rotation);
            Destroy(laserOBJ, 10);
            yield return new WaitForSeconds(1);
            int i2 = System.Convert.ToInt32(splitCombo[1]);
            GameObject location2 = SpawnObjects[i2];
            GameObject laserOBJ2 = Instantiate(CurrentPhaseLaser, location2.transform.position, location2.transform.rotation);
            Destroy(laserOBJ2, 10);
            yield return new WaitForSeconds(2);
            int i3 = System.Convert.ToInt32(splitCombo[2]);
            GameObject location3 = SpawnObjects[i3];
            GameObject laserOBJ3 = Instantiate(CurrentPhaseLaser, location3.transform.position, location3.transform.rotation);
            Destroy(laserOBJ3, 10);
            SpawnCombinations.Remove(DoNotUse);
        }
    }


    IEnumerator LaserCoroutine()
    {
        if (PhaseActive)
        {
            if (lasersFired == 1)
            {
                // After every other laser fire, the secure drive appears in a given location for a short period of time
                yield return new WaitForSeconds(timeBetweenFire);
                SecureDriveOBJ.transform.position = DriveLocations[Random.Range(0, DriveLocations.Count)];
                SecureDriveOBJ.SetActive(true);
                yield return new WaitForSeconds(2);
                SecureDriveOBJ.transform.position = new Vector3(0,50,0);
                lasersFired -= 1;
                yield return new WaitForSeconds(1);
                SpawnLasers();
            }
            else
            {
                lasersFired++;
                yield return new WaitForSeconds(timeBetweenFire);
                SpawnLasers();
            }

        }
        else
        {
            yield return null;
        }
    }
    
    public void CheckFolderCount()
    {
        if (FolderManager.instance.allFolders.Count == 0 && PhaseActive)
        {
            StopAllCoroutines();
            PhaseActive = false;
            phaseActive = false;
            PhaseCount++;
            StartCoroutine(messenger.InitDialogue());
            StartCoroutine(NextPhaseCoroutine());
        }
    }

    IEnumerator NextPhaseCoroutine()
    {
        while (!PhaseActive)
        {
            yield return null;
        }
        lasersFired = 0;
        DoNotUse = "";
        yield return new WaitForSeconds(.5f);
        if (PhaseCount == 2)
        {
            timeBetweenFire = 8;
            DoNotUse = "";
            SpawnFolders(4);
        }
        else if (PhaseCount == 3)
        {
            SpawnFolders(5);
        }
        else if (PhaseCount == 4)
        {
            DataManager.Instance.levelNum = 7;
            DataManager.Instance.SaveData();
            StartCoroutine(FadeIn.instance.Fade());
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Overworld");
        }
        yield return new WaitForSeconds(2);
        PhaseActive = true;
        phaseActive = true;
        SpawnLasers();
    }
}