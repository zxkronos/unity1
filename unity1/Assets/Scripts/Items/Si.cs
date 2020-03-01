using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SiType { Si, Ojo, TileGrass, TileSand, TileTree, TileWater }

[CreateAssetMenu(fileName = "Si", menuName = "Items/Si", order = 2)]
public class Si : Item, IUseable
{
    [SerializeField]
    public SiType siType;

    public void Use()
    {

    }

    public override string GetDescription()
    {

        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Condición Si!</color>");

    }

}
