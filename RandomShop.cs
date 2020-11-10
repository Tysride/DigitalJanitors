using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomShop : MonoBehaviour
{
    public GameObject PlayerItem;

    public string itemName;

    public int abilityNum;

    public int price;

    public static bool isMouseOver;

    public GameCursor gameCursor;

    void Start()
    {
        if (worldCam.shop != true)
        {
            worldCam.shop = true;
        }

        if (abilityNum == 1)
        {
            if (DataManager.Instance.AbilityNums.Contains(1))
            {
                Destroy(gameObject);
            }
            else
            {
                price = 80;
            }
        }

        if (abilityNum == 2)
        {
            if (DataManager.Instance.AbilityNums.Contains(2))
            {
                Destroy(gameObject);
            }
            else
            {
                price = 100;
            }
        }

        if (abilityNum == 3)
        {
            if (DataManager.Instance.AbilityNums.Contains(3))
            {
                Destroy(gameObject);
            }
            else
            {
                price = 100;
            }
        }
    }

    void OnMouseOver()
    {
        isMouseOver = true;
        gameCursor.UpdateSprite(GameCursor.CursorState.Selected);
    }
    void OnMouseExit()
    {
        isMouseOver = false;
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
    }

    public void ButtonSelected()
    {
        isMouseOver = false;
        gameCursor.UpdateSprite(GameCursor.CursorState.NotSelected);
        if (DataManager.Instance.currency >= price)
        {
            if (DataManager.Instance.AbilityNums[0] == 0)
            {
                DataManager.Instance.currency -= price;
                DataManager.Instance.AbilityNums[0] = abilityNum;
                Destroy(gameObject);
                DataManager.Instance.SaveData();
                return;
            }
            else if (DataManager.Instance.AbilityNums[1] == 0)
            {
                DataManager.Instance.currency -= price;
                DataManager.Instance.AbilityNums[1] = abilityNum;
                Destroy(gameObject);
                DataManager.Instance.SaveData();
                return;
            }
            else if (DataManager.Instance.AbilityNums[2] == 0)
            {
                DataManager.Instance.currency -= price;
                DataManager.Instance.AbilityNums[2] = abilityNum;
                Destroy(gameObject);
                DataManager.Instance.SaveData();
                return;
            }
        }
    }
}
