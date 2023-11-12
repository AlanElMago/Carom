using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour
{
    [SerializeField] GameObject bolaRoja = null;
    [SerializeField] float multiplicadorFuerzaColision = 50.0f;

    void OnTriggerEnter(Collider other)
    {
        // Si la bola roja choca contra una bola blanca, reemplaza la bola blanca por una bola roja
        if ( other.gameObject.CompareTag("BolaBlanca") ) {
            other.gameObject.SetActive(false);

            Vector3 posicionBolaBlanca = other.gameObject.transform.position;
            Quaternion rotacionBolaBlanca = other.gameObject.transform.rotation;
            GameObject nuevaBolaRoja = Instantiate(bolaRoja, posicionBolaBlanca, rotacionBolaBlanca);

            Rigidbody bolaRojaRigidBody = bolaRoja.GetComponent<Rigidbody>();
            Rigidbody nuevaBolaRojaRigidBody = nuevaBolaRoja.GetComponent<Rigidbody>();

            nuevaBolaRojaRigidBody.AddForce(bolaRojaRigidBody.velocity * multiplicadorFuerzaColision);
        }
    }
}
