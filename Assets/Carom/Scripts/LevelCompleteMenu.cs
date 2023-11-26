using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject menuNivelCompletado;
    public String siguienteNivel;
    public void SiguienteNivel(){
        if (siguienteNivel == null){
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(siguienteNivel);
    }

    public void Reiniciar(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
