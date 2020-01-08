using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private float currentFill;
    private Image content;
    public float MyMaxValue { get; set; }

    private float currentValue;

    public float MicurrentValue{
        get{
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            currentValue = value;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
        content.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
