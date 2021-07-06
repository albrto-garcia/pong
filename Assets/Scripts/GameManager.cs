using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    AudioSource fuenteDeAudio;
    public AudioClip audioPrincipal, audioBoton;

    void Start()
    {
        //Capturando el audiosource para poder reproducir el intro
        fuenteDeAudio = GetComponent<AudioSource>();
        fuenteDeAudio.clip = audioPrincipal;

        //El audio estará en loop solo si estamos en inicio
        if (SceneManager.GetActiveScene().name == "Inicio")
            fuenteDeAudio.loop = true;
            
        fuenteDeAudio.Play();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Inicio")
        {
            //Si pulsa la tecla P o hace clic izquierdo empieza el juego
            if (Input.GetKeyDown(KeyCode.P) || Input.GetMouseButton(0))
            {
                fuenteDeAudio.clip = audioBoton;
                fuenteDeAudio.Play();
                StartCoroutine(TimerInicio());
            }
        }
        else
            if (SceneManager.GetActiveScene().name == "Final")
            {
                //Esto es para saber cual es el jugador que ganó
                if (Bola.golesIzquierda == 15 || Bola.golesDerecha == 15){
                    GameObject.Find("Título").GetComponent<Text>().text = "¡Jugador " + ((Bola.golesIzquierda == 15) ? 1 : 2) + " gana!";
                    Bola.golesIzquierda = Bola.golesDerecha = 0;
                }

                //Recibe cualquier tecla o click que se haga en el juego para ir a inicio
                if (Input.anyKeyDown)
                {
                    fuenteDeAudio.clip = audioBoton;
                    fuenteDeAudio.Play();
                    StartCoroutine(TimerFinal());
                }
            }
    }

    //Estos IEnumerator son para usar un timer que produce un delay de 0.5 segundos entre los cambios de escena
    IEnumerator TimerInicio()
    {
        //Creador del timer
        yield return new WaitForSeconds(0.5f);
        //Cargo la escena de Juego
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator TimerFinal()
    {
        //Creador del timer
        yield return new WaitForSeconds(0.5f);
        //Cargo la escena de Juego
        SceneManager.LoadScene("Inicio");
    }
}