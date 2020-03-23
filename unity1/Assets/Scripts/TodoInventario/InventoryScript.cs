using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;

    private static InventoryScript instance;

    public Bag bagSi;
    public Bag bagMov;
    public Color colorItems;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    public BagButton[] bagButtons;
    //private BagButton BagButtonSi;
    private Image BagIconSi;

    //For debugging
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get { return MyBags.Count < 5; }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }

            return count;
        }
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }

            return count;
        }
    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;

            if (value != null)
            {
                fromSlot.MyCover.enabled = true;
            }
        }
    }

    public List<Bag> MyBags
    {
        get
        {
            return bags;
        }
    }

    private void Awake()
    {
        bagMov = (Bag)Instantiate(items[8]);
        bagMov.Initialize(4);

        bagMov.Use();


        AgregarItem(bagMov, (CmdMover)Instantiate(items[14]));
        AgregarItem(bagMov, (CmdMover)Instantiate(items[15]));
        AgregarItem(bagMov, (CmdMover)Instantiate(items[16]));
        AgregarItem(bagMov, (CmdMover)Instantiate(items[12]));
        

        //códigos de Si
        bagSi = (Bag)Instantiate(items[8]);
        bagSi.Initialize(12);

        bagSi.Use();
        //BagIconSi = bagButtons[0].GetComponent<Image>();
        //BagIconSi.color = new Vector4(0.4f, 0.4f, 0.4f, 1f);

       // AgregarItem(bagSi, (Si)Instantiate(items[17]));
       // AgregarItem(bagSi, (Si)Instantiate(items[18]));
        AgregarItem(bagSi, (Si)Instantiate(items[19]));//Tiles
        AgregarItem(bagSi, (Si)Instantiate(items[20]));//Tiles
        AgregarItem(bagSi, (Si)Instantiate(items[21]));//Tiles
        AgregarItem(bagSi, (Si)Instantiate(items[22]));//Tiles
        bagSi.MyBagScript.MySlots[11].AddItem((Si)Instantiate(items[17])); //item 17 si
        bagSi.MyBagScript.MySlots[9].AddItem((Si)Instantiate(items[23])); //item 23 sino
        bagSi.MyBagScript.MySlots[10].AddItem((Si)Instantiate(items[24])); //item 24 fin si
        bagSi.MyBagScript.MySlots[8].AddItem((Si)Instantiate(items[25])); //item 25 fin sino
        bagSi.MyBagScript.MySlots[7].AddItem((Si)Instantiate(items[18])); //item 18 ojo

        //bloqueos de botones
        siBloqueoInicial();

        OpenClose();
    }
    
    public void siBloqueoInicial()
    {
        bloqBoton(bagSi, 0, true);
        bloqBoton(bagSi, 1, true);
        bloqBoton(bagSi, 2, true);
        bloqBoton(bagSi, 3, true);
        bloqBoton(bagSi, 4, true);
        bloqBoton(bagSi, 7, true);
        bloqBoton(bagSi, 9, true);
        bloqBoton(bagSi, 10, true);
        bloqBoton(bagSi, 8, true);
        bloqBoton(bagSi, 11, false);
  
    }

    public void SiBloquearBotones()
    {
        //SiBloqBotones = true;
        bloqBoton(bagSi, 0, true);
        bloqBoton(bagSi, 1, true);
        bloqBoton(bagSi, 2, true);
        bloqBoton(bagSi, 3, true);
        bloqBoton(bagSi, 4, true);
        bloqBoton(bagSi, 11, true);
        bloqBoton(bagSi, 8, true);
        bloqBoton(bagSi, 9, true);
        bloqBoton(bagSi, 7, false);
       
        //placeinspecific:
        //bags[bagIndex].MyBagScript.MySlots[slotIndex].AddItem(item);
        //bagSi.MyBagScript.MySlots[0].MyCover.enabled = true;
    }
    public void OjoBloquearBotones()
    {
        bloqBoton(bagSi, 0, false);
        bloqBoton(bagSi, 1, false);
        bloqBoton(bagSi, 2, false);
        bloqBoton(bagSi, 3, false);
        bloqBoton(bagSi, 4, false);
        bloqBoton(bagSi, 7, true);
       
    }
    public void tilesDesbloqueoBotones()
    {
        

        siBloqueoInicial();
        bloqBoton(bagSi, 10, false);
        bloqBoton(bagSi, 11, true);

    }

    public void finSiDesbloq()
    {
        bloqBoton(bagSi, 9, false);
        bloqBoton(bagSi, 8, false);
        bloqBoton(bagSi, 10, true);
        bloqBoton(bagSi, 11, false);
        
    }
    public void finSinoDesbloq()
    {
        siBloqueoInicial();

    }

    public void SinoDesbloq()
    {
        bloqBoton(bagSi, 11, true);
        bloqBoton(bagSi, 8, false);
       
    }

    public void bloqBoton(Bag bolsa, int numSlot, bool bloq)
    {
        bolsa.MyBagScript.MySlots[numSlot].bloqueado = bloq;
        bolsa.MyBagScript.MySlots[numSlot].MyCover.enabled = bloq;
    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.J))
        {
            Bag bag = (Bag)Instantiate(items[8]);
            bag.Initialize(40);
            AddItem(bag);
        }
        if (Input.GetKeyDown(KeyCode.K))//Debugging for adding a bag to the inventory
        {
            Bag bag = (Bag)Instantiate(items[8]);
            bag.Initialize(20);
            AddItem(bag);

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            HealthPotion potion = (HealthPotion)Instantiate(items[9]);
            AddItem(potion);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GoldNugget nugget = (GoldNugget)Instantiate(items[11]);
            AddItem(nugget);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {

            AddItem((Armor)Instantiate(items[0]));
            AddItem((Armor)Instantiate(items[1]));
            AddItem((Armor)Instantiate(items[2]));
            AddItem((Armor)Instantiate(items[3]));
            AddItem((Armor)Instantiate(items[4]));
            AddItem((Armor)Instantiate(items[5]));
            AddItem((Armor)Instantiate(items[6]));
            AddItem((Armor)Instantiate(items[7]));
            AddItem((Armor)Instantiate(items[10]));

        }

    }

    
    /// <summary>
    /// Equips a bag to the inventory
    /// </summary>
    /// <param name="bag"></param>
    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                MyBags.Add(bag);
                bag.MyBagButton = bagButton;
                bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
                break;
            }
        }
    }

    public void AddBag(Bag bag, BagButton bagButton)
    {
        MyBags.Add(bag);
        bagButton.MyBag = bag;
        bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);
    }

    public void AddBag(Bag bag, int bagIndex)
    {
        bag.SetupScript();
        MyBags.Add(bag);
        bag.MyBagScript.MyBagIndex = bagIndex;
        bag.MyBagButton = bagButtons[bagIndex];
        bagButtons[bagIndex].MyBag = bag;
    }

    /// <summary>
    /// Removes the bag from the inventory
    /// </summary>
    /// <param name="bag"></param>
    public void RemoveBag(Bag bag)
    {
        MyBags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCount = (MyTotalSlotCount - oldBag.MySlotCount) + newBag.MySlotCount;

        if (newSlotCount - MyFullSlotCount >= 0)
        {
            //Do Swap
            List<Item> bagItems = oldBag.MyBagScript.GetItems();

            RemoveBag(oldBag);

            newBag.MyBagButton = oldBag.MyBagButton;

            newBag.Use();

            foreach (Item item in bagItems)
            {
                if (item != newBag) //No duplicates
                {
                    AddItem(item);
                }
            }

            AddItem(oldBag);

            HandScript.MyInstance.Drop();

            MyInstance.fromSlot = null;

        }
    }

    /// <summary>
    /// Adds an item to the inventory
    /// </summary>
    /// <param name="item">Item to add</param>
    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }

       return PlaceInEmpty(item);
    }

    /// <summary>
    /// Places an item on an empty slot in the game
    /// </summary>
    /// <param name="item">Item we are trying to add</param>
    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in MyBags)//Checks all bags
        {
            if (bag.MyBagScript.AddItem(item)) //Tries to add the item
            {
                OnItemCountChanged(item);
                return true; //It was possible to add the item
            }
        }

        return false;
    }

    private void AgregarItem(Bag bag, Item item) //agrega item en el lugar en la bolsa correspondiente
    {
        bag.MyBagScript.AddItem(item); //Tries to add the item
            
    }

    /// <summary>
    /// Tries to stack an item on anothe
    /// </summary>
    /// <param name="item">Item we try to stack</param>
    /// <returns></returns>
    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in MyBags)//Checks all bags
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots) //Checks all the slots on the current bag
            {
                if (slots.StackItem(item)) //Tries to stack the item
                {
                    OnItemCountChanged(item);
                    return true; //It was possible to stack the item
                }
            }
        }

        return false; //It wasn't possible to stack the item
    }

    public void PlaceInSpecific(Item item, int slotIndex, int bagIndex)
    {
        bags[bagIndex].MyBagScript.MySlots[slotIndex].AddItem(item);

    }

    /// <summary>
    /// Opens and closes all bags
    /// </summary>
    public void OpenClose()
    {
        //Checks if any bags are closed
        bool closedBag = MyBags.Find(x => !x.MyBagScript.IsOpen);

        //If closed bag == true, then open all closed bags
        //If closed bag == false, then close all open bags

        foreach (Bag bag in MyBags)
        {
            if (bag.MyBagScript.IsOpen != closedBag)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }

    public void Close()
    {
        foreach (Bag bag in MyBags)
        {
            if (bag.MyBagScript.IsOpen)
            {
                bag.MyBagScript.OpenClose();
            }
        }
    }
    public List<SlotScript> GetAllItems()
    {
        List<SlotScript> slots = new List<SlotScript>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty)
                {
                    slots.Add(slot);
                }
            }
        }

        return slots;
    }



    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }

        return useables;
    }

    public IUseable GetUseable(string type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    return (slot.MyItem as IUseable);
                }
            }
        }

        return null;
    }

    public int GetItemCount(string type)
    {
        int itemCount = 0;

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    itemCount += slot.MyItems.Count;
                }
            }
        }

        return itemCount;

    }

    public Stack<Item> GetItems(string type, int count)
    {
        Stack<Item> items = new Stack<Item>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    foreach (Item item in slot.MyItems)
                    {
                        items.Push(item);

                        if (items.Count == count)
                        {
                            return items;
                        }
                    }
                }
            }
        }

        return items;

    }

    public void RemoveItem(Item item)
    {
        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == item.MyTitle)
                {
                    slot.RemoveItem(item);
                    break;
                }
            }
        }
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
