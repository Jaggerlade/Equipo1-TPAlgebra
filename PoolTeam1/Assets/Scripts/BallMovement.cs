﻿using UnityEngine;


public class BallMovement : MonoBehaviour
{
    public Balls.Tipo tipo;
    public Balls.NombreColic nombre;
    public Balls.NombreColic ultimaColic;
    public float mass = 3.0f;
    public float coefOfFriction = 0.15f;
    public float gravity = 9.8f;
    public float radius = 0.5f;

    public float force;
    float aceleration;
    float time;
    float speed;

    public Vector2 launchDirection;

    void Start()
    {
        aceleration = 0.0f;
        force = 0.0f;
        time = 0.0f;
        launchDirection = Vector2.zero;
    }

    void FixedUpdate()
    {
        switch (tipo)
        {
            case Balls.Tipo.blanca:
                PushBall();
                ApplyFriction();
                if (Input.GetKeyDown(KeyCode.P))
                {
                    transform.position = Vector3.zero;
                }
                break;
            case Balls.Tipo.lisa:
                ApplyFriction();
                break;
            case Balls.Tipo.rayada:
                ApplyFriction();
                break;
            case Balls.Tipo.negra:
                ApplyFriction();
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void PushBall()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (time <= 4)
            {
                time += Time.deltaTime * 3;
                launchDirection = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            aceleration = time;
            force = mass * aceleration;
            time = 0.0f;
        }
    }

    float CalculateFriction()
    {
        float Fr = 0.0f;
        float NormalF = mass * gravity;

        Fr = (coefOfFriction * NormalF) * Time.deltaTime;

        return Fr;
    }

    void ApplyFriction()
    {
        if (force >= 0.0f)
        {
            transform.Translate(launchDirection * force * Time.deltaTime);
            force -= CalculateFriction();
        }
        else
        {
            ultimaColic = nombre; // para restablecer las ultimas coliciones de las pelotas

            if (tipo != Balls.Tipo.blanca) //para limpiar la direccion de las pelotas que no son blancas
            {
                launchDirection = Vector2.zero;
            }
        }
    }
}