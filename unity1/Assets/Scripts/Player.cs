using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
            Debug.Log("inmov");
            boton._t1Paused = true;
            Debug.Log("inmov2");
        }
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
