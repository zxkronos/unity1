﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    public Vector2 direction;
    protected Animator animator;

    public Tilemap highlightMap;
    [SerializeField]
    public Tile grass;
    [SerializeField]
    public Tile sand;
    public Vector3Int posPlayer;
    public Vector3Int posVista; //lo que ve el personaje frente
    public Vector3 direVista;
    public GameObject Vista;

    public int dist = 33; //distancia a recorrer
    public int pasos; //pasos recorridos hasta llegar a dist
    protected bool mover = false; //boton de mover presionado?
    protected bool caminarAdelante = false; //camina hacia adelante?
    public bool choqueArbol; //choca con un arbol?
    protected bool choqueAgua = false; // Choca con el agua?
    protected Vector2 directionSpell;
    public bool cambiarDir;
    [SerializeField]
    float bulletSpeed = 500f;

   // public Transform barrel;
   // public Rigidbody2D bullet;
    protected float velX = 5f;
    protected float velY = 5f;
    private Rigidbody2D myRigidbody;
    public BotonPlay botonplay;
    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }
    // protected bool isAttacking = false;

    //  protected Coroutine attackRoutine;
    // Start is called before the first frame update
    protected virtual void Start()
    {
       
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pasos = dist; //pasos parte igual a la distancia para permanecer quieto cuando se presiona el boton de mover pasos cambia a 0
        direction = Vector2.down; //parte en direccion hacia abajo
        choqueArbol = false;
        botonplay = BotonPlay.PlayInstance;
        //directionSpell = Vector2.down;
        posPlayer = highlightMap.WorldToCell(Player.MyInstance.transform.position);
        posVista = highlightMap.WorldToCell(Vista.transform.position);
        direVista = Vista.transform.position;
    }

    
    // Update is called once per frame
    protected virtual void Update()
    {


    }

    private void FixedUpdate()
    {
       // Debug.Log(choqueArbol);
        //Se mueve una cantidad de pasos
        if (pasos < dist)
        {
            
            if (mover) // si mover es verdadero
            {
                //StopAttack();
                if (caminarAdelante)
                {
                    myRigidbody.velocity = direction.normalized * speed;
                    //insa++;

                }
                else if (!caminarAdelante)
                {
                    myRigidbody.velocity = -direction.normalized * speed;
                    //Debug.Log("hellow ");
                }

               // Debug.Log(choqueArbol);
                if (choqueArbol)
                {
                    caminarAdelante = false;
                    pasos = dist - pasos-2;
                    choqueArbol = false;
                    //Debug.Log("hola en if choque");
                }else if (choqueAgua)
                {
                    caminarAdelante = false;
                    pasos = dist - pasos - 2;
                    choqueAgua = false;
                }
                //transform.Translate(direction * speed * Time.deltaTime);
                pasos++;
                //Debug.Log(pasos);
                if (pasos == dist)
                {
                    mover = false;
                    botonplay._t1Paused = false;
                }
            }
            else
            {
                posPlayer = highlightMap.WorldToCell(Player.MyInstance.transform.position);
                posVista = highlightMap.WorldToCell(Vista.transform.position);
                direVista = Vista.transform.position;
                pasos = dist; //para que se detenga
                botonplay._t1Paused = false;
                caminarAdelante = true;
                //Debug.Log("in");
            }
            if (direction.x != 0 || direction.y != 0) // si una de las dos direcciones no es 0 significa que se está moviendo
            {
                AnimateMove();
            }
            else
            {
                Debug.Log("inidle");
            }
        }
        else if (cambiarDir)
        {
            posPlayer = highlightMap.WorldToCell(Player.MyInstance.transform.position);
            posVista = highlightMap.WorldToCell(Vista.transform.position);
            //direVista = Vista.transform.position;
            //Player.MyInstance.transform.position = new Vector3(posPlayer.x+1, posPlayer.y, posPlayer.z);
            Vista.transform.localPosition = direVista;
            CambiarDireccion(direction);
            cambiarDir = false;
        }
        else //este else funciona al terminar de caminar
        {
            direVista = Vista.transform.position;
            myRigidbody.velocity = Vector2.zero;
            ActivateLayer("IdleLayer");
        }

        /*   else if (isAttacking)
           {
               ActivateLayer("AttackLayer");
               //Debug.Log("in");
           }
           else
           {

           }*/

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Root") //If we hit an obstacle
        {
            Debug.Log("hola root char");
            choqueArbol = true;
            // pasos = dist - pasos;
            //caminarAdelante = false;
        }
        if (collision.tag == "Water") //If we hit an obstacle
        {
            Debug.Log("No puedo pasar por el agua!");
            choqueAgua = true;
            // pasos = dist - pasos;
            //caminarAdelante = false;
        }
    }

        public void HandleLayers()
    {

    }

    public void Move()
    {
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        //Debug.Log("Hello: "+direction.x);

        if (direction.x != 0 || direction.y != 0)
        {
            AnimateMove();
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

    }
    public void CambiarDireccion(Vector2 dire)
    {
        direction = dire;
        animator.SetFloat("x", dire.x);
        animator.SetFloat("y", dire.y);
    }
    


    public void AnimateMove()
    {
        ActivateLayer("WalkLayer");
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

  /*  public void StopAttack()
    {
        if(attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            animator.SetBool("attack", isAttacking);
        }
        
    }*/

  /*  public void Fire()
    {
        Rigidbody2D firedBullet;
        if (direction == Vector2.down)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = -90;
            firedBullet = Instantiate(bullet, barrel.position, Quaternion.Euler(rotationVector)) as Rigidbody2D;
        }
        else if (direction == Vector2.right)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 0;
            firedBullet = Instantiate(bullet, barrel.position, Quaternion.Euler(rotationVector)) as Rigidbody2D;
        }
        else if (direction == Vector2.left)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 180;
            firedBullet = Instantiate(bullet, barrel.position, Quaternion.Euler(rotationVector)) as Rigidbody2D;
        }
        else if (direction == Vector2.up)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = 90;
            firedBullet = Instantiate(bullet, barrel.position, Quaternion.Euler(rotationVector)) as Rigidbody2D;
        }
        else
        {
            firedBullet = Instantiate(bullet, barrel.position, Quaternion.identity) as Rigidbody2D;
        }
        //Instantiate(spellPrefab[0], transform.position, Quaternion.identity);
        //firedBullet.GetComponent<rigidbody2D> ();
        firedBullet.velocity = new Vector2(direction.x * velX, direction.y * velY);
        //firedBullet.AddForce(barrel.up * bulletSpeed);
    }*/
}
