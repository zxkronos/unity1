using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetalleLinea : MonoBehaviour
{
    // Start is called before the first frame update
    public int myIndex; //es el index del numero de linea


    public void subirAct()
    {
        //Debug.Log("in");
        EditorScript.MyInstance.subirAct(myIndex);
    }

    public void bajarAct()
    {
        EditorScript.MyInstance.bajarAct(myIndex);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
