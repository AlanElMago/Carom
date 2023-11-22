using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject menuNivelPerdido;
    public SceneAsset actualNivel; 
    
    public void Reiniciar(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
