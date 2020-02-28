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
    private CmdMover mov;

    private void Awake()
    {
        StackField.text = "1";
        editor = EditorScript.MyInstance;
        MyIndex = 1;
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
        mov =(CmdMover)editor.acts[MyIndex].item;
        int x = 0;
     //   Debug.Log(mov);
        if (Int32.TryParse(StackField.text, out x)) {
           // Debug.Log("index " + MyIndex);
            mov.stackSize = x;
        }
        
        

    }
}
