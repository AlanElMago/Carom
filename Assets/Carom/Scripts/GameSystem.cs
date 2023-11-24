using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro; 
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private enum EstadosJuego { EnProceso, Gano, Perdio }
    private GameObject[] bolasBlancas;
    private GameObject[] bolasRojas;
    private EstadosJuego estadoJuego = EstadosJuego.EnProceso;

    private DateTime retrasoNivelAcabado;
    private bool eventoGanarActivado = false;
    private bool eventoPerderActivado = false;

    public static int puntaje;

    [SerializeField] GameObject menuNivelComletado;
    [SerializeField] GameObject menuPerdiste;
    [SerializeField] TextMeshProUGUI puntajeText;
    [SerializeField] int puntajeObjetivo;

    // Start is called before the first frame update
    void Start()
    {
        this.bolasBlancas = GameObject.FindGameObjectsWithTag("BolaBlanca");
        this.bolasRojas = GameObject.FindGameObjectsWithTag("BolaRoja");
        puntaje = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarPuntaje();
        if (!AlMenosUnaBolaBlancaEstaActiva() && estadoJuego == EstadosJuego.EnProceso)
        {
            retrasoNivelAcabado = DateTime.Now.AddSeconds(1);
            estadoJuego = EstadosJuego.Gano;
        }

        if (estadoJuego == EstadosJuego.Gano && DateTime.Now >= retrasoNivelAcabado && !eventoGanarActivado)
        {
            menuNivelComletado.SetActive(true);
            eventoGanarActivado = true;
        }

        if (!AlMenosUnaBolaRojaEstaActiva() && estadoJuego == EstadosJuego.EnProceso)
        {
            retrasoNivelAcabado = DateTime.Now.AddSeconds(1);
            estadoJuego = EstadosJuego.Perdio;
        }

        if (estadoJuego == EstadosJuego.Perdio && DateTime.Now >= retrasoNivelAcabado && !eventoPerderActivado)
        {
            menuPerdiste.SetActive(true);
            eventoPerderActivado = true;
        }
    }

    bool AlMenosUnaBolaBlancaEstaActiva()
    {
        foreach(GameObject bolaBlanca in bolasBlancas)
        {
            if (bolaBlanca.activeSelf) { return true; }
        }

        return false;
    }

    bool AlMenosUnaBolaRojaEstaActiva()
    {
        foreach(GameObject bolaRoja in bolasRojas)
        {
            if (bolaRoja.activeSelf) { return true; }
        }

        return false;
    }


    void ActualizarPuntaje()
    {    
        if (puntajeText != null)
        {
            puntajeText.text = "Puntaje: " + puntaje.ToString() + "/" + puntajeObjetivo.ToString();
        }
    }
}
