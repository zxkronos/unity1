using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorScript : MonoBehaviour
{
    

    //For debugging
    [SerializeField]
    private Item[] items;

    //[SerializeField]
    //private Linea[] Lineas;
    private static EditorScript instance;

    [SerializeField]
    private GameObject lineaPrefab;

    [SerializeField]
    private GameObject actPrefab;

    public static EditorScript MyEditorInstance
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

    private void Awake()
    {
        LineaScript linea = Instantiate(lineaPrefab, transform).GetComponent<LineaScript>();
        
        Instantiate(lineaPrefab, transform).name = "hola";

         Instantiate(actPrefab, transform).transform.parent = linea.transform;
        //act.transform.parent = linea.transform;
        // Linea linea = (Linea)Instantiate(items[0]);
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
