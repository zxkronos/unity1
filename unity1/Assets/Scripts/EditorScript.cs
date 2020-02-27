using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    [SerializeField] public GameObject inputWinGO;
    public TMP_InputField InputFieldStack;

    [SerializeField]
    private GameObject lineaPrefab;

    [SerializeField]
    private GameObject actPrefab;

    [SerializeField]
    public int CantLineas;

    public LineaScript linea;
    public ActScript act;

    public List<LineaScript> lineas = new List<LineaScript>();

    public List<ActScript> acts = new List<ActScript>();

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


    public void AgregarLinea() //añade linea y act
    {
        if (HandScript.MyInstance.MyMoveable != null)
        {
            items.Add((Item)HandScript.MyInstance.MyMoveable); //añade lo que tiene el handscript en la lista de items
        }
        else if (ActScript.MyAct.MiUsable != null)
        {
            //Debug.Log("Holo");
            items.Add((Item)ActScript.MyAct.MiUsable);
            ActScript.MyAct.MiUsable = null;
        }

        int IndexActualLinea = linea.MyIndex; //numero de linea de codigo
        linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>(); //se agrega linea como gameobject
        linea.MyIndex = IndexActualLinea + 1; // num de linea de codigo
        int IndexActualAct = ActScript.MyAct.MyIndex;
        ActScript.MyAct = linea.transform.GetChild(0).GetComponent<ActScript>();
        ActScript.MyAct.MyIndex = IndexActualAct + 1;
        //ActScript.MyAct.transform.name = "hola";
        acts.Add(ActScript.MyAct);
        // 
        //Debug.Log(acts[0].MyIndex);
        
        //Debug.Log(linea.MyIndex);
        lineas.Add(linea);// lista de lineas
    }

    public void AgregarAct() //añade act horizontal es para el if por ejemplo
    {    // ActScript.MyAct es el act actual, acts es la lista de actos
        if (HandScript.MyInstance.MyMoveable != null)
        {
            items.Add((Item)HandScript.MyInstance.MyMoveable);
        }  
        else if (ActScript.MyAct.MiUsable !=null)
        {
            items.Add((Item)ActScript.MyAct.MiUsable);
            ActScript.MyAct.MiUsable = null;
        }

        int IndexActualAct = ActScript.MyAct.MyIndex;
        ActScript.MyAct = Instantiate(actPrefab, transform).GetComponent<ActScript>();
        ActScript.MyAct.MyIndex = IndexActualAct + 1;
        ActScript.MyAct.transform.parent = linea.transform;
        ActScript.MyAct.transform.localScale = new Vector3(0.5f, 0.5f, 1f) ;
        acts.Add(ActScript.MyAct);
    }

    private void Awake()
    {
        linea = FindObjectOfType<LineaScript>();
        linea.MyIndex = 1;
        //linea.name = "a "+linea.MyIndex;
        lineas.Add(linea);
        // LineaScript linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>();

        // Instantiate(lineaPrefab, transform).name = "hola";

        
    }
}
