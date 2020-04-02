using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorScript : MonoBehaviour
{


    //For debugging
    [SerializeField]
    //private Item[] items;

    //[SerializeField]
    //private Linea[] Lineas;
    private static EditorScript instance;

    private List<Item> items = new List<Item>();

    public List<Item> MyItems
    {
        get
        {
            return items;
        }

    }

    public int contMov; //contador de movimiento.

    [SerializeField] public GameObject inputWinGO;
    public GameObject detalleLinea;
    private bool activarError;
    //public TMP_InputField InputFieldStack;
    public Color colorPanelIn;
    public Color colorPanelOut;

    [SerializeField]
    private GameObject lineaPrefab;

    [SerializeField]
    private GameObject actPrefab;

    [SerializeField]
    private GameObject ContentEditor;

    [SerializeField]
    private GameObject scrollBar;

    [SerializeField]
    public int CantLineas;

    public DetalleLinea detalle;
    public GameObject numLineaGO;

    public LineaScript linea;
    public ActScript act;
    public ActScript actSig;
    public bool siCompleto; // para validar si los codigos si tienen fin
    public bool sinoCompleto; // para validar si los codigos si tienen fin


    public List<LineaScript> lineas = new List<LineaScript>();
    public List<DetalleLinea> detalles = new List<DetalleLinea>();

    public List<ActScript> acts = new List<ActScript>();
    public List<TMP_InputField> inputs = new List<TMP_InputField>();
    private void Awake()
    {
        contMov = 0;
        linea = FindObjectOfType<LineaScript>();
        linea.MyIndex = 1;
        //linea.name = "a "+linea.MyIndex;
        lineas.Add(linea);
        // LineaScript linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>();
        siCompleto = true;
        sinoCompleto = true;
        // Instantiate(lineaPrefab, transform).name = "hola";
        if (act == null)
        {
            act = FindObjectOfType<ActScript>(); //primer Act por defecto
            ActScript.MyAct = act;
            act.MyIndex = 1;
            acts.Add(act);
        }

        detalle = FindObjectOfType<DetalleLinea>();
        detalle.myIndex = 1;
        detalles.Add(detalle);
        var color = detalle.transform.GetChild(1).GetComponent<Image>().color;
        color.a = 0f;
        detalle.transform.GetChild(1).GetComponent<Image>().color = color;
        numLineaGO = GameObject.Find("NumLinea");
        activarError = false;
        cambiarColorInEx = false;
        cambiarColorOutEx = false;
        posCambiarColor = -1;

    }



    public static EditorScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EditorScript>();
            }

            return instance;
        }
    }

    public void subirAct(int posicion)
    {
        EditorScript.MyInstance.desactivarErroresLineas();
        if (posicion > 1 && posicion != lineas.Count) //la linea 1 no puede subir mas
        {
            lineas[posicion - 1].transform.SetSiblingIndex(posicion - 2);
            LineaScript lineAux = lineas[posicion - 2]; //linea que esta arriba de la que quiero subir
            //Debug.Log(lineas[posicion - 1].transform.GetSiblingIndex());
            lineas[posicion - 2] = lineas[posicion - 1];
            lineas[posicion - 1] = lineAux;
        }
    }
    public void bajarAct(int posicion)
    {
        desactivarErroresLineas();
        if (posicion != lineas.Count - 1 && posicion != lineas.Count) //la ultima y penultima no pueden bajar
        {
            lineas[posicion - 1].transform.SetSiblingIndex(posicion); //se le pone el index del hermano de delante
            LineaScript lineAux = lineas[posicion]; //linea que esta arriba de la que quiero subir
            Debug.Log(lineas[posicion - 1].transform.GetSiblingIndex());
            lineas[posicion] = lineas[posicion - 1];
            lineas[posicion - 1] = lineAux;

        }
    }
    public void eliminarAct(int posicion)
    {
        
        Destroy(lineas[posicion - 1].gameObject);
        lineas.Remove(lineas[posicion - 1]);
    }

    private int numLine;
    private List<int> lineasConError = new List<int>();
    public void activarErrorLinea(int numLinea)
    {
        activarError = true;
        lineasConError.Add(numLinea);
        numLine = numLinea;
    }

    public void desactivarErroresLineas()
    {
        activarError = true;
        numLine = -1;
        //Thread.Sleep(50);
    }
    private bool cambiarColorInEx;
    private bool cambiarColorOutEx;
    private int posCambiarColor;
    public void cambiarColorEjecucion(int i)
    {
        if(i == posCambiarColor)
        {
            cambiarColorOutEx = true;
        }
        else
        {
            cambiarColorInEx = true;
            posCambiarColor = i;
        }
    }

    void Update()
    {
        if (activarError) //activa y desactiva errores
        {
            if(numLine == -1)
            {
                foreach (int num in lineasConError)
                {
                    detalle = numLineaGO.transform.GetChild(num).GetComponent<DetalleLinea>();
                    var color = detalle.transform.GetChild(1).GetComponent<Image>().color;
                    color.a = 0f;
                    detalle.transform.GetChild(1).GetComponent<Image>().color = color;
                    

                }
                lineasConError.Clear();
                activarError = false;
            }
            else
            {
                detalle = numLineaGO.transform.GetChild(numLine).GetComponent<DetalleLinea>();
                var color = detalle.transform.GetChild(1).GetComponent<Image>().color;
                color.a = 1f;
                detalle.transform.GetChild(1).GetComponent<Image>().color = color;
                //detalle.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
                activarError = false;
            }
            
        }
        if (cambiarColorInEx)
        {
            detalle = numLineaGO.transform.GetChild(posCambiarColor).GetComponent<DetalleLinea>();
            detalle.transform.GetChild(0).GetComponent<Image>().color = colorPanelIn;
            cambiarColorInEx = false;
        }else if (cambiarColorOutEx)
        {
            detalle = numLineaGO.transform.GetChild(posCambiarColor).GetComponent<DetalleLinea>();
            detalle.transform.GetChild(0).GetComponent<Image>().color = colorPanelOut;
            cambiarColorOutEx = false;
        }

    }

    public void AgregarLinea() //añade linea y act
    {
        //desactivarErroresLineas();
        if (HandScript.MyInstance.MyMoveable != null)
        {
            items.Add((Item)HandScript.MyInstance.MyMoveable); //añade lo que tiene el handscript en la lista de items
        }
        else if (act.MiUsable != null)
        {
            
            items.Add((Item)act.MiUsable);
            act.MiUsable = null;
        }

        //instanciar bloque de codigo
        detalle = Instantiate(detalleLinea, numLineaGO.transform).GetComponent<DetalleLinea>();
        var color = detalle.transform.GetChild(1).GetComponent<Image>().color;
        color.a = 0f; //el error inicia transparente
        detalle.transform.GetChild(1).GetComponent<Image>().color = color;

        //Agranda el content para que lo detecte el scrollbar cuando llegue al final de la pantalla
        RectTransform rect = ContentEditor.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 50);
        scrollBar.GetComponent<Scrollbar>().value = 0; // con el value en cerro el scrollbar va hasta el ultimo codigo añadido

        //cada linea tiene los actos que se le ingresan.
        linea.actosLinea.Add(act);
        int IndexActualLinea = linea.MyIndex; //numero de linea de codigo
        linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>(); //se agrega linea como gameobject
        linea.MyIndex = IndexActualLinea + 1; // num de linea de codigo

        detalle.myIndex = linea.MyIndex;
        //poner numero de linea de codigo en el canvas
        detalle.transform.GetChild(2).GetComponent<Text>().text = "" +linea.MyIndex;
        detalles.Add(detalle);

        int IndexActualAct = act.MyIndex;
        act = linea.transform.GetChild(0).GetComponent<ActScript>(); //Obtener hijo de linea que es un act
        ActScript.MyAct = act;
        act.MyIndex = IndexActualAct + 1; // el indexs 
        //act.transform.name = "hola";
        acts.Add(act);
        // 
        //Debug.Log(acts[0].MyIndex);
        
        //Debug.Log(linea.MyIndex);
        lineas.Add(linea);// lista de lineas
    }

    public void AgregarAct() //añade act horizontal es para el if por ejemplo
    {    // act es el act actual, acts es la lista de actos
         // Debug.Log(acts);
       // desactivarErroresLineas();
        if (HandScript.MyInstance.MyMoveable != null)
        {
            items.Add((Item)HandScript.MyInstance.MyMoveable);
        }  
        else if (act.MiUsable !=null)
        {
            items.Add((Item)act.MiUsable);
            act.MiUsable = null;
        }
        linea.actosLinea.Add(act);
        int IndexActualAct = act.MyIndex;
        act = Instantiate(actPrefab, transform).GetComponent<ActScript>();
        ActScript.MyAct = act;
        act.MyIndex = IndexActualAct + 1;
        act.transform.parent = linea.transform;
        
        act.transform.localScale = new Vector3(0.5f, 0.5f, 1f) ;
        acts.Add(act);
    }

    
}
