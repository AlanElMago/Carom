using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; 
using System;

public class menu : MonoBehaviour
{
    private int nivelActual = 1;
    public string nombreNivel = "Nivel1";
    public TextMeshProUGUI textoNivel;
    public RawImage fondoNivel; 
    public Texture[] texturasFondo;

    private void Start()
    {
        ActualizarFondo();
    }

    public void SiguienteNivel()
    {
        if (nivelActual < 5)
        {
            nivelActual++;
            nombreNivel = "Nivel" + nivelActual.ToString();
            textoNivel.text = nombreNivel;
            textoNivel.text = "NIVEL " + nivelActual.ToString();
            ActualizarFondo();
        }
    }

    public void NivelAnterior()
    {
        if (nivelActual > 1)
        {
            nivelActual--;
            nombreNivel = "Nivel" + nivelActual.ToString();
            textoNivel.text = "NIVEL " + nivelActual.ToString();
            ActualizarFondo();
        }
    }

    public void EmpezarNivel(){
        SceneManager.LoadScene(nombreNivel);
    }

    public void Salir(){
        Application.Quit();
        Debug.Log("Salir");
    }

    private void ActualizarFondo()
    {
        try
        {
            // Intenta cargar la textura correspondiente al nivel actual
            if (fondoNivel != null && texturasFondo != null && nivelActual - 1 < texturasFondo.Length)
            {
                fondoNivel.texture = texturasFondo[nivelActual - 1];
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al cargar el fondo del nivel: " + ex.Message);
        }
    }


}
