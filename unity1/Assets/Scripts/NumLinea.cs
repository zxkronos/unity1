using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumLinea : MonoBehaviour
{

    public int NumLineaClick; // guarda el numero de la linea clickeada.
    private DetalleLinea detalle;

    private static NumLinea instance;

    public static NumLinea MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NumLinea>();
            }

            return instance;
        }
    }
    void Start()
    {
        NumLineaClick = -1; //ninguna linea clickeada.
    }

    public void desClickLineaAnterior() //se activa al presionar otro panel
    {
        if (NumLineaClick > 0) // si es superior a 0 ya se clickeo un panel
        {
            detalle = transform.GetChild(NumLineaClick - 1).GetComponent<DetalleLinea>();
            detalle.transform.GetChild(0).GetComponent<PanelLinea>().Desclickear();
        }
    }

/*    public void subirLine(int pos)
    {
        if ( NumLineaClick > 1)
        {
            if (NumLineaClick < EditorScript.MyInstance.lineas.Count)
            {

                detalle = transform.GetChild(pos-1).GetComponent<DetalleLinea>();
                //detalle.subirAct();
                EditorScript.MyInstance.subirAct(pos-1);
                if (NumLineaClick > 0) // si es superior a 0 ya se clickeo un panel
                {
                    desClickLineaAnterior();
                    detalle = transform.GetChild(pos - 2).GetComponent<DetalleLinea>();
                    detalle.transform.GetChild(0).GetComponent<PanelLinea>().clickear();
                    NumLineaClick = pos - 1;
                }
            }
        }
    }
    public void bajarLine(int pos)
    {
        detalle = transform.GetChild(NumLineaClick - 1).GetComponent<DetalleLinea>();
        EditorScript.MyInstance.bajarAct(detalle.myIndex);
        desClickLineaAnterior();
        detalle = transform.GetChild(NumLineaClick).GetComponent<DetalleLinea>();
        detalle.transform.GetChild(0).GetComponent<PanelLinea>().clickear();
        NumLineaClick = NumLineaClick + 1;
    }*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && NumLineaClick > 1)
        {
            //subirAct();
            detalle = transform.GetChild(NumLineaClick - 1).GetComponent<DetalleLinea>();
            EditorScript.MyInstance.subirAct(detalle.myIndex);
            desClickLineaAnterior();
            detalle = transform.GetChild(NumLineaClick - 2).GetComponent<DetalleLinea>();
            detalle.transform.GetChild(0).GetComponent<PanelLinea>().clickear();
            NumLineaClick = NumLineaClick - 1;

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && NumLineaClick < EditorScript.MyInstance.lineas.Count-1 && NumLineaClick > 1)
        {
            detalle = transform.GetChild(NumLineaClick - 1).GetComponent<DetalleLinea>();
            EditorScript.MyInstance.bajarAct(detalle.myIndex);
            desClickLineaAnterior();
            detalle = transform.GetChild(NumLineaClick).GetComponent<DetalleLinea>();
            detalle.transform.GetChild(0).GetComponent<PanelLinea>().clickear();
            NumLineaClick = NumLineaClick + 1;
            //bajarAct();
        }
        if (Input.GetKeyDown(KeyCode.Delete) && NumLineaClick > 0)
        {
            
            detalle = transform.GetChild(NumLineaClick - 1).GetComponent<DetalleLinea>();
            desClickLineaAnterior();
            Destroy(transform.GetChild(EditorScript.MyInstance.lineas.Count-1).gameObject);
            detalle.eliminarAct();
        }
    }

}
