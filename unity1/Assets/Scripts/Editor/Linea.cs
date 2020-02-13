using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Linea", menuName = "Items/Linea", order = 1)]
public class Linea : Item, IUseable
{

    [SerializeField]
    private int acts;
        /// <summary>
    /// A reference to a bag prefab, so that we can instanitate a bag in the game
    /// </summary>
    [SerializeField]
    private GameObject lineaPrefab;

    // Start is called before the first frame update
    public EditorScript MyEditorScript { get; set; }

    public void Initialize()
    {
        this.acts = 1;
    }

    public void Use()
    {

    }
}
