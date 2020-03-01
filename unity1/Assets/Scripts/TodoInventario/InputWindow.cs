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
    public Item item;
    public ActScript act;
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
       // item =(Item)editor.MyItems;
        int x = 0;
        //Debug.Log(act.MyIndex);
       // Debug.Log("index " + MyIndex);
        
        if (Int32.TryParse(StackField.text, out x)) {

            EditorScript.MyInstance.acts[MyIndex-1].miStack = x; //el index actual donde se modifica el textfield
        }
        
        

    }
}
