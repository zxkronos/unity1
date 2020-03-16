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
    public ActScript actSig;

    public List<LineaScript> lineas = new List<LineaScript>();

    public List<ActScript> acts = new List<ActScript>();
    public List<TMP_InputField> inputs = new List<TMP_InputField>();
    private void Awake()
    {
        linea = FindObjectOfType<LineaScript>();
        linea.MyIndex = 1;
        //linea.name = "a "+linea.MyIndex;
        lineas.Add(linea);
        // LineaScript linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>();

        // Instantiate(lineaPrefab, transform).name = "hola";
        if (act == null)
        {
            act = FindObjectOfType<ActScript>(); //primer Act por defecto
            ActScript.MyAct = act;
            act.MyIndex = 1;
            acts.Add(act);    
        }

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


    public void AgregarLinea() //añade linea y act
    {
        if (HandScript.MyInstance.MyMoveable != null)
        {
            items.Add((Item)HandScript.MyInstance.MyMoveable); //añade lo que tiene el handscript en la lista de items
        }
        else if (act.MiUsable != null)
        {
            //Debug.Log("Holo");
            items.Add((Item)act.MiUsable);
            act.MiUsable = null;
        }

        int IndexActualLinea = linea.MyIndex; //numero de linea de codigo
        linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>(); //se agrega linea como gameobject
        linea.MyIndex = IndexActualLinea + 1; // num de linea de codigo
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

        if (HandScript.MyInstance.MyMoveable != null)
        {
            items.Add((Item)HandScript.MyInstance.MyMoveable);
        }  
        else if (act.MiUsable !=null)
        {
            items.Add((Item)act.MiUsable);
            act.MiUsable = null;
        }
        
        int IndexActualAct = act.MyIndex;
        act = Instantiate(actPrefab, transform).GetComponent<ActScript>();
        ActScript.MyAct = act;
        act.MyIndex = IndexActualAct + 1;
        act.transform.parent = linea.transform;
        act.transform.localScale = new Vector3(0.5f, 0.5f, 1f) ;
        acts.Add(act);
    }

    
}
