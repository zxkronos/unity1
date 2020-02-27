﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    /// <summary>
    /// A reference to all the action buttons
    /// </summary>
    //[SerializeField]
    //private ActionButton[] actionButtons;

    [SerializeField]
    private CanvasGroup[] menus;


    [SerializeField]
    private GameObject targetFrame;

    private Stat healthStat;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Button botonplay;

    [SerializeField]
    private Image portraitFrame;

    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
   // private CharacterPanel charPanel;

    private Text tooltipText;

    [SerializeField]
    private RectTransform tooltipRect;

    /// <summary>
    /// A reference to the keybind menu
    /// </summary>
    [SerializeField]
    private CanvasGroup keybindMenu;

    [SerializeField]
    private CanvasGroup spellBook;

    /// <summary>
    /// A reference to all the kibind buttons on the menu
    /// </summary>
    private GameObject[] keybindButtons;

    private void Awake()
    {
       // keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start()
    {
        //healthStat = targetFrame.GetComponentInChildren<Stat>();
    }

    public void setbotonplayEnable(bool en)
    {
        botonplay.enabled = en;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(menus[0]);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenClose(menus[1]);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenClose(menus[2]);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            OpenClose(menus[3]);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenClose(menus[6]);
        }


        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    OpenClose(keybindMenu);
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    OpenClose(spellBook);
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    charPanel.OpenClose();
        //}
    }

 /*   public void ShowTargetFrame(Enemy target)
    {
        targetFrame.SetActive(true);

        healthStat.Initialize(target.MyHealth.MyCurrentValue, target.MyHealth.MyMaxValue);

        portraitFrame.sprite = target.MyPortrait;

        levelText.text = target.MyLevel.ToString();

        target.healthChanged += new HealthChanged(UpdateTargetFrame);

        target.characterRemoved += new CharacterRemoved(HideTargetFrame);

        if (target.MyLevel >= Player.MyInstance.MyLevel + 5)
        {
            levelText.color = Color.red;
        }
        else if (target.MyLevel == Player.MyInstance.MyLevel + 3 || target.MyLevel == Player.MyInstance.MyLevel + 4)
        {
            levelText.color = new Color32(255, 124, 0, 255);
        }
        else if (target.MyLevel >= Player.MyInstance.MyLevel -2 && target.MyLevel <= Player.MyInstance.MyLevel+2)
        {
            levelText.color = Color.yellow;
        }
        else if (target.MyLevel <= Player.MyInstance.MyLevel-3 && target.MyLevel > XPManager.CalculateGrayLevel())
        {
            levelText.color = Color.green;
        }
        else
        {
            levelText.color = Color.grey;
        }
    }*/

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    /// <summary>
    /// Updates the targetframe
    /// </summary>
    /// <param name="health"></param>
    public void UpdateTargetFrame(float health)
    {
     //   healthStat.MyCurrentValue = health;
    }

    /// <summary>
    /// Updates the text on a keybindbutton after the key has been changed
    /// </summary>
    /// <param name="key"></param>
    /// <param name="code"></param>
    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

   /* public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }
    */

    public void OpenClose(CanvasGroup canvasGroup)
    {
        //Debug.Log("in?");
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void OpenSingle(CanvasGroup canvasGroup)
    {
        //Debug.Log("in?");
        foreach (CanvasGroup canvas in menus)
        {
            CloseSingle(canvas);
        }

        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void CloseSingle(CanvasGroup canvasGroup)
    {
        Debug.Log("in?");
        canvasGroup.alpha  = 0;
        canvasGroup.blocksRaycasts = false;

    }

    /// <summary>
    /// Updates the stacksize on a clickable slot
    /// </summary>
    /// <param name="clickable"></param>
    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount >= 1) //If our slot has more than one item on it
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.enabled = true;
            clickable.MyIcon.enabled = true;
        }
        else //If it only has 1 item on it
        {
            clickable.MyStackText.enabled = false;
            clickable.MyIcon.enabled = true;
        }
        if (clickable.MyCount == 0) //If the slot is empty, then we need to hide the icon
        {
            clickable.MyStackText.enabled = false;
            clickable.MyIcon.enabled = false;
        }
    }

    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyStackText.enabled = false;
        clickable.MyIcon.enabled = true;
    }

    /// <summary>
    /// Shows the tooltip
    /// </summary>
    public void ShowTooltip(Vector2 pivot, Vector3 position, IDescribable description)
    {
        tooltipRect.pivot = pivot;
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        tooltipText.text = description.GetDescription();
    }

    /// <summary>
    /// Hides the tooltip
    /// </summary>
    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void RefreshTooltip(IDescribable description)
    {
        tooltipText.text = description.GetDescription();
    }
}
