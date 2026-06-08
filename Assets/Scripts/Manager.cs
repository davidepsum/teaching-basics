using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public int vida;
    public GameObject[] baraja=new GameObject[4];
    public GameObject panelEleccion;
    public GameObject panelPartida;
    public bool[] fila1=new bool[4];
    public bool[] fila2=new bool[4];
    public int turno;
    public bool turnoaliado;
    public Button pasaTurnos;

    private void Start()
    {
        for (int i = 0; i < baraja.Length; i++)
        {
            baraja[i].SetActive(false);
        }
        for (int i=0;i< fila1.Length; i++)
        {
            fila1[i]=false;
        }
        panelEleccion.SetActive(true);
        panelPartida.SetActive(false);
        turno = 0;
    }

    public void ElegirMazo(int numero)
    {
        panelEleccion.SetActive(false);
        panelPartida.SetActive(true);
        baraja[numero].SetActive(true);
    }

    public void PasarTurno()
    {
        turno++;
        if (turno > 0)
        {
            pasaTurnos.interactable = false;
        }
    }
}
