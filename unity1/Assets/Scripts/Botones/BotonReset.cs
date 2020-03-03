using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonReset : MonoBehaviour
{
    private BotonPlay botonplay;
    private Player elplayer; 
    // Start is called before the first frame update
    void Start()
    {
        botonplay = BotonPlay.PlayInstance;
        elplayer = Player.MyInstance;
    }

    // Update is called once per frame
    public void botonReset()
    {
        if (botonplay.numHilos > 0) //detecta si se presiona el boton más de una vez
        {
            //vuelve a la posicion inicial
            elplayer.CambiarDireccion(Vector2.down);
          //  elplayer.pasos = 0;
            elplayer.pasos = elplayer.dist;
            elplayer.transform.position = botonplay.posicionIncial;
            botonplay.Vista.transform.position = botonplay.posicionIncialVista;
            elplayer.direVista = botonplay.posicionIncialVista;
            botonplay.threadTerminado = true;
        }
    }
}
