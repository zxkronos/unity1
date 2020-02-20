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

    public void _func1()
    {
        
        while (true)
        {
            

            for (int i = 0; i < timeWaiting; i++)
            {
                Debug.Log("for");
                //_t1Paused = true;
            }
            Thread.Sleep(10000000);
            
            while (_t1Paused)
            {
               // Debug.Log("pausada");
            
            }
        }
    }

    void Start()
    {
        _t1 = new Thread(_func1);
       
    }

    public void Play()
    {
        Debug.Log("estado "+_t1.ThreadState);

        if (!_t1.IsAlive)
            _t1.Start();
        else
            //_t1Paused = !_t1Paused;

        items = EditorScript.MyInstance.MyItems;
         UseItem();
        Debug.Log("ol2");

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
