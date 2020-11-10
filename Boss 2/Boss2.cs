using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Boss2 : MonoBehaviour
{

    #region Singleton
    public static Boss2 instance;
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
    public GameObject Folder;
    public GameObject SecureDriveOBJ;
    public MessengerScript messenger;
    public bool PhaseActive = true;
    public static bool phaseActive = true;

    [Header("Pushy")]
    public Animator pushyAnim;
    public Animator pushyTextAnim;
    public GameObject pushyOBJ;
    public GameObject pushyCanvasOBJ;
    public string pushySpeech;
    public TextMeshProUGUI pushyText;
    public AudioSource pushyAudio;

    public List<Vector2> DriveLocations = new List<Vector2>();
    public List<GameObject> SpawnObjects = new List<GameObject>();
    public List<string> Phase1SpawnCombinations = new List<string>();
    public List<string> SpawnCombinations = new List<string>();
    public string DoNotUse;
    #endregion
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    IEnumerator InitCoroutine()
    {
       // DataManager.Instance.reachedBossFight2 = true;
       // DataManager.Instance.SaveData();
        SpawnFolders(1);
        // Pushy Enters
        pushyAnim.SetTrigger("Enter");
        yield return new WaitForSeconds(2);
        // Pushy Greeting
        pushyCanvasOBJ.SetActive(true);
        pushyAnim.SetTrigger("TalkOnce");
        pushyTextAnim.SetTrigger("Once");
        pushyAudio.Play();
        pushyText.text = "";
        pushySpeech = "The Hacker is sending a courupt firewall stop you! Dont let your files touch the red!";
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
       // SpawnLasers();
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

    public void SpawnHackedBlocks()
    {

    }
}
