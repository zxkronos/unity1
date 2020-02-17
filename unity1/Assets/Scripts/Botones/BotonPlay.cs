using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonPlay : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("in");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            items = EditorScript.MyInstance.MyItems;
            UseItem();
        }
    }

    public void Play()
    {
        Debug.Log("in");
        
            items = EditorScript.MyInstance.MyItems;
            UseItem();
        
    }

    public void UseItem()
    {

        foreach (Item item in items)

        if (item is CmdMover)
        {
                Debug.Log("hola cmdmover "+ item.MyTitle);
        }
       

    }
}
