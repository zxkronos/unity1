using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// A reference t o the useable on the actionbutton
    /// </summary>
    public IUseable MyUseable { get; set; }

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

    [SerializeField]
    private Image icon;

    // Use this for initialization
    void Start ()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// This is executed the the button is clicked
    /// </summary>
    public void OnClick()
    {
       /* if (MyUseable != null)
        {
            MyUseable.Use();
        }*/
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
