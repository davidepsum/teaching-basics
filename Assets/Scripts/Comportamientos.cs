using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Comportamientos : MonoBehaviour
{
    public Manager manager;
    public Cartas[] aliados = new Cartas[4];
    public Cartas[] enemigos = new Cartas[4];
    public int turno=0;
    public bool turnoaliado;
    public Button pasaTurnos;
    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Manager");
        if (obj != null)
        {
            manager = obj.GetComponent<Manager>();
        }
        turnoaliado = true;
    }
    public void AsignarAliado(GameObject carta, int casilla)
    {
        aliados[casilla] = carta.GetComponent<Cartas>();
    }
    public void AsignarEnemigo(GameObject carta, int casilla)
    {
        enemigos[casilla] = carta.GetComponent<Cartas>();
    }
    public void RealizarDaño(int daño,int casilla,bool aliado)
    {
        if (aliado == true)
        {
            if (enemigos[casilla] == null)
            {
                manager.vidaEnemigo -= daño;
            }
            else
            {
                enemigos[casilla].defensa -= daño;
            }
        }
        if (aliado == false)
        {
            if (aliados[casilla] == null)
            {
                manager.vidaAliado -= daño;
            }
            else
            {
                aliados[casilla].defensa -= daño;
            }
        }
    }
    public void EmpezarTurno()
    {
        pasaTurnos.interactable = false;
        do
        {
            turno++;
            Debug.Log(turno);
            if ((turnoaliado == true) && (aliados[turno - 1] != null))
            {
                aliados[turno - 1].activarEfecto();
                Debug.Log("Efectuado");
            }
            if ((turnoaliado == false)&& (enemigos[turno-1] != null))
            {
                enemigos[turno - 1].activarEfecto();
                Debug.Log("Efectuado");
            }
            for (int i = 0; i < aliados.Length; i++)
            {
                if (aliados[i] != null)
                {
                    aliados[i].Morir();
                }
                if (enemigos[i] != null)
                {
                    enemigos[i].Morir();
                }
            }
        }while (turno < 4);
        turno = 0;
        pasaTurnos.interactable = true;
        turnoaliado = !turnoaliado;
    }
}
