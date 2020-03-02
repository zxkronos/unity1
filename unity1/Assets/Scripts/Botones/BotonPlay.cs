using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Jobs;
using Unity.Collections;
using System.Threading;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Tilemaps;

public class BotonPlay : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private List<ActScript> acts = new List<ActScript>();
    private Item item;
    public Thread _t1;
    public List<Thread> ListaDeHilos;
    public int numHilos;
    public bool _t1Paused = false;
    public Vector3 posicionIncial;
    public bool threadTerminado;
    public Tilemap highlightMap;
    [SerializeField]
    public Tile grass;
    [SerializeField]
    public Tile sand;

    public float timeWaiting = 5.0f;

    private static BotonPlay instance;

    private UIManager ui;

    private Player player;

    public static BotonPlay PlayInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BotonPlay>();
            }

            return instance;
        }
    }

    void Awake()
    {
        player = Player.MyInstance;
        ui = UIManager.MyInstance;
        ListaDeHilos = new List<Thread>();
        numHilos = 0;
        posicionIncial = player.transform.position;
        threadTerminado = true;
       // botonplay = Transform.GetComponent<Button>(); 
    }
    void Start()
    {
        

    }

    public void Ejecutar()
    {
        
        try
        {
            foreach (ActScript act in acts)
            {
                //Debug.Log("inforach");
                item = act.item;

                if (item is CmdMover)
                {
                    
                    CmdMover mov = (CmdMover)item;
                    //Debug.Log("stack "+act.miStack);
                    for (int i = 0; i< act.miStack; i++)
                    {
                        if (mov.moveType.ToString() == "MoverAdelante")
                        {
                            //Debug.Log("hola cmdmover " + item.MyTitle);
                            _t1Paused = false;
                            player.moverAdelante();
                            //Debug.Log("oli " + item.MyTitle);
                            Thread.Sleep(1000);
                            /*while (_t1Paused)
                            {
                            }*/

                        }
                        else if (mov.moveType.ToString() == "GirarDerecha")
                        {
                            //_t1Paused = true;
                            player.GirarDerecha();
                            //Debug.Log("oli " + item.MyTitle);
                            Thread.Sleep(500);

                        }
                        else if (mov.moveType.ToString() == "GirarIzquierda")
                        {
                            //Debug.Log("in");
                            //_t1Paused = true;
                            player.GirarIzquierda();


                            Thread.Sleep(500);
                        }
                        else if (mov.moveType.ToString() == "MoverAtras")
                        {
                            _t1Paused = false;
                            player.moverAtras();
                            //Debug.Log("oli " + item.MyTitle);
                            Thread.Sleep(1000);
                            /*  while (_t1Paused)
                              {
                              }*/


                        }
                    }
                    //Debug.Log(mov.moveType.ToString());
                    

                }

            }
            Debug.Log("thread ejecutado correctamente");
            threadTerminado = true;
        }
        catch (ThreadAbortException ex)
        {
            Debug.Log("Thread es abortado " + ex.ExceptionState);
        }

    }
   
    public void posicionInicial()
    {
        if (numHilos > 0) //detecta si se presiona el boton más de una vez
        {
            //vuelve a la posicion inicial
            player.CambiarDireccion(Vector2.down);
            player.pasos = player.dist;
            player.transform.position = posicionIncial;
            
        }
    }

    public void Play()
    {
        
        //Debug.Log(highlightMap.);
       /* if (highlightMap.GetTile(currentCell) == grass)
        {
            Debug.Log("estoy en el pasto");
        }
        else if (highlightMap.GetTile(currentCell) == sand)
        {
            Debug.Log("estoy en la tierra");
        }*/
        //Debug.Log(highlightMap.tag);
        //Debug.Log("current cell " + currentCell);
        items = EditorScript.MyInstance.MyItems;
        acts = EditorScript.MyInstance.acts;
        //Debug

        //ui.setbotonplayEnable(false);

        if (threadTerminado == true)
        {
            posicionInicial();

            ListaDeHilos.Add(_t1);
            ListaDeHilos[numHilos] = new Thread(Ejecutar);
            threadTerminado = false;
            ListaDeHilos[numHilos].Start();
            numHilos++;
        }
        else
        {
            
            Debug.Log("Aun no termina el thread anterior");
        }
        
    }

   

    public void UseItem()
    {

        foreach (Item item in items)
        {
            Debug.Log("in");
            if (item is CmdMover)
            {

                CmdMover mov = (CmdMover)item;

                if (mov.moveType.ToString() == "MoverAdelante")
                {
                   // Debug.Log("hola cmdmover " + item.MyTitle);
                        Player.MyInstance.moverAdelante();
                    
                    
                }

            }

        }
        
    }
}
