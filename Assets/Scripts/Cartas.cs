using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public enum TipoCarta
{
    Espartanos,
    MurallaChina,
    LegionRomana,
    JulioCesar,
    Escritura,
    PesteNegra,
    Cruzadas,
    Inquisicion,
    Feudalismo,
    Constantinopla,
    Renacimiento,
    Guerra30,
    Iglesias,
    Colonizacion,
    TierraNoPlana,
    Holocausto,
    Covid,
    Internet,
    Capitalismo,
    Calentamiento
}
public class Cartas : MonoBehaviour
{
    public TipoCarta tipo;
    public int ataque;
    private int ataqueInicial;
    public TMP_Text texto_ataque;
    public int defensa;
    private int defensaInicial;
    public TMP_Text texto_defensa;
    public int coste;
    private int costeInicial;
    public TMP_Text texto_coste;
    public int buff;
    public TMP_Text texto_buff;
    public bool enMesa;
    public bool enCasilla;
    [SerializeField] private int casilla;
    [SerializeField] private GameObject casillaactual;
    public bool aliado;
    private Vector2 posicioninicial;
    private Manager manager;
    private Comportamientos comportamientos;

    private void Start()
    {
        GameObject objetoManager = GameObject.FindWithTag("Manager");

        if (objetoManager != null)
        {
            manager = objetoManager.GetComponent<Manager>();
            comportamientos = objetoManager.GetComponent<Comportamientos>();
        }
        posicioninicial = transform.position;
        ActualizarEstadísticas();
        ataqueInicial = ataque;
        defensaInicial = defensa;
        costeInicial = coste;
    }
    public void OnMouseDrag()
    {
        if (comportamientos.turno == 0)
        {
            if (enMesa == false)
            {
                Vector3 posicion = Input.mousePosition;
                Vector3 posicionMundo = Camera.main.ScreenToWorldPoint(posicion);
                posicionMundo.z = 0;
                transform.position = posicionMundo;

            }
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Casilla"))
        {
            enCasilla = true;
            casillaactual = other.gameObject;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Casilla"))
        {
            enCasilla = false;
        }
    }
    private void OnMouseUp()
    {
        if (enMesa == false)
        {
            if (enCasilla == true)
            {
                for (int i = 0; i < comportamientos.aliados.Length; i++)
                {
                    if (casillaactual.name == "Casilla " + (i+1))
                    {
                        //Cambiar el if inferior para que solo se puedan colocar las cartas en el lado de quien es el turno
                        // if ((comportamientos.aliados[i] == null) && (comportamientos.turnoaliado == true))
                        if (comportamientos.aliados[i] == null)
                        {
                            enMesa = true;
                            transform.position = casillaactual.transform.position;
                            casilla = i;
                            aliado = true;
                            comportamientos.AsignarAliado(gameObject, casilla);
                        }
                        else
                        {
                            transform.position = posicioninicial;
                        }
                    }
                }
                for (int i = 0; i < comportamientos.enemigos.Length; i++)
                {
                    if (casillaactual.name == "Casilla B " + (i + 1))
                    {
                        //Cambiar el if inferior para que solo se puedan colocar las cartas en el lado de quien es el turno
                        //if ((comportamientos.enemigos[i] == null) && (comportamientos.turnoaliado==false))
                        if (comportamientos.enemigos[i] == null)
                        {
                            enMesa = true;
                            transform.position = casillaactual.transform.position;
                            casilla = i;
                            aliado = false;
                            comportamientos.AsignarEnemigo(gameObject, casilla);
                        }
                        else
                        {
                            transform.position = posicioninicial;
                        }
                    }
                }
            }
            else
            {
                transform.position = posicioninicial;
            }
        }
    }

    public void OnMouseEnter()
    {
        if (enMesa==false)
        {
            Vector2 nuevaPosicion= new Vector2(posicioninicial.x, posicioninicial.y + 1f);
            transform.position = nuevaPosicion;
        }
    }
    public void OnMouseExit()
    {
        if (enMesa==false)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicioninicial, 5f);
        }
    }
    public void ActualizarEstadísticas()
    {
        texto_ataque.text = ataque.ToString();
        texto_coste.text = coste.ToString();
        texto_defensa.text = defensa.ToString();
        if (texto_buff != null)
        {
            texto_buff.text = buff.ToString();
        }
    }
    public void activarAlColocar()
    {
        if ((tipo == TipoCarta.LegionRomana) || (tipo==TipoCarta.Escritura) || (tipo==TipoCarta.Feudalismo))
        {
            comportamientos.ReforzarAliados(casilla, aliado, buff, tipo);
        }
        if (tipo == TipoCarta.MurallaChina)
        {
            comportamientos.RestarVida(buff,tipo,aliado);
        }
    }
    public void activarEfecto()
    {
        if (tipo == TipoCarta.PesteNegra)
        {
            comportamientos.DañarEnArea(aliado, ataque);
        }
        if (tipo == TipoCarta.Cruzadas)
        {
            if (aliado == true)
            {
                if (comportamientos.enemigos[casilla] == null)
                {
                    for (int i = 0; i < comportamientos.enemigos.Length; i++)
                    {
                        if ((comportamientos.enemigos[i] != null) && (comportamientos.aliados[i] == null))
                        {
                            casilla = i;
                            i++;
                            casillaactual = GameObject.Find("Casilla " + i);
                            transform.position = casillaactual.transform.position;
                            i = comportamientos.enemigos.Length;
                        }
                    }
                }
            }
            if (aliado == false)
            {
                if (comportamientos.aliados[casilla] == null)
                {
                    for (int i = 0; i < comportamientos.aliados.Length; i++)
                    {
                        if ((comportamientos.enemigos[i] == null) && (comportamientos.aliados[i] != null))
                        {
                            casilla = i;
                            i++;
                            casillaactual = GameObject.Find("Casilla B " + i);
                            transform.position = casillaactual.transform.position;
                            i = comportamientos.enemigos.Length;
                        }
                    }
                }
            }
            comportamientos.RealizarDaño(ataque, casilla, aliado);
        }
        if (tipo == TipoCarta.Inquisicion)
        {
            int numero = Random.Range(2, comportamientos.cartas.Length);
            int numero2;
            for (int i = 0; i < numero; i++)
            {
                numero2 = Random.Range(0, comportamientos.cartas.Length);
                if (numero2 != casilla)
                {
                    if (comportamientos.cartas[numero2] != null)
                    {
                        comportamientos.cartas[numero2].defensa -= ataque;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
        }
        if (tipo == TipoCarta.Constantinopla)
        {
            comportamientos.turno = 0;
            comportamientos.RealizarDaño(ataque, casilla, aliado);
        }

        else
        {
            comportamientos.RealizarDaño(ataque, casilla, aliado);
        }
    }

    
    public void Morir()
    {
        if (defensa <= 0)
        {
            if (tipo == TipoCarta.JulioCesar)
            {
                comportamientos.ReforzarAliados(casilla, aliado,buff, tipo);
            }
            ataque = ataqueInicial;
            defensa = defensaInicial;
            coste = costeInicial;
            enMesa =false;
            enCasilla=false;
            if (aliado == true)
            {
                comportamientos.aliados[casilla] = null;
            }
            else
            {
                comportamientos.enemigos[casilla] = null;
            }
            comportamientos.cartas[casilla] = null;
            casilla = 0;
            transform.position = posicioninicial;
            ActualizarEstadísticas();
        }
    }
}
