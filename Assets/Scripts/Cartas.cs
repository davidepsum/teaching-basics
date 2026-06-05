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
    public TMP_Text texto_ataque;
    public int defensa;
    public TMP_Text texto_defensa;
    public int coste;
    public TMP_Text texto_coste;
    public bool enMesa;
    public bool enCasilla;
    public Manager manager;
    public Turnos turnos;
    public int casilla;
    public Vector2 posicioninicial;

    private void Start()
    {
        GameObject objetoManager = GameObject.FindWithTag("Manager");

        if (objetoManager != null)
        {
            manager = objetoManager.GetComponent<Manager>(); 
            turnos = objetoManager.GetComponent<Turnos>(); 
        }
        posicioninicial = transform.position;
        ActualizarEstadísticas();
    }
    public void ActualizarEstadísticas()
    {
        texto_ataque.text = ataque.ToString();
        texto_coste.text = coste.ToString();
        texto_defensa.text = defensa.ToString();
    }

    public void recibirDaño(int cantidad)
    {
        manager.vida -=cantidad;
        if (manager.vida <= 0)
        {
            manager.vida = 0;
            Destroy(gameObject);
        }
    }

    public void OnMouseDrag()
    {
        if (manager.turno == 0)
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Dentro");
        if (other.gameObject.CompareTag("Casilla"))
        {
            enCasilla = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Fuera");
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
                //colocarEnCasilla();
            }
            else
            {
                transform.position = posicioninicial;
            }

        }
    }
    public void Update()
    {
        if (manager.turno > 4)
        {
            manager.turno = 0 ;
            manager.pasaTurnos.interactable = true ;
        }
        if (manager.turno != 0)
        {
            Debug.Log(manager.turno);
            activarEfecto();
            manager.turno++;
        }
    }
    public void activarEfecto()
    {
        Debug.Log("Ejecutado");
        if (casilla==manager.turno)
        {
           
        }
    }
    public void RealizarDaño()
    {
        turnos.RecibirDaño(casilla,ataque);
    }
    public void RecibirDaño(int pupa)
    {
        defensa-=pupa;
        if (defensa <= 0)
        {
            gameObject.SetActive(false);
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
