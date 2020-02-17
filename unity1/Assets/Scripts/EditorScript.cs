using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private GameObject lineaPrefab;

    [SerializeField]
    private GameObject actPrefab;

    [SerializeField]
    public int CantLineas;

    private LineaScript linea;
    private ActScript act;

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
    
    public void AgregarLinea()
    {
        int IndexActual = linea.MyIndex;
        linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>();
        linea.MyIndex = IndexActual + 1;
        items.Add((Item)HandScript.MyInstance.MyMoveable);
       //Debug.Log(linea.MyIndex);
        lineas.Add(linea);
    }
    public void AgregarAct()
    {
        items.Add((Item)HandScript.MyInstance.MyMoveable);
        act = Instantiate(actPrefab, transform).GetComponent<ActScript>();
        act.transform.parent = linea.transform;
        act.transform.localScale = new Vector3(0.5f, 0.5f, 1f) ;
    }

    private void Awake()
    {
        linea = FindObjectOfType<LineaScript>();
        linea.MyIndex = 1;
        linea.name = "a "+linea.MyIndex;
        lineas.Add(linea);
        // LineaScript linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>();

        // Instantiate(lineaPrefab, transform).name = "hola";

        //    ActScript act = Instantiate(actPrefab, transform).GetComponent<ActScript>();
        //  act.transform.parent = linea.transform;
        //act.transform.localScale = new Vector3(0.5f, 0.5f, 1f) ;

        /*Bag bag = (Bag)Instantiate(items[8]);
        bag.Initialize(20);

        bag.Use();
        

        Bag bag2 = (Bag)Instantiate(items[8]);
        bag2.Initialize(20);

        bag2.Use();

        AgregarItem(bag2, (Armor)Instantiate(items[0]));
        AgregarItem(bag2, (Armor)Instantiate(items[1]));
        */
    }
}
