using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public int vidaAliado = 100;
    public int vidaEnemigo = 100;
    public TMP_Text vidaTextoAliado;
    public TMP_Text vidaTextoEnemigo;

    public int manaAliado;
    public TMP_Text manaTextoAliado;
    public int manaEnemigo;
    public TMP_Text manaTextoEnemigo;

    public TMP_Text eleccion;
    public Button empezar;
    public Button[] botones=new Button[4];

    public Comportamientos comportamientos;
    
    public GameObject[] baraja=new GameObject[4];
    public string tagAliada;
    public GameObject barajaAliada;
    public string tagEnemiga;
    public GameObject barajaEnemiga;

    public GameObject panelEleccion;
    public GameObject panelPartida;
    public GameObject panelFinal;
    public TMP_Text victoria;

    public Cartas[] aliados;
    public Cartas[] enemigos;

    private void Start()
    {
        GameObject obj = GameObject.FindWithTag("Manager");
        if (obj != null)
        {
            comportamientos = obj.GetComponent<Comportamientos>();
        }
        for (int i = 0; i < baraja.Length; i++)
        {
            baraja[i].SetActive(false);
            botones[i].interactable = true;
        }
        panelEleccion.SetActive(true);
        panelPartida.SetActive(false);
        panelFinal.SetActive(false);
        empezar.gameObject.SetActive(false);
        eleccion.text = "Elige el mazo del jugador 1:";
        vidaAliado = 100;
        vidaEnemigo = 100;
        manaAliado = 10;
        manaEnemigo = 10;
    }
    public void Update()
    {
        vidaTextoAliado.text=vidaAliado.ToString();
        vidaTextoEnemigo.text=vidaEnemigo.ToString();
        manaTextoAliado.text = manaAliado.ToString();
        manaTextoEnemigo.text = manaEnemigo.ToString();
        if (manaEnemigo > 10)
        {
            manaEnemigo = 10;
        }
        if (manaAliado > 10)
        {
            manaAliado = 10;
        }
        if ((vidaEnemigo <= 0) || (vidaAliado <= 0))
        {
            panelPartida.SetActive(false);
            panelFinal.SetActive(true);
            if (vidaAliado > 0)
            {
                victoria.text = "Jugador 1 gana";
            }
            if (vidaEnemigo > 0)
            {
                victoria.text = "Jugador 2 gana";
            }
        }
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
        barajaEnemiga.SetActive(true);
        GameObject[] cartas = GameObject.FindGameObjectsWithTag(tagAliada);
        aliados=new Cartas[cartas.Length];
        for (int i = 0; i < aliados.Length; i++)
        {
            aliados[i] = cartas[i].GetComponent<Cartas>();
            aliados[i].aliado = true;
        }
        GameObject[] cartas2 = GameObject.FindGameObjectsWithTag(tagEnemiga);
        enemigos = new Cartas[cartas2.Length];
        for (int i = 0; i < enemigos.Length; i++)
        {
            enemigos[i] = cartas2[i].GetComponent<Cartas>();
            enemigos[i].aliado=false;
            enemigos[i].gameObject.SetActive(false);
        }
    }
    public void MostrarCartas()
    {
        if (comportamientos.turnoaliado == true)
        {
            for (int i = 0; i < aliados.Length; i++)
            {
                aliados[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < enemigos.Length; i++)
            {
                enemigos[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < aliados.Length; i++)
            {
                aliados[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < enemigos.Length; i++)
            {
                enemigos[i].gameObject.SetActive(true);
            }
        }
    }
}
