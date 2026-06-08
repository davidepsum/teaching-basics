using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Comportamientos : MonoBehaviour
{
    public int vidaAliado=100;
    public int vidaEnemigo=100;

    public Manager manager;
    public Cartas[] aliados = new Cartas[4];
    public Cartas[] enemigos = new Cartas[4];
    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Manager");
        if (obj != null)
        {
            manager = obj.GetComponent<Manager>();
        }
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
            enemigos[casilla].defensa -= daño;

            if (enemigos[casilla] != null)
            {
                vidaEnemigo -= daño;
            }
        }
        if (aliado == false)
        {
            aliados[casilla].defensa -= daño;
            if (enemigos[casilla] != null)
            {
                vidaAliado -= daño;
            }
        }
    }
    public void Actualizar()
    {
        for (int i = 0; i < enemigos.Length; i++)
        {

        }
    }
}
