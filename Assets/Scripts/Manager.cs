using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public int vidaAliado = 100;
    public int vidaEnemigo = 100;
    public GameObject[] baraja=new GameObject[4];
    public GameObject panelEleccion;
    public GameObject panelPartida;
    public bool[] fila1=new bool[4];
    public bool[] fila2=new bool[4];

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
    }

    public void ElegirMazo(int numero)
    {
        panelEleccion.SetActive(false);
        panelPartida.SetActive(true);
        baraja[numero].SetActive(true);
    }
}
