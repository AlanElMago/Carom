using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    [SerializeField] GameObject bola = null;
    [SerializeField] float altura = -20.0f;

    // Update is called once per frame
    void Update()
    {
        if (this.bola.transform.position.y < this.altura) {
            this.bola.SetActive(false);
        }
    }
}
