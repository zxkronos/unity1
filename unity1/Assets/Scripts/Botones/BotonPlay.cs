using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Jobs;
using Unity.Collections;

public class BotonPlay : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    


    public void Play()
    {
        Debug.Log("in");
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);
        MyJob jobData = new MyJob();
        jobData.a = 10;
        jobData.b = 10;
        jobData.result = result;
        Debug.Log(result);
        // Schedule the job
        JobHandle handle = jobData.Schedule();

        // Wait for the job to complete
        handle.Complete();
        // All copies of the NativeArray point to the same memory, you can access the result in "your" copy of the NativeArray
        float aPlusB = result[0];

        Debug.Log(aPlusB);
        // Free the memory allocated by the result array
        result.Dispose();

        items = EditorScript.MyInstance.MyItems;
         UseItem();

    }

    public struct MyJob : IJob
    {
        public float a;
        public float b;
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = a + b;
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
                    Debug.Log("hola cmdmover " + item.MyTitle);
                    
                    
                        Player.MyInstance.moverAdelante();
                    
                    
                }

            }

        }
        
    }
}
