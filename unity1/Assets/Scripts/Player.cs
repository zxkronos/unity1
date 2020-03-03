using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

public class Player : Character
{

    private static Player instance;
    public BotonPlay boton;

    

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }


    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        boton = BotonPlay.PlayInstance;
        
    }

    

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        //health.MicurrentValue = 100;
        //transform.Translate(direction * 5 * Time.deltaTime);
        base.Update();
    }
    
    public void moverAdelante()
    {
        if (pasos == dist)
        {
            //direction = Vector2.up;
            
            mover = true;
            caminarAdelante = true;
            pasos = 0;
            boton._t1Paused = true;
            
        }
    }
    public void moverAtras()
    {
        if (pasos == dist)
        {
            //direction = Vector2.up;
            mover = true;
            caminarAdelante = false;
            pasos = 0;
            boton._t1Paused = true;
        }
    }

    public void GirarDerecha()
    {
        //Vector3Int currentCell = highlightMap.WorldToCell(Player.MyInstance.transform.position);
        //Debug.Log(highlightMap.);
       // transform.position = posPlayer; 
      /*  if (highlightMap.GetTile(currentCell) == grass)
        {
            Debug.Log("estoy en el pasto");
        }
        else if (highlightMap.GetTile(currentCell) == sand)
        {
            Debug.Log("estoy en la tierra");
        }*/
        if (pasos == dist)
        {

            if (direction == Vector2.left)
            { 
                direction = Vector2.up;
                direVista = Vector3.zero;
                cambiarDir = true;
                //  barrel.transform.position = new Vector3(-0.03f - 4.96f, 0.9f - 0.9200001f, 0);
            }
            else if (direction == Vector2.up)
            {
                direction = Vector2.right;
                direVista = Vector3.zero;
                cambiarDir = true;
                // barrel.transform.position = new Vector3(1f - 4.96f, -0.1f - 0.9200002f, 0);
            }
            else if (direction == Vector2.right)
            {
                direction = Vector2.down;
                direVista = Vector3.zero;
                cambiarDir = true;
                // barrel.transform.position = new Vector3(-0.03f - 4.96f, -0.9f - 0.9200001f, 0);
            }
            else if (direction == Vector2.down)
            {
                Debug.Log("y "+direVista.y);
                Debug.Log("x " + direVista.x);
                direction = Vector2.left;
                direVista = Vector3.zero;
                Debug.Log("y " + direVista.y);
                Debug.Log("x " + direVista.x);
                cambiarDir = true;
                //  barrel.transform.position = new Vector3(1f - 6.96f, -0.1f - 0.9200002f, 0);
            }
            //directionSpell = direction;
            
        }
      //  BotonPlay.PlayInstance._t1Paused = false;
    }

    public void GirarIzquierda()
    {
       // boton._t1Paused = true;

        if (pasos == dist)
        {
            if (direction == Vector2.left)
            {
                
                direction = Vector2.down;
                cambiarDir = true;
                //  barrel.transform.position = new Vector3(-0.03f-4.96f, -0.9f-0.9200001f, 0);
            }
            else if (direction == Vector2.down)
            {
                //CambiarDireccion( Vector2.right);
                direction = Vector2.right;
                cambiarDir = true;
                //directionSpell = Vector2.right;
                //  barrel.transform.position = new Vector3(1f-4.96f, -0.1f-0.9200002f, 0);
            }

            else if (direction == Vector2.right)
            {
                //CambiarDireccion(Vector2.up);
                direction = Vector2.up;
                cambiarDir = true;
                //directionSpell = Vector2.up;
                //  barrel.transform.position = new Vector3(-0.03f - 4.96f, 0.9f - 0.9200001f, 0);
            }

            else if (direction == Vector2.up)
            {
                //CambiarDireccion(Vector2.left);
                direction = Vector2.left;
                cambiarDir = true;
                //directionSpell = Vector2.left;
                //  barrel.transform.position = new Vector3(1f - 6.96f, -0.1f - 0.9200002f, 0);
            }

            //directionSpell = direction;

            //direction = Vector2.left;
            //pasos = 0;
        }
        //BotonPlay.PlayInstance._t1Paused = false;
    }

    private void GetInput() {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //directionSpell = Vector2.up;
            if (pasos == dist) {
                //direction = Vector2.up;
                mover = true;
                caminarAdelante = true;
                pasos = 0;
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            if (pasos == dist)
            {
                if (direction == Vector2.left)
                {
                    direction = Vector2.down;
                  //  barrel.transform.position = new Vector3(-0.03f-4.96f, -0.9f-0.9200001f, 0);
                }
                else if (direction == Vector2.down)
                {
                    direction = Vector2.right;
                    //directionSpell = Vector2.right;
                  //  barrel.transform.position = new Vector3(1f-4.96f, -0.1f-0.9200002f, 0);
                }
                    
                else if (direction == Vector2.right)
                {
                    direction = Vector2.up;
                    //directionSpell = Vector2.up;
                  //  barrel.transform.position = new Vector3(-0.03f - 4.96f, 0.9f - 0.9200001f, 0);
                }
                    
                else if (direction == Vector2.up)
                {
                    direction = Vector2.left;
                    //directionSpell = Vector2.left;
                  //  barrel.transform.position = new Vector3(1f - 6.96f, -0.1f - 0.9200002f, 0);
                }

                directionSpell = direction;

                //direction = Vector2.left;
                pasos = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
            if (pasos == dist)
            {

                if (direction == Vector2.left)
                {
                    direction = Vector2.up;
                  //  barrel.transform.position = new Vector3(-0.03f - 4.96f, 0.9f - 0.9200001f, 0);
                }
                else if (direction == Vector2.up)
                {
                    direction = Vector2.right;
                   // barrel.transform.position = new Vector3(1f - 4.96f, -0.1f - 0.9200002f, 0);
                }
                else if (direction == Vector2.right)
                {
                    direction = Vector2.down;
                   // barrel.transform.position = new Vector3(-0.03f - 4.96f, -0.9f - 0.9200001f, 0);
                } 
                else if (direction == Vector2.down)
                {
                    direction = Vector2.left;
                  //  barrel.transform.position = new Vector3(1f - 6.96f, -0.1f - 0.9200002f, 0);
                }
                //directionSpell = direction;
                pasos = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            if (pasos == dist)
            {
                //direction = Vector2.up;
                mover = true;
                caminarAdelante = false;
                pasos = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
          /*  if (!isAttacking && !mover)
            {
                attackRoutine = StartCoroutine(Attack());
            }*/
        }

    }

    public void MoverArriba( Vector2 dir)
    {
        // exit
        Debug.Log(direction);
        Direction = dir;
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(Direction * speed * Time.deltaTime);
        }
        Debug.Log("mover arriba");
    }
}
