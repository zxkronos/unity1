using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelLinea : MonoBehaviour, IPointerClickHandler
{
    public Color colorPanelClick;
    public bool estaClick;
    private DetalleLinea detalle;
    public Color colorNormalPanel;

    void Start()
    {
        estaClick = false;
        detalle= transform.parent.gameObject.GetComponent<DetalleLinea>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!estaClick && detalle.myIndex != EditorScript.MyInstance.lineas.Count && BotonPlay.PlayInstance.threadTerminado)
            {
                NumLinea.MyInstance.desClickLineaAnterior();
                GetComponent<Image>().color = colorPanelClick;
                NumLinea.MyInstance.NumLineaClick = detalle.myIndex;
                estaClick = true;
               // Debug.Log(NumLinea.MyInstance.NumLineaClick);
            }
            else if (BotonPlay.PlayInstance.threadTerminado)
            {
                GetComponent<Image>().color = colorNormalPanel;
                //NumLinea.MyInstance.NumLineaClick = -1;
                estaClick = false;
            }
        }
    }
    
    public void Desclickear()
    {
        GetComponent<Image>().color = colorNormalPanel;
        //NumLinea.MyInstance.NumLineaClick = -1;
        estaClick = false;
    }

    public void clickear()
    {
        GetComponent<Image>().color = colorPanelClick;
        estaClick = true;
    }
    
}
