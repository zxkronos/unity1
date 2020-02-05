using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MoveType { Movimiento, MoverAdelante, GirarDerecha, GirarIzquierda }

[CreateAssetMenu(fileName = "Mover", menuName = "Items/Mover", order = 2)]
public class CmdMover : Item, IUseable
{

    [SerializeField]
    private MoveType moveType;

    [SerializeField]
    private int cantidadPasos;

    public override string GetDescription()
    {
        if(moveType.ToString() == "MoverAdelante") 
        return base.GetDescription() + string.Format("\n<color=#00ff00ff>Mover adelante!</color>");
        else
        {
            return base.GetDescription() + string.Format("\n<color=#00ff00ff>Hola!</color>")+ moveType.ToString();
        }
    }

    public void Use()
    {
        //Player.MyInstance.ExitIndex = 0;
        Player.MyInstance.MoverArriba(Vector2.up) ;

        Debug.Log("in use");
    }
}
