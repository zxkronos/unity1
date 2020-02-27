using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputWindow : MonoBehaviour
{
    public TMP_InputField StackField;
    public int MyIndex { get; set; }
    private EditorScript editor;
    private Item item;

    private void Awake()
    {
        StackField.text = "1";
        editor = EditorScript.MyInstance;
    }

        public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void CambiarStack()
    {
        item =(Item)editor.acts[MyIndex].MiUsable;
        int x = 0;

        Int32.TryParse(StackField.text, out x);
        item.stackSize = x;
    }
}
