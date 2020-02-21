using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Jobs;
using Unity.Collections;
using System.Threading;

public class BotonPlay : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    
    public Thread _t1;
    public Thread _t2;
    public bool _t1Paused = false;
    public bool _t2Paused = false;

    public float timeWaiting = 5.0f;

    private static BotonPlay instance;

    public Player player;

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
    }

    public void _func1()
    {
        foreach (Item item in items)
        {
            Debug.Log("inforach");
            if (item is CmdMover)
            {

                CmdMover mov = (CmdMover)item;

                if (mov.moveType.ToString() == "MoverAdelante")
                {
                    Debug.Log("hola cmdmover " + item.MyTitle);

                    _t1Paused = false;
                    player.moverAdelante();
                    Debug.Log("oli " + item.MyTitle);

                    while (_t1Paused)
                    {
                        //Debug.Log("ca");

                    }

                }

            }

        }


        
        
    }

    void Start()
    {
        _t1 = new Thread(_func1);
       
    }

    public void Play()
    {
       

        items = EditorScript.MyInstance.MyItems;

        if (!_t1.IsAlive)
            _t1.Start();
        
            //_t1Paused = !_t1Paused;

        
        // UseItem();
        

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
                    Debug.Log("hola cmdmover " + item.MyTitle);
                    
                    
                        Player.MyInstance.moverAdelante();
                    
                    
                }

            }

        }
        
    }
}
