using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botones : MonoBehaviour
{
    public Text codigo;
    public Text textoConsola;

    //botón que ejecuta el código.
    public void botonPlay()
    {
        //Debug.Log(codigo.text);
        string texto;
        textoConsola.text = codigo.text;
        texto = codigo.text;
        Debug.Log("hola "+ texto);
        Console.WriteLine("Hello World!");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
