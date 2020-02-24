using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonStop : MonoBehaviour
{
    private BotonPlay botonplay;
    private Player elPlayer;

    // Start is called before the first frame update
    void Start()
    {
        botonplay = BotonPlay.PlayInstance;
        elPlayer = Player.MyInstance;
    }

    public void BotonStopPlay()
    {
        if (botonplay.threadTerminado == false && botonplay.numHilos > 0)
        {
            botonplay.ListaDeHilos[botonplay.numHilos - 1].Abort();
            elPlayer.pasos = elPlayer.dist;
          //  elPlayer.transform.position = botonplay.posicionIncial;
            botonplay.threadTerminado = true;
        }
    }
}
