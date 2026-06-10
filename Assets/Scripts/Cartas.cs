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
    public void asignarAliado()
    {
        //if (comportamientos.turnoaliado==true)
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
        if ((tipo == TipoCarta.LegionRomana) || (tipo==TipoCarta.Escritura))
        {
            comportamientos.ReforzarAliados(casilla, aliado, buff, tipo);
        }
    }
    public void activarEfecto()
    {
        comportamientos.RealizarDaño(ataque, casilla, aliado);
        
        if (tipo == TipoCarta.MurallaChina)
        {
            
        }

        if (tipo == TipoCarta.LegionRomana)
        {
            comportamientos.RealizarDaño(ataque, casilla, aliado);
        }
        /*
        if (tipo == TipoCarta.JulioCesar)
        {
            sacrificarVidaJugador(3);
            buffAliados(0, 3);
        }

        if (tipo == TipoCarta.Escritura)
        {
            buffAliados(2, 2);
        }

        if (tipo == TipoCarta.PesteNegra)
        {
            dañoAreaEnemigos(3);
            bajarAtaqueEnemigos(1);
        }

        if (tipo == TipoCarta.Cruzadas)
        {
            avanzarHaciaEnemigos();
        }

        if (tipo == TipoCarta.Inquisicion)
        {
            dañoAleatorioEnemigos(4);
        }

        if (tipo == TipoCarta.Feudalismo)
        {
            buffAliados(3, 0);
            sacrificarVidaJugador(2);
        }

        if (tipo == TipoCarta.Constantinopla)
        {
            aliadosAtacanDeNuevo();
        }

        if (tipo == TipoCarta.Renacimiento)
        {
            buffAliados(2, 2);
        }

        if (tipo == TipoCarta.Guerra30)
        {
            dañoTodos(2);
        }

        if (tipo == TipoCarta.Iglesias)
        {
            recolocarAliados();
        }

        if (tipo == TipoCarta.Colonizacion)
        {
            convertirEnemigos();
        }

        if (tipo == TipoCarta.TierraNoPlana)
        {
            enemigosSeAutodañan(2);
        }

        if (tipo == TipoCarta.Holocausto)
        {
            quemarEnemigos(4);
        }

        if (tipo == TipoCarta.Covid)
        {
            dañoAreaEnemigos(2);
            saltarTurnoEnemigo();
        }

        if (tipo == TipoCarta.Internet)
        {
            bloquearDañoJugador();
        }

        if (tipo == TipoCarta.Capitalismo)
        {
            robarMitadStatsAliados();
        }

        if (tipo == TipoCarta.Calentamiento)
        {
            dañoPorTurno(1);
        }
        */
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

    /*
    public void curar(int cantidad)
    {
        manager.vida += cantidad;
    }

    public void sumarAtaque(int cantidad)
    {
        ataque = ataque + cantidad;
    }

    public void sumarDefensa(int cantidad)
    {
        defensa = defensa + cantidad;
    }

    private void colocarEnCasilla()
    {
        float x = transform.position.x;

        float[] posiciones = { -3f, -1f, 1f, 3f };

        for (int i = 0; i < 4; i++)
        {
            if (x < posiciones[i] && manager.fila1[i] == false)
            {
                transform.position = new Vector3(posiciones[i], -3f, 0);
                manager.fila1[i] = true;
                enMesa = true;
                casilla = i;
                activarEfecto();
                return;
            }
        }
    }

    public void activarEfecto()
    {
        if (tipo == TipoCarta.Espartanos)
        {
            ataque = ataque + 5;
            defensa = defensa + 5;
        }

        if (tipo == TipoCarta.MurallaChina)
        {
            defensa = defensa - 1;
        }

        if (tipo == TipoCarta.LegionRomana)
        {
            dañoEnemigo(4);
            buffAliados(2, 2);
        }

        if (tipo == TipoCarta.JulioCesar)
        {
            sacrificarVidaJugador(3);
            buffAliados(0, 3);
        }

        if (tipo == TipoCarta.Escritura)
        {
            buffAliados(2, 2);
        }

        if (tipo == TipoCarta.PesteNegra)
        {
            dañoAreaEnemigos(3);
            bajarAtaqueEnemigos(1);
        }

        if (tipo == TipoCarta.Cruzadas)
        {
            avanzarHaciaEnemigos();
        }

        if (tipo == TipoCarta.Inquisicion)
        {
            dañoAleatorioEnemigos(4);
        }

        if (tipo == TipoCarta.Feudalismo)
        {
            buffAliados(3, 0);
            sacrificarVidaJugador(2);
        }

        if (tipo == TipoCarta.Constantinopla)
        {
            aliadosAtacanDeNuevo();
        }

        if (tipo == TipoCarta.Renacimiento)
        {
            buffAliados(2, 2);
        }

        if (tipo == TipoCarta.Guerra30)
        {
            dañoTodos(2);
        }

        if (tipo == TipoCarta.Iglesias)
        {
            recolocarAliados();
        }

        if (tipo == TipoCarta.Colonizacion)
        {
            convertirEnemigos();
        }

        if (tipo == TipoCarta.TierraNoPlana)
        {
            enemigosSeAutodañan(2);
        }

        if (tipo == TipoCarta.Holocausto)
        {
            quemarEnemigos(4);
        }

        if (tipo == TipoCarta.Covid)
        {
            dañoAreaEnemigos(2);
            saltarTurnoEnemigo();
        }

        if (tipo == TipoCarta.Internet)
        {
            bloquearDañoJugador();
        }

        if (tipo == TipoCarta.Capitalismo)
        {
            robarMitadStatsAliados();
        }

        if (tipo == TipoCarta.Calentamiento)
        {
            dañoPorTurno(1);
        }
    }

    void dañoEnemigo(int cantidad) {}
    void dañoAreaEnemigos(int cantidad) {}
    void bajarAtaqueEnemigos(int cantidad) {}
    void avanzarHaciaEnemigos() {}
    void dañoAleatorioEnemigos(int cantidad) {}
    void aliadosAtacanDeNuevo() {}
    void dañoTodos(int cantidad) {}
    void recolocarAliados() {}
    void convertirEnemigos() {}
    void enemigosSeAutodañan(int cantidad) {}
    void quemarEnemigos(int cantidad) {}
    void saltarTurnoEnemigo() {}
    void bloquearDañoJugador() {}
    void robarMitadStatsAliados() {}
    void dañoPorTurno(int cantidad) {}
    void buffAliados(int atk, int def) {}
    void sacrificarVidaJugador(int cantidad) {}
    */
}
