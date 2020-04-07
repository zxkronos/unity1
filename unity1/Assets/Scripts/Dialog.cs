using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public static Dialog instance;
    public bool hayMensaje = false;
    public string mensaje;

    void Start()
    {
        index = 0;
        mensaje = "";
        textDisplay.text = "";
        //StartCoroutine(Type());  
    }

    public static Dialog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Dialog>();
            }

            return instance;
        }
    }

    public IEnumerator Type()
    {

        foreach (char letter in mensaje)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void enviarMensaje(string mensaje)
    {
        this.mensaje = mensaje;
        hayMensaje = true;
    }

    void Update()
    {
        if (hayMensaje)
        {
            StartCoroutine(Type());
            hayMensaje = false;
        }
    }
}
