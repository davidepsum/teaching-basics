using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public int vidaAliado = 100;
    public int vidaEnemigo = 100;

    public TMP_Text eleccion;
    public Button empezar;
    public Button[] botones=new Button[4];
    
    public GameObject[] baraja=new GameObject[4];
    public string tagAliada;
    public GameObject barajaAliada;
    public string tagEnemiga;
    public GameObject barajaEnemiga;

    public GameObject panelEleccion;
    public GameObject panelPartida;

    public Cartas[] aliados=new Cartas[5];
    public Cartas[] enemigos=new Cartas[5];

    private void Start()
    {
        for (int i = 0; i < baraja.Length; i++)
        {
            baraja[i].SetActive(false);
            botones[i].interactable = true;
        }
        panelEleccion.SetActive(true);
        panelPartida.SetActive(false);
        empezar.gameObject.SetActive(false);
        eleccion.text = "Elige el mazo del jugador 1:";
    }

    public string EstablecerBandos(int numero)
    {
        switch (numero)
        {
            case 0:
                return "Antigua";
            case 1:
                return "Media";
            case 2:
                return "Moderna";
            case 3:
                return "Contemporánea";
            default:
                return "";
        }
    }
    public void ElegirMazo(int numero)
    {
        if (barajaAliada == null)
        {
            tagAliada = EstablecerBandos(numero);
            barajaAliada = baraja[numero];
            botones[numero].interactable=false;
            eleccion.text = "Elige el mazo del jugador 2:";
        }
        else
        {
            if(barajaEnemiga == null)
            {
                tagEnemiga = EstablecerBandos(numero);
                barajaEnemiga = baraja[numero];
                botones[numero].interactable = false;
                empezar.gameObject.SetActive(true);
                eleccion.gameObject.SetActive(false);
            }
        }
    }
    public void EmpezarPartida()
    {
        panelEleccion.SetActive(false);
        panelPartida.SetActive(true);
        barajaAliada.SetActive(true);
        GameObject[] cartas = GameObject.FindGameObjectsWithTag(tagAliada);
        GameObject[] cartas2 = GameObject.FindGameObjectsWithTag(tagEnemiga);
        for (int i = 0; i < cartas.Length; i++)
        {
            aliados[i] = cartas[i].GetComponent<Cartas>();
            enemigos[i] = cartas2[i].GetComponent<Cartas>();
            aliados[i].aliado = true;
            enemigos[i].aliado=false;
        }
        barajaEnemiga.SetActive(true);
    }
}
