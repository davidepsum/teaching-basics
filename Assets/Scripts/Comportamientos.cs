using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Comportamientos : MonoBehaviour
{
    public Manager manager;
    public Cartas[] cartas = new Cartas[8];
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
        contadorMurallas = 0;
    }
    public void AsignarAliado(GameObject carta, int casilla)
    {
        aliados[casilla] = carta.GetComponent<Cartas>();
        AsignarCartas(true, casilla);
    }
    public void AsignarEnemigo(GameObject carta, int casilla)
    {
        enemigos[casilla] = carta.GetComponent<Cartas>();
        AsignarCartas(false, casilla);
    }
    public void AsignarCartas(bool aliado,int casilla)
    {
        if (aliado == true)
        {
            cartas[casilla] = aliados[casilla];
        }
        else
        {
            cartas[casilla+4]= enemigos[casilla];
            casilla += 4;
        }
        cartas[casilla].activarAlColocar();
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
    public void DañarEnArea(bool aliado, int cantidad)
    {
        for (int i = 0; i < cartas.Length; i++)
        {
            if ((cartas[i] != null) && (cartas[i].aliado != aliado))
            {
                cartas[i].defensa -= cantidad;
                cartas[i].ActualizarEstadísticas();
            }
        }
    }
    public void ReforzarAliados(int casilla,bool aliado, int cantidad,TipoCarta tipo)
    {
        for (int i=0; i<cartas.Length; i++)
        {
            if ((cartas[i] != null) && (cartas[i].aliado == aliado) && (tipo != cartas[i].tipo))
            {
                if ((tipo == TipoCarta.Escritura)||(tipo==TipoCarta.Feudalismo))
                {
                    cartas[i].ataque+=cantidad;
                }
                if (tipo == TipoCarta.Feudalismo)
                {
                    cartas[i].defensa -= cantidad;
                }
                cartas[i].defensa += cantidad;
                cartas[i].ActualizarEstadísticas();
            }
        }
    }
    public void EmpezarTurno()
    {
        pasaTurnos.interactable = false;
        do
        {
            turno++;
            if ((turnoaliado == true) && (aliados[turno - 1] != null))
            {
                aliados[turno - 1].activarEfecto();
            }
            if ((turnoaliado == false)&& (enemigos[turno-1] != null))
            {
                enemigos[turno - 1].activarEfecto();
            }
            for (int i = 0; i < aliados.Length; i++)
            {
                if (aliados[i] != null)
                {
                    aliados[i].ActualizarEstadísticas();
                    aliados[i].Morir();
                }
                if (enemigos[i] != null)
                {
                    enemigos[i].ActualizarEstadísticas();
                    enemigos[i].Morir();
                }
            }
        }while (turno < 4);
        turno = 0;
        pasaTurnos.interactable = true;
        turnoaliado = !turnoaliado;
        manager.MostrarCartas();
        for (int i = 0;i < cartas.Length; i++)
        {
            if (cartas[i] != null)
            {
                cartas[i].gameObject.SetActive(true);
            }
        }
    }
    //Muralla
    [HideInInspector]
    public int contadorMurallas = 0;
    public void RestarVida(int cantidad, TipoCarta tipo, bool aliado)
    {
        contadorMurallas++;
        if (aliado == true)
        {
            for (int i = 0; i < manager.aliados.Length; i++)
            {
                if ((tipo == manager.aliados[i].tipo) && (contadorMurallas > 1))
                {
                    manager.aliados[i].defensa -= cantidad;
                    if (manager.aliados[i].defensa <= 0)
                    {
                        contadorMurallas--;
                    }
                    manager.aliados[i].ActualizarEstadísticas();
                    manager.aliados[i].Morir();
                }
            }
        }
        else
        {
            for (int i = 0; i < manager.enemigos.Length; i++)
            {
                if ((tipo == manager.enemigos[i].tipo) && (contadorMurallas > 1))
                {
                    manager.enemigos[i].defensa -= cantidad;
                    if (manager.enemigos[i].defensa <= 0)
                    {
                        contadorMurallas--;
                    }
                    manager.enemigos[i].ActualizarEstadísticas();
                    manager.enemigos[i].Morir();
                }
            }
        }
            
    }
}
