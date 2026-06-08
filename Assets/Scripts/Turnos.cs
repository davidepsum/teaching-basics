using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turnos : MonoBehaviour
{
    public Manager manager;
    public Cartas[] cartas = new Cartas[4];
    public Cartas[] enemigos = new Cartas[4];

    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Manager");
        if (obj != null)
        {
            manager = obj.GetComponent<Manager>();
        }
    }
    public void AsignarCarta(GameObject carta, int casilla)
    {
        cartas[casilla] = carta.GetComponent<Cartas>();
    }
    public void AsignarEnemigo(GameObject carta, int casilla)
    {
        enemigos[casilla] = carta.GetComponent<Cartas>();
    }

    public void RecibirDaño(int casilla, int daño,bool aliado)
    {
        if (aliado == true)
        {
            enemigos[casilla].defensa -= daño;
        }
        if (cartas[casilla] != null)
        {
            manager.vida-=daño;
        }
    }
    public void TurnoEnemigo()
    {
        for (int i = 0; i < enemigos.Length; i++)
        {
            if ((enemigos[i] != null) && (enemigos[i].enMesa))
            {
                Atacar(i);
            }
        }
    }
    void Atacar(int casilla)
    {
        if (cartas[casilla] != null && cartas[casilla].enMesa)
        {
            cartas[casilla].recibirDaño(enemigos[casilla].ataque);
        }
        else
        {
            manager.vida -= enemigos[casilla].ataque;
            if (manager.vida < 0)
            {
                manager.vida = 0;
            }
        }
    }
}

    /*
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
