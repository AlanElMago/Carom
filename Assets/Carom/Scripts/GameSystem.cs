using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    internal static int _recuperacionTiros = 0;
    [SerializeField] int recuperacionTiros = 1;

    internal static List<GameObject> bolasBlancas;
    internal static List<GameObject> bolasRojas;

    private DateTime tiempoExtra;

    [SerializeField] GameObject menuNivelComletado;
    [SerializeField] GameObject menuPerdiste;

    [SerializeField] TextMeshProUGUI bolasBlancasRestantesText;
    [SerializeField] TextMeshProUGUI tirosRestantesText;
    [SerializeField] AudioSource audioAplausos;
    [SerializeField] AudioSource audioAbucheos;

    private int BolasBlancasRestantes {
        get
        {
            int cantidad = 0;

            foreach(GameObject bolaBlanca in GameSystem.bolasBlancas) {
                if (bolaBlanca.activeSelf) { cantidad++; }
            }

            return cantidad;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameSystem.estadoJuego = GameSystem.EstadosJuego.EnProceso;
        GameSystem.bolasBlancas = GameObject.FindGameObjectsWithTag("BolaBlanca").ToList();
        GameSystem.bolasRojas = GameObject.FindGameObjectsWithTag("BolaRoja").ToList();
        GameSystem.tirosRestantes = this.tirosRestantesIniciales;
        GameSystem._recuperacionTiros = this.recuperacionTiros;
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
        foreach(GameObject bolaBlanca in GameSystem.bolasBlancas)
        {
            if (bolaBlanca.activeSelf) { return true; }
        }

        return false;
    }

    bool AlMenosUnaBolaRojaEstaActiva()
    {
        foreach(GameObject bolaRoja in GameSystem.bolasRojas)
        {
            if (bolaRoja.activeSelf) { return true; }
        }

        return false;
    }

    void TerminarJuego()
    {
        if (GameSystem.tirosRestantes < 0) {
            this.menuPerdiste.SetActive(true);
            audioAbucheos.Play(); 
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
            audioAbucheos.Play(); 
            GameSystem.estadoJuego = GameSystem.EstadosJuego.Terminado;
            return;
        }

        this.menuNivelComletado.SetActive(true); 
        audioAplausos.Play();
        GameSystem.estadoJuego = GameSystem.EstadosJuego.Terminado;
    }

    void ActualizarDatos()
    {
        this.bolasBlancasRestantesText.text = "Bolas Blancas Restantes: " + this.BolasBlancasRestantes;
        this.tirosRestantesText.text = "Tiros Restantes: " + GameSystem.tirosRestantes;
    }
}
