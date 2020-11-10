using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public static bool isMouseOver;
    public SpriteRenderer ScannerLights;
    public Sprite Green;
    public Sprite Red;
    public Sprite Idle;


    void OnMouseOver()
    {
        isMouseOver = true;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }
    

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.GetComponent<MysteryFolder>() != null)
        {
            MysteryFolder mystery = Other.gameObject.GetComponent<MysteryFolder>();
            mystery.inScanner = true;
            mystery.IconRenderer.sortingOrder++;
            if (Other.CompareTag("Malware"))
            {
                ScannerLights.sprite = Red;
            }
            else if (Other.CompareTag("Personal"))
            {
                ScannerLights.sprite = Green;
            }

        }
    }

    void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.GetComponent<MysteryFolder>() != null)
        {
            MysteryFolder mystery = Other.gameObject.GetComponent<MysteryFolder>();
            mystery.inScanner = false;
            mystery.IconRenderer.sortingOrder--; 
            ScannerLights.sprite = Idle;

        }
    }
}
