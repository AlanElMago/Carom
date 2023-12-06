using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Camera camaraPrincipal = null;
    [SerializeField] float multiplicadorFuerza = 1.5f;
    [SerializeField] GameObject puntoPrefab;

    private Transform objetoSeleccionado;
    private Rigidbody objetoSeleccionadoRigidBody;
    private Vector3 posicionInicialMouse;

    private List<GameObject> puntos = new(11);
    private DateTime tiempoInstanciarProximoPunto = DateTime.Now;

    void Update()
    {
        // Mouse click izquierdo presionado
        if ( Input.GetMouseButtonDown(0) == true ) {
            this.objetoSeleccionado = null;
            this.posicionInicialMouse = Input.mousePosition;
            Ray rayo = this.camaraPrincipal.ScreenPointToRay(Input.mousePosition);

            if ( Physics.Raycast(rayo, out RaycastHit rayoAcertado) ) {
                this.objetoSeleccionado = rayoAcertado.transform;
            }

            if ( objetoSeleccionado != null && objetoSeleccionado.CompareTag("BolaRoja") ) {
                this.objetoSeleccionadoRigidBody = this.objetoSeleccionado.GetComponent<Rigidbody>();
            }
        }

        if ( Input.GetMouseButton(0) == true && objetoSeleccionado != null && objetoSeleccionado.CompareTag("BolaRoja") ) {
            if (DateTime.Now < tiempoInstanciarProximoPunto) {
                return;
            }

            float rotacionCamaraPrincipalY = this.camaraPrincipal.transform.eulerAngles.y;
            Vector3 posicionFinalMouse = Input.mousePosition;
            Vector3 distancia = this.posicionInicialMouse - posicionFinalMouse;
            Vector3 fuerza = new(distancia.x, 0, distancia.y);

            fuerza = Quaternion.AngleAxis(rotacionCamaraPrincipalY, Vector3.up) * fuerza;
        
            GameObject nuevoPunto = Instantiate(puntoPrefab, objetoSeleccionado.transform.position, new());
            nuevoPunto.GetComponent<Rigidbody>().AddForce(5.0f * this.multiplicadorFuerza * fuerza);

            puntos.Add(nuevoPunto);

            if ( puntos.Count >= 10 ) {
                puntos[0].SetActive(false);
                puntos.RemoveAt(0);
            }

            tiempoInstanciarProximoPunto = DateTime.Now.AddMilliseconds(0.25);
        }

        // Mouse click izquierdo soltado
        if ( Input.GetMouseButtonUp(0) == true && objetoSeleccionado != null && objetoSeleccionado.CompareTag("BolaRoja") ) {
            foreach (GameObject punto in puntos) {
                punto.SetActive(false);
            }

            puntos.Clear();

            float rotacionCamaraPrincipalY = this.camaraPrincipal.transform.eulerAngles.y;
            Vector3 posicionFinalMouse = Input.mousePosition;
            Vector3 distancia = this.posicionInicialMouse - posicionFinalMouse;
            Vector3 fuerza = new(distancia.x, 0, distancia.y);

            // Aplicar la rotación de la cámara principal en el eje Y
            fuerza = Quaternion.AngleAxis(rotacionCamaraPrincipalY, Vector3.up) * fuerza;

            this.objetoSeleccionadoRigidBody.useGravity = true;
            this.objetoSeleccionadoRigidBody.AddForce(fuerza * this.multiplicadorFuerza);

            // No cambien el orden de los Audio Sources en el prefab de la bola roja, porfas 😭
            this.objetoSeleccionado.GetComponents<AudioSource>()[1].time = 0.1f;
            this.objetoSeleccionado.GetComponents<AudioSource>()[1].Play();

            GameSystem.tirosRestantes--;
        }
    }
}
