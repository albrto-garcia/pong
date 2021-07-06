using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Porteria : MonoBehaviour
{
    //Detecto si la bola atraviesa la porteria
    void OnCollisionEnter2D(Collision2D objeto)
    {
        if (objeto.collider.name == "Bola")
        {
            //Si es la portería izquierda
            if (this.name == "Izquierda")
            {
                //Cuento el gol y reinicio la bola
                Bola.golesDerecha++;
                objeto.collider.GetComponent<Bola>().reiniciarBola("Derecha");
            }
            //Si es la portería derecha
            else if (this.name == "Derecha")
            {
                //Cuento el gol y reinicio la bola
                Bola.golesIzquierda++;
                objeto.collider.GetComponent<Bola>().reiniciarBola("Izquierda");
            }
        }
    }
}