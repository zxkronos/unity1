using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class ActScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// A reference t o the useable on the actionbutton
    /// </summary>
    public IUseable MyUseable { get; set; }
    public IUseable MiUsable { get; set; }
    
    
    public InputWindow InputWinObj;
    public int MyIndex { get; set; }

    public static ActScript MyAct { get; set; }

    public static ActScript act;
    public Item item;
    public Item itemAnterior;
    public CmdMover mov;

    [SerializeField]
    private Text stackSize;

    public int miStack;

    private Stack<IUseable> useables = new Stack<IUseable>();

    private int count;

    /// <summary>
    /// A reference to the actual button that this button uses
    /// </summary>
    public Button MyButton { get; private set; }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return count;
        }
    }


    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public Stack<IUseable> MyUseables
    {
        get
        {
            return useables;
        }

        set
        {
            if (value.Count > 0)
            {
                MyUseable = value.Peek();
            }
            else
            {
                MyUseable = null;
            }
            
            useables = value;
        }
    }

    

    private void Awake()
    {
       

    }

   /* public static ActScript MyInstanceAct
    {
        get
        {
            if (act == null)
            {
                act = FindObjectOfType<EditorScript>();
            }

            return act;
        }
    }
    */
    [SerializeField]
    private Image icon;

    // Use this for initialization
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);

        // MyAct = new ActScript();
        

    }

    // Update is called once per frame
    

    /// <summary>
    /// This is executed the the button is clicked
    /// </summary>
    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            else if (MyUseables != null && MyUseables.Count > 0)
            {
                MyUseables.Peek().Use();
            }
        }

    }


    

    /// <summary>
    /// Checks if someone clicked on the actionbutton
    /// </summary>
    /// <param name="eventData"></param>
    ///
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //si handscript no es nulo tiene un item y es usable
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                //items.Add((Item)HandScript.MyInstance.MyMoveable);
                if (HandScript.MyInstance.MyMoveable is CmdMover)
                {
                    CmdMover mov = (CmdMover)HandScript.MyInstance.MyMoveable;
                    item = mov;
                    MiUsable = (IUseable)HandScript.MyInstance.MyMoveable;
                    //Debug.Log(mov.MyTitle);
                    //IClickable clickable= (IClickable)HandScript.MyInstance.MyMoveable;
                    // mov.stackSize = 1;
                    miStack = 1;
                    //Debug.Log("in?");
                    InputWinObj = Instantiate(EditorScript.MyInstance.inputWinGO, transform).GetComponent<InputWindow>(); //se agrega linea como gameobject
                    InputWinObj.transform.SetParent(EditorScript.MyInstance.linea.transform, false);
                    InputWinObj.MyIndex = EditorScript.MyInstance.linea.MyIndex;
                    
                    //inputWinGO.text = mov.stackSize.ToString();
                    // clickable.MyStackText.text = mov.stackSize.ToString();
                    //clickable.MyStackText.enabled = true;
                    //clickable.MyIcon.enabled = true;

                    EditorScript.MyInstance.AgregarLinea();

                /*    if (mov.MyTitle.Equals("ComandosMov"))
                    {
                        EditorScript.MyInstance.AgregarLinea();
                    }
                    else if (mov.MyTitle.Equals("Mover"))
                    {
                        EditorScript.MyInstance.AgregarAct();
                    }*/
                }

                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
                
                    

            }
            else if (HandScript.MyInstance.MyMoveable == null)
            {
                //HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
            }
        }
    }

    public void AddActClickDerecho(IUseable actUsable)
    {
        if (actUsable is CmdMover)
        {
            CmdMover movAnterior=null;

            if (EditorScript.MyInstance.MyItems.Count > 0)
            {
                //Debug.Log("hola");
                movAnterior = (CmdMover)EditorScript.MyInstance.MyItems[EditorScript.MyInstance.MyItems.Count-1];
            }
            
            mov = (CmdMover)actUsable; // cuando se genera el mov actual mov va recoger el ultimo item de la lista
                                        //el cual es el que se agrego anteriormente porque este serta el ultimo
           // Debug.Log("mov " + mov);
            //Debug.Log("movAnt " + movAnterior);
            if (movAnterior != null)
            {
               // Debug.Log("mov ant "+movAnterior.moveType);
               // Debug.Log("mov "+mov.moveType);
                if (movAnterior.moveType == mov.moveType)
                {
                    Debug.Log("mi index "+MyIndex);
                    int x = 0;

                    if (Int32.TryParse(EditorScript.MyInstance.inputs[EditorScript.MyInstance.MyItems.Count - 1].text, out x))
                    {
                        x = x + 1;
                        EditorScript.MyInstance.inputs[EditorScript.MyInstance.MyItems.Count - 1].text = x.ToString();
                        EditorScript.MyInstance.acts[EditorScript.MyInstance.MyItems.Count - 1].miStack = x;
                    }
                }
                else
                {
                    addActMov(actUsable);
                }
            }
            else
            {
                addActMov(actUsable);
            }

        }
        else if (actUsable is Si)
        {
            
            MiUsable = actUsable;
            
            EditorScript.MyInstance.AgregarAct();

            SetUseable(actUsable);
        }

        

    }

    private void addActMov(IUseable actUsable)
    {
        item = mov;
        //Debug.Log(mov.MyTitle);
        MiUsable = actUsable;
        //mov.stackSize = 1;
        miStack = 1;
        GameObject go = Instantiate(EditorScript.MyInstance.inputWinGO, transform) as GameObject; //se agrega linea como gameobject
        InputWinObj = go.GetComponent<InputWindow>();
        EditorScript.MyInstance.inputs.Add(InputWinObj.StackField);

        // InputWinObj.transform.parent = EditorScript.MyInstance.linea.transform;
        InputWinObj.transform.SetParent(EditorScript.MyInstance.linea.transform, false);
        InputWinObj.MyIndex = EditorScript.MyInstance.linea.MyIndex;
        InputWinObj.item = item;
        //EditorScript.MyInstance.inputs[0].text = "66";
        //InputWinObj.act = this;
        EditorScript.MyInstance.AgregarLinea();

        SetUseable(actUsable);
    }

    /// <summary>
    /// Sets the useable on an actionbutton
    /// </summary>
    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            MyUseables = InventoryScript.MyInstance.GetUseables(useable);
            if (InventoryScript.MyInstance.FromSlot != null)
            {
                InventoryScript.MyInstance.FromSlot.MyCover.enabled = false;
                InventoryScript.MyInstance.FromSlot.MyIcon.enabled = true;
                InventoryScript.MyInstance.FromSlot = null;
            }
 

        }
        else
        {
            MyUseables.Clear();
            this.MyUseable = useable;
        }

        count = MyUseables.Count;
        UpdateVisual(useable as IMoveable);
        UIManager.MyInstance.RefreshTooltip(MyUseable as IDescribable);
    }

    /// <summary>
    /// Updates the visual representation of the actionbutton
    /// </summary>
    public void UpdateVisual(IMoveable moveable)
    {
        if (HandScript.MyInstance.MyMoveable != null)
        {
            HandScript.MyInstance.Drop();
        }

        MyIcon.sprite = moveable.MyIcon;
        MyIcon.enabled = true;

        if (count > 1)
        {
            //UIManager.MyInstance.UpdateStackSize(this);
        }
      /*  else if (MyUseable is Spell)
        {
            UIManager.MyInstance.ClearStackCount(this);
        }*/
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUseable && MyUseables.Count > 0)
        {
            if (MyUseables.Peek().GetType() == item.GetType())
            {
                MyUseables = InventoryScript.MyInstance.GetUseables(item as IUseable);

                count = MyUseables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IDescribable tmp = null;

        if (MyUseable != null && MyUseable is IDescribable)
        {
            tmp = (IDescribable)MyUseable;
            //UIManager.MyInstance.ShowToolitip(transform.position);
        }
        else if (MyUseables.Count > 0)
        {
            // UIManager.MyInstance.ShowToolitip(transform.position);
        }
        if (tmp != null)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(1, 0), transform.position, tmp);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
