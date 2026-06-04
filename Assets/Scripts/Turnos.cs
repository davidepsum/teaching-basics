using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
public class Turnos : MonoBehaviour
{
    public bool turnoJugador = true;
    public Manager manager;

    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Manager");
        if (obj != null)
        {
            manager = obj.GetComponent<Manager>();
        }
    }

    public void SiguienteTurno()
    {
        turnoJugador = !turnoJugador;

        if (turnoJugador)
        {
            TurnoJugador();
        }
        else
        {
            TurnoEnemigo();
        }
    }

    private void TurnoJugador()
    {
        Cartas[] cartas = FindObjectsOfType<Cartas>();

        for (int i = 0; i < cartas.Length; i++)
        {
            if (cartas[i].enMesa == true)
            {
                cartas[i].activarEfecto();
            }
        }

        AplicarDañoPorTurno();
    }

    private void TurnoEnemigo()
    {
        Cartas[] cartas = gameObject.FindObjectsByType<Cartas>();

        for (int i = 0; i < cartas.Length; i++)
        {
            if (cartas[i].enMesa == true)
            {
                AtacarJugador(cartas[i]);
            }
        }

        AplicarDañoPorTurno();
    }

    private void AtacarJugador(Cartas carta)
    {
        manager.vida = manager.vida - carta.ataque;

        if (manager.vida < 0)
        {
            manager.vida = 0;
        }
    }

    private void AplicarDañoPorTurno()
    {
        Cartas[] cartas = FindObjectsOfType<Cartas>();

        for (int i = 0; i < cartas.Length; i++)
        {
            if (cartas[i].tipo == TipoCarta.Calentamiento)
            {
                cartas[i].recibirDaño(1);
            }
        }
    }
}
*/
