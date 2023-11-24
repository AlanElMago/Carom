using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro; 
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    internal enum EstadosJuego { EnProceso, Gano, Perdio }
    internal static EstadosJuego estadoJuego = EstadosJuego.EnProceso;

    internal static int tirosRestantes = 0;
    [SerializeField] int tirosRestantesIniciales = 20;

    private GameObject[] bolasBlancas;
    private GameObject[] bolasRojas;

    private DateTime retrasoNivelAcabado;
    private bool eventoGanarActivado = false;
    private bool eventoPerderActivado = false;

    [SerializeField] GameObject menuNivelComletado;
    [SerializeField] GameObject menuPerdiste;

    [SerializeField] TextMeshProUGUI bolasBlancasRestantesText;
    [SerializeField] TextMeshProUGUI tirosRestantesText;

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
        GameSystem.estadoJuego = GameSystem.EstadosJuego.EnProceso;
        this.bolasBlancas = GameObject.FindGameObjectsWithTag("BolaBlanca");
        this.bolasRojas = GameObject.FindGameObjectsWithTag("BolaRoja");
        GameSystem.tirosRestantes = this.tirosRestantesIniciales;
    }

    // Update is called once per frame
    void Update()
    {
        this.ActualizarDatos();

        if (!this.AlMenosUnaBolaBlancaEstaActiva() && GameSystem.tirosRestantes > 0 && GameSystem.estadoJuego != EstadosJuego.Gano)
        {
            this.retrasoNivelAcabado = DateTime.Now.AddSeconds(1);
            GameSystem.estadoJuego = EstadosJuego.Gano;
        }

        if (GameSystem.estadoJuego == EstadosJuego.Gano && DateTime.Now >= this.retrasoNivelAcabado && !this.eventoGanarActivado)
        {
            this.menuNivelComletado.SetActive(true);
            this.eventoGanarActivado = true;
        }

        if ((!this.AlMenosUnaBolaRojaEstaActiva() || GameSystem.tirosRestantes <= 0) && GameSystem.estadoJuego == EstadosJuego.EnProceso)
        {
            this.retrasoNivelAcabado = GameSystem.tirosRestantes > 0 ? DateTime.Now.AddSeconds(1) : DateTime.Now.AddSeconds(11);
            GameSystem.estadoJuego = EstadosJuego.Perdio;
        }

        if (GameSystem.estadoJuego == EstadosJuego.Perdio && (DateTime.Now >= this.retrasoNivelAcabado || tirosRestantes < 0) && !this.eventoPerderActivado)
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

    void ActualizarDatos()
    {
        this.bolasBlancasRestantesText.text = "Bolas Blancas Restantes: " + this.BolasBlancasRestantes;
        this.tirosRestantesText.text = "Tiros Restantes: " + GameSystem.tirosRestantes;
    }
}
