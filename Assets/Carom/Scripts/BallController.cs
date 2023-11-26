using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Camera camaraPrincipal = null;
    [SerializeField] float multiplicadorFuerza = 1.5f;

    private Transform objetoSeleccionado;
    private Rigidbody objetoSeleccionadoRigidBody;
    private Vector3 posicionInicialMouse;

    void Update()
    {
        // Mouse click izquierdo presionado
        if ( Input.GetMouseButtonDown(0) == true ) {
            this.posicionInicialMouse = Input.mousePosition;
            Ray rayo = this.camaraPrincipal.ScreenPointToRay(Input.mousePosition);

            if ( Physics.Raycast(rayo, out RaycastHit rayoAcertado) ) {
                this.objetoSeleccionado = rayoAcertado.transform;
            }

            if ( objetoSeleccionado != null && objetoSeleccionado.CompareTag("BolaRoja") ) {
                this.objetoSeleccionadoRigidBody = this.objetoSeleccionado.GetComponent<Rigidbody>();
            }
        }

        // Mouse click izquierdo soltado
        if ( Input.GetMouseButtonUp(0) == true && objetoSeleccionado != null && objetoSeleccionado.CompareTag("BolaRoja") ) {
            float rotacionCamaraPrincipalY = this.camaraPrincipal.transform.eulerAngles.y;
            Vector3 posicionFinalMouse = Input.mousePosition;
            Vector3 distancia = this.posicionInicialMouse - posicionFinalMouse;
            Vector3 fuerza = new(distancia.x, 0, distancia.y);

            // Aplicar la rotación de la cámara principal en el eje Y
            fuerza = Quaternion.AngleAxis(rotacionCamaraPrincipalY, Vector3.up) * fuerza;

            this.objetoSeleccionadoRigidBody.useGravity = true;
            this.objetoSeleccionadoRigidBody.AddForce(fuerza * this.multiplicadorFuerza);

            GameSystem.tirosRestantes--;
        }
    }
}
