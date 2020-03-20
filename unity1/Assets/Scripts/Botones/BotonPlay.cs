﻿using System.Collections;
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
    public Vector3 posicionIncialVista;
    public GameObject Vista;
    public bool threadTerminado;
    public Tilemap highlightMap;
    [SerializeField]
    public Tile grass;
    [SerializeField]
    public Tile sand;
    public int contSi;

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
        posicionIncialVista = Vista.transform.position;
        threadTerminado = true;
        contSi = 0;
       // botonplay = Transform.GetComponent<Button>(); 
    }
    void Start()
    {
        

    }

    public void Ejecutar()
    {
        
        try
        {

            if (EditorScript.MyInstance.siCompleto == false || EditorScript.MyInstance.sinoCompleto == false)
            {
                bool siFull = true;
                bool sinoFull = true;
                int posicion = 0;
                int contSi = 0; //Si la cuenta llega hasta 3 está completo.
                bool faltaTile = false;

                //se analizan las lineas y bloques para buscar donde está incompleto
                for (int i = 0; i < EditorScript.MyInstance.lineas.Count; i++ )
                {
                    foreach (ActScript acto in EditorScript.MyInstance.lineas[i].actosLinea)
                    {
                        if(acto.item is Si)
                        {
                            
                            Si siaux = (Si)acto.item;
                            //Debug.Log(siaux.siType.ToString());
                            if (siaux.siType.ToString() == "Si")
                            { 
                                siFull = false;
                                posicion = i;
                                contSi++;
                            }
                            else if (siaux.siType.ToString() == "Ojo")
                            {
                                contSi ++;
                                faltaTile = true;
                            }
                            else if (siaux.siType.ToString() == "TileGrass" || siaux.siType.ToString() == "TileSand" || siaux.siType.ToString() == "TileTree" || siaux.siType.ToString() == "TileWater")
                            {
                                faltaTile = false;
                                contSi ++;
                            }
                            else if (siaux.siType.ToString()=="FinSi")
                            {
                                siFull = true;
                                contSi = 0;
                            }
                            else if (siaux.siType.ToString() == "Sino")
                            {
                                sinoFull = false;
                                posicion = i;
                            }
                            else if (siaux.siType.ToString() == "FinSino")
                            {
                                sinoFull = true;
                            }

                            // if ((Si)acto.item.)
                        }
                       /* else if(acto.item is CmdMover){
                            CmdMover mov= (CmdMover)acto.item;
                            Debug.Log(mov.moveType.ToString());
                        }*/
                    }
                }

                if (contSi == 1)
                {
                    Debug.Log("Falta condición en la línea " + (posicion + 1));
                }
                else if (contSi == 2 && faltaTile)
                {
                    Debug.Log("Falta código de terreno en la línea " + (posicion + 1));
                }
                else if (siFull == false)
                {
                    Debug.Log("Falta cerrar el Si de la línea "+(posicion+1));
                }
                else if(sinoFull == false)
                {
                    Debug.Log("Falta cerrar el Sino de la línea " + (posicion + 1));
                }

                threadTerminado = true;
            }
            else
            {
                bool condicionSi = true;

                foreach (ActScript act in acts)
                {

                    item = act.item;
                    //Debug.Log(item is Si);

                    if (item is Si)
                    {
                        //Debug.Log("si");
                        Si si = (Si)item;

                        if (si.siType.ToString() == "Si")
                        {
                            //contSi++;

                        }
                        else if (si.siType.ToString() == "Sino")
                        {

                        }
                        else if (si.siType.ToString() == "Ojo")
                        {

                        }
                        else if (si.siType.ToString() == "TileGrass")
                        {
                            if (player.terrenoDelante == "pasto")
                            {
                                condicionSi = true;
                            }
                            else
                            {
                                condicionSi = false;
                                Debug.Log("El terreno delante no es pasto, es " + player.terrenoDelante);
                            }
                        }
                        else if (si.siType.ToString() == "TileSand")
                        {
                            if (player.terrenoDelante == "tierra")
                            {
                                condicionSi = true;
                            }
                            else
                            {
                                condicionSi = false;
                            }
                        }
                        else if (si.siType.ToString() == "TileTree")
                        {
                            if (player.terrenoDelante == "arbol")
                            {
                                condicionSi = true;
                            }
                            else
                            {
                                condicionSi = false;
                            }
                        }
                        else if (si.siType.ToString() == "TileWater")
                        {
                            if (player.terrenoDelante == "agua")
                            {
                                condicionSi = true;
                            }
                            else
                            {
                                condicionSi = false;
                            }
                        }
                        else if (si.siType.ToString() == "FinSi")
                        {
                            condicionSi = true; //para que siga avanzando las instrucciones despues del finsi
                        }
                    }
                    else if (item is CmdMover && condicionSi)
                    {

                        CmdMover mov = (CmdMover)item;
                        //Debug.Log("stack "+act.miStack);
                        for (int i = 0; i < act.miStack; i++)
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
            }//fin else
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
            Vista.transform.position = posicionIncialVista;
            player.direVista = posicionIncialVista;
        }
    }

    public void Play()
    {

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
