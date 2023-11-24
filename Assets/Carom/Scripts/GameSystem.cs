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

    [SerializeField] GameObject menuNivelComletado;
    [SerializeField] GameObject menuPerdiste;
    [SerializeField] TextMeshProUGUI puntajeText;
    [SerializeField] int puntajeObjetivo;

    private int BolasBlancasRestantes {
        get
        {
            int cantidad = 0;

            foreach(GameObject bolaBlanca in bolasBlancas) {
                if (bolaBlanca.activeSelf) { cantidad++; }
            }

            return cantidad;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.bolasBlancas = GameObject.FindGameObjectsWithTag("BolaBlanca");
        this.bolasRojas = GameObject.FindGameObjectsWithTag("BolaRoja");
    }

    // Update is called once per frame
    void Update()
    {
        this.ActualizarPuntaje();

        if (!this.AlMenosUnaBolaBlancaEstaActiva() && this.estadoJuego == EstadosJuego.EnProceso)
        {
            this.retrasoNivelAcabado = DateTime.Now.AddSeconds(1);
            this.estadoJuego = EstadosJuego.Gano;
        }

        if (this.estadoJuego == EstadosJuego.Gano && DateTime.Now >= this.retrasoNivelAcabado && !this.eventoGanarActivado)
        {
            this.menuNivelComletado.SetActive(true);
            this.eventoGanarActivado = true;
        }

        if (!this.AlMenosUnaBolaRojaEstaActiva() && this.estadoJuego == EstadosJuego.EnProceso)
        {
            this.retrasoNivelAcabado = DateTime.Now.AddSeconds(1);
            this.estadoJuego = EstadosJuego.Perdio;
        }

        if (this.estadoJuego == EstadosJuego.Perdio && DateTime.Now >= this.retrasoNivelAcabado && !this.eventoPerderActivado)
        {
            this.menuPerdiste.SetActive(true);
            this.eventoPerderActivado = true;
        }
    }

    bool AlMenosUnaBolaBlancaEstaActiva()
    {
        foreach(GameObject bolaBlanca in this.bolasBlancas)
        {
            if (bolaBlanca.activeSelf) { return true; }
        }

        return false;
    }

    bool AlMenosUnaBolaRojaEstaActiva()
    {
        foreach(GameObject bolaRoja in this.bolasRojas)
        {
            if (bolaRoja.activeSelf) { return true; }
        }

        return false;
    }

    void ActualizarPuntaje()
    {
        if (this.puntajeText != null)
        {
            this.puntajeText.text = "Bolas Blancas Restantes: " + this.BolasBlancasRestantes;
        }
    }
}
