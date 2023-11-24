using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    [SerializeField] GameObject bolaRoja = null;
    [SerializeField] float multiplicadorFuerzaColision = 50.0f;
    [SerializeField] AudioSource sonidoConversion;

    void OnTriggerEnter(Collider other)
    {
        // Si la bola roja choca contra una bola blanca, reemplaza la bola blanca por una bola roja
        if ( other.gameObject.CompareTag("BolaBlanca") ) {
            other.gameObject.SetActive(false);

            other.gameObject.transform.GetPositionAndRotation(out Vector3 posicionBolaBlanca, out Quaternion rotacionBolaBlanca);
            GameObject nuevaBolaRoja = Instantiate(this.bolaRoja, posicionBolaBlanca, rotacionBolaBlanca);

            Rigidbody bolaRojaRigidBody = this.bolaRoja.GetComponent<Rigidbody>();
            Rigidbody nuevaBolaRojaRigidBody = nuevaBolaRoja.GetComponent<Rigidbody>();

            nuevaBolaRojaRigidBody.AddForce(bolaRojaRigidBody.velocity * this.multiplicadorFuerzaColision);
            sonidoConversion.Play();

            GameSystem.puntaje++;
        }
    }
}
