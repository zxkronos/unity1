using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private Text stackSize;

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
       // MyAct = new ActScript();
        if (MyAct == null)
        {
            MyAct = FindObjectOfType<ActScript>(); //primer Act por defecto
        }
        EditorScript.MyInstance.acts.Add(MyAct);
        MyIndex = 1;

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


    public void AddActClickDerecho(IUseable actUsable)
    {
        if (actUsable is CmdMover)
        {
            CmdMover mov = (CmdMover)actUsable;
            //Debug.Log(mov.MyTitle);
            MiUsable = actUsable;
            mov.stackSize = 1;
            InputWinObj = Instantiate(EditorScript.MyInstance.inputWinGO, transform).GetComponent<InputWindow>(); //se agrega linea como gameobject
            InputWinObj.transform.parent = EditorScript.MyInstance.linea.transform;
            EditorScript.MyInstance.AgregarLinea();
            
        }

        SetUseable(actUsable);

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
                    //Debug.Log(mov.MyTitle);
                    //IClickable clickable= (IClickable)HandScript.MyInstance.MyMoveable;
                    mov.stackSize = 1;
                    //Debug.Log("in?");
                    InputWinObj = Instantiate(EditorScript.MyInstance.inputWinGO, transform).GetComponent<InputWindow>(); //se agrega linea como gameobject
                    InputWinObj.transform.parent = EditorScript.MyInstance.linea.transform;
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
