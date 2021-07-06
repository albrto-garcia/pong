using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Bola : MonoBehaviour
{
    //Velocidad de la pelota
    public float velocidad = 30.0f;
    //Audio Source
    AudioSource fuenteDeAudio;
    //Clips de audio
    public AudioClip audioGol, audioRaqueta, audioRebote;
    //Contadores de goles
    public static int golesIzquierda = 0, golesDerecha = 0;
    public Text contadorIzquierda, contadorDerecha;
    Vector2 outOfBoundsArriba = new Vector2(0, 30), outOfBoundsAbajo = new Vector2(0, -30);
    // Use this for initialization
    void Start()
    {
        //Recupero el componente audio source;
        fuenteDeAudio = GetComponent<AudioSource>();
        //Velocidad inicial hacia la derecha
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        //Pongo los contadores a 0
        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();
    }

    //Se ejecuta si choco con la raqueta
    void OnCollisionEnter2D(Collision2D micolision)
    {
        //Si me choco con la raqueta izquierda
        if (micolision.gameObject.name == "RaquetaIzquierda")
        {
            //Valor de x
            int x = 1;
            //Valor de y
            int y = direccionY(transform.position, micolision.transform.position);
            //Vector de dirección
            Vector2 direccion = new Vector2(x, y);
            //Aplico la velocidad a la bola
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }
        //Si me choco con la raqueta derecha
        else if (micolision.gameObject.name == "RaquetaDerecha")
        {
            //Valor de x
            int x = -1;
            //Valor de y
            int y = direccionY(transform.position, micolision.transform.position);
            //Vector de dirección
            Vector2 direccion = new Vector2(x, y);
            //Aplico la velocidad a la bola
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }

        //Para el sonido del rebote
        if (micolision.gameObject.name == "Arriba" || micolision.gameObject.name == "Abajo")
        {
            //Reproduzco el sonido del rebote
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }
    }

    //Calculo la dirección de Y
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    //Reinicio la posición de la bola
    public void reiniciarBola(string direccion)
    {
        //El juego termina cuando un jugador llega a 15 goles.
        if (golesIzquierda == 15 || golesDerecha == 15)
        {
            SceneManager.LoadScene("Final");
        }
        else
        {
            //Posición 0 de la bola
            transform.position = Vector2.zero;
            //Vector2.zero es lo mismo que new Vector2(0,0);

            //Velocidad y dirección
            if (direccion == "Derecha")
            {
                //Incremento la velocidad de la bola
                velocidad = velocidad + 1f;
                //Lo escribo en el marcador
                contadorDerecha.text = golesDerecha.ToString();
                //Reinicio la bola
                GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
                //Vector2.right es lo mismo que new Vector2(1,0)
            }
            else if (direccion == "Izquierda")
            {
                //Incremento la velocidad de la bola
                velocidad = velocidad + 1f;
                //Lo escribo en el marcador
                contadorIzquierda.text = golesIzquierda.ToString();
                //Reinicio la bola
                GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
                //Vector2.right es lo mismo que new Vector2(-1,0)
            }

            //Reproduzco el sonido del gol
            fuenteDeAudio.clip = audioGol;
            fuenteDeAudio.Play();
        }
    }

    //Esto es para prevenir en caso de que la bola salga del campo. Devuelve la bola a su posición inicial, si es que esto ocurre.
    void Update()
    {
        if (this.transform.position.y > outOfBoundsArriba.y ||
            this.transform.position.y < outOfBoundsAbajo.y)
        {
            if (this.transform.position.x < 0)
            {
                transform.position = Vector2.zero;
                GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
            }
            else
            {
                transform.position = Vector2.zero;
                GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
            }
        }
    }
}