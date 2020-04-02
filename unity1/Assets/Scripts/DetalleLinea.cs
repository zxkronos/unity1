using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetalleLinea : MonoBehaviour
{
    // Start is called before the first frame update
    public int myIndex; //es el index del numero de linea
    public bool isClickeado;
    private PanelLinea panel;
    public bool estaClickeado()
    {
        isClickeado = panel.estaClick;
        return isClickeado;
    }
    public void desclickPanel()
    {
        Debug.Log("in");
        panel.estaClick = false;
    }

    public void subirAct()
    {
        //Debug.Log("in");
        // NumLinea.MyInstance.subirLine(myIndex);
        EditorScript.MyInstance.subirAct(myIndex);
    }

    public void bajarAct()
    {
        EditorScript.MyInstance.bajarAct(myIndex);
    }

    public void eliminarAct()
    {
        EditorScript.MyInstance.eliminarAct(myIndex);
    }

    void Start()
    {
        panel = transform.GetChild(0).GetComponent<PanelLinea>();
    }

    // Update is called once per frame
    
}
