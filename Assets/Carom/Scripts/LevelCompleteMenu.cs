using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject menuNivelCompletado;
    public SceneAsset siguienteNivel;

    public void SiguienteNivel(){
        if (siguienteNivel == null){
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(siguienteNivel.name);
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
