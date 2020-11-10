using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Variables
    public bool laserActive;

    public GameObject bomb;

    public AudioSource chargeSFX;

    public List<FolderClass> folderList = new List<FolderClass>();
    #endregion
    private void Start()
    {
        chargeSFX.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FolderClass>() != null)
        {
            FolderClass folderClass = other.GetComponent<FolderClass>();
            folderList.Add(folderClass);
            StartCoroutine(CollisionCoroutine(other.gameObject, folderClass));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<FolderClass>() != null)
        {
            FolderClass folderClass = other.GetComponent<FolderClass>();
            folderList.Remove(folderClass);
        }
    }

    IEnumerator CollisionCoroutine(GameObject obj, FolderClass folderClass)
    {
        while (folderList.Contains(folderClass))
        {
            if (laserActive && obj != null)
            {
                Shake.instance.CamBigShake();
                FolderManager.instance.allFolders.Remove(obj);
                GameObject Bomb = Instantiate(bomb, obj.transform.position, obj.transform.rotation);
                Destroy(Bomb, 3);
                yield return new WaitForEndOfFrame();
                Destroy(obj);
                DangerBar.instance.slider.value += .4f;
                DangerBar.instance.BossHealthCheck();
                yield return new WaitForEndOfFrame();
                if (!DangerBar.instance.GameOver)
                {
                    LaserBoss.instance.CheckFolderCount();
                }
                Destroy(gameObject);
            }
            yield return null;
        }
        yield return null;
    }
}
