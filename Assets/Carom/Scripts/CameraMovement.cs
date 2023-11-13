using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject camaraPrincipal = null;
    [SerializeField] float velocidad = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            float rotacionCamaraPrincipalY = this.camaraPrincipal.transform.eulerAngles.y;
            Vector3 distancia = new(0, 0, velocidad);
            distancia = Quaternion.AngleAxis(rotacionCamaraPrincipalY, Vector3.up) * distancia;
            this.camaraPrincipal.transform.position += distancia;
        }
        if (Input.GetKey(KeyCode.A)) {
            float rotacionCamaraPrincipalY = this.camaraPrincipal.transform.eulerAngles.y;
            Vector3 distancia = new(-velocidad, 0, 0);
            distancia = Quaternion.AngleAxis(rotacionCamaraPrincipalY, Vector3.up) * distancia;
            this.camaraPrincipal.transform.position += distancia;
        }
        if (Input.GetKey(KeyCode.S)) { 
            float rotacionCamaraPrincipalY = this.camaraPrincipal.transform.eulerAngles.y;
            Vector3 distancia = new(0, 0, -velocidad);
            distancia = Quaternion.AngleAxis(rotacionCamaraPrincipalY, Vector3.up) * distancia;
            this.camaraPrincipal.transform.position += distancia;
        }
        if (Input.GetKey(KeyCode.D)) {
            float rotacionCamaraPrincipalY = this.camaraPrincipal.transform.eulerAngles.y;
            Vector3 distancia = new(velocidad, 0, 0);
            distancia = Quaternion.AngleAxis(rotacionCamaraPrincipalY, Vector3.up) * distancia;
            this.camaraPrincipal.transform.position += distancia;
        }
    }
}
