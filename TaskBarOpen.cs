using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBarOpen : MonoBehaviour
{
    public Animator Anim;
    public static bool isMouseOver;
    public bool isOpen;
    public SpriteRenderer ClickedOrNot;
    public Sprite Clicked;
    public Sprite NotClicked;

    private GameCursor gameCursor;

    private void Start()
    {
        gameCursor = ReferenceManager.instance.GetGameCursor();
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isMouseOver)
        {
           if (!isOpen)
            {
                Anim.SetTrigger("Open");
                isOpen = true;
                ClickedOrNot.sprite = Clicked;
            }
            else
            {
                Anim.SetTrigger("Close");
                isOpen = false;
                ClickedOrNot.sprite = NotClicked;
            }
        }
    }
}
