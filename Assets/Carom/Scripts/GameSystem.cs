using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    private enum EstadosJuego { EnProceso, Gano, Perdio }
    private GameObject[] bolasBlancas;
    private EstadosJuego estadoJuego = EstadosJuego.EnProceso;

    private DateTime tiempoAparicionMenuNivelCompletado;
    private bool eventoGanarActivado = false;


    [SerializeField] public GameObject menuNivelComletado;

    // Start is called before the first frame update
    void Start()
    {
        this.bolasBlancas = GameObject.FindGameObjectsWithTag("BolaBlanca");
    }

    // Update is called once per frame
    void Update()
    {
        if (!AlMenosUnaBolaBlancaEstaActiva() && estadoJuego != EstadosJuego.Gano)
        {
            tiempoAparicionMenuNivelCompletado = DateTime.Now.AddSeconds(1);
            estadoJuego = EstadosJuego.Gano;
        }

        if (estadoJuego == EstadosJuego.Gano && DateTime.Now >= tiempoAparicionMenuNivelCompletado && !eventoGanarActivado)
        {
            menuNivelComletado.SetActive(true);
            eventoGanarActivado = true;
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
}
