using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro; 
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    internal enum EstadosJuego { EnProceso, TiempoExtra, Evaluacion, Terminado }
    internal static EstadosJuego estadoJuego = EstadosJuego.EnProceso;
    private bool ganaUltimoTiro = false;

    internal static int tirosRestantes = 0;
    [SerializeField] int tirosRestantesIniciales = 20;

    private GameObject[] bolasBlancas;
    private GameObject[] bolasRojas;

    private DateTime tiempoExtra;

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

        // Evaluar para tiempo extra
        if (GameSystem.estadoJuego == GameSystem.EstadosJuego.EnProceso && (!this.AlMenosUnaBolaBlancaEstaActiva() || !this.AlMenosUnaBolaRojaEstaActiva() || GameSystem.tirosRestantes <= 0)) {
            GameSystem.estadoJuego = GameSystem.EstadosJuego.TiempoExtra;
        }

        // Evaluar cantidad de tiempo extra
        if (GameSystem.estadoJuego == GameSystem.EstadosJuego.TiempoExtra) {
            int segundosTiempoExtra = GameSystem.tirosRestantes > 0 ? 1 : 10;
            this.tiempoExtra = DateTime.Now.AddSeconds(segundosTiempoExtra);
            GameSystem.estadoJuego = GameSystem.EstadosJuego.Evaluacion;
        }

        // Intentar terminar juego
        if (GameSystem.estadoJuego == GameSystem.EstadosJuego.Evaluacion) {
            TerminarJuego();
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

    void TerminarJuego()
    {
        if (GameSystem.tirosRestantes < 0) {
            this.menuPerdiste.SetActive(true);
            GameSystem.estadoJuego = GameSystem.EstadosJuego.Terminado;
            return;
        }

        if (!AlMenosUnaBolaBlancaEstaActiva() && !this.ganaUltimoTiro) {
            this.tiempoExtra = DateTime.Now.AddSeconds(1);
            this.ganaUltimoTiro = true;
        }

        if (DateTime.Now < tiempoExtra) {
            return;
        }

        if (AlMenosUnaBolaBlancaEstaActiva()) {
            this.menuPerdiste.SetActive(true);
            GameSystem.estadoJuego = GameSystem.EstadosJuego.Terminado;
            return;
        }

        this.menuNivelComletado.SetActive(true); 
        GameSystem.estadoJuego = GameSystem.EstadosJuego.Terminado;
    }

    void ActualizarDatos()
    {
        this.bolasBlancasRestantesText.text = "Bolas Blancas Restantes: " + this.BolasBlancasRestantes;
        this.tirosRestantesText.text = "Tiros Restantes: " + GameSystem.tirosRestantes;
    }
}
