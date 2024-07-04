using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript2 : MonoBehaviour
{
    public Text pregunta;
    public Text[] respuestas;
    public Text cronometroText;
    public Text puntaje;

    public Button[] botonesRespuestas;
    public GameObject canvasPrincipal;
    public GameObject pantallaFinal;
    public GameObject canvasPausa;
    private bool isPaused = false;

    int numero1;
    int numero2;
    string preguntaFinal;
    int multiplicacion;
    int respuestaAleatoria;
    int a;
    bool b;

    public int tiempoCronometro;
    private float contador;
    public int maximoPuntaje;
    public bool pasarEscena;
    public int indiceEscena;

    // Start is called before the first frame update
    void Start()
    {
        pantallaFinal.SetActive(false);
        canvasPausa.SetActive(false);
        b = true;
        int a = 0;
        foreach (Button boton in botonesRespuestas)
        {
            boton.interactable = true;
        }
        puntaje.text = a.ToString();
        contador = tiempoCronometro;
        cronometroText.text = contador.ToString();
        Iniciador();
    }

    void Iniciador()
    {
        numero1 = Random.Range(2, 10);
        numero2 = Random.Range(2, 10);
        respuestaAleatoria = Random.Range(0, respuestas.Length);
        multiplicacion = numero1 * numero2;

        preguntaFinal = numero1 + " X " + numero2;
        pregunta.text = preguntaFinal;

        List<string> posiblesRespuestas = new List<string>();
        posiblesRespuestas.Add(multiplicacion.ToString());

        for (int i = 1; i < respuestas.Length; i++)
        {
            int respuestaAleatoria = Random.Range(4, 50);
            posiblesRespuestas.Add(respuestaAleatoria.ToString());
        }

        posiblesRespuestas = posiblesRespuestas.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < respuestas.Length; i++)
        {
            respuestas[i].text = posiblesRespuestas[i];
        }
    }

    public void RepetirEjercicio()
    {
        a = 0; // Reinicia el puntaje
        puntaje.text = a.ToString(); // Muestra el puntaje en 0
        pantallaFinal.SetActive(false); // Oculta el canvas de victoria
        Start();
    }

    public void daleClick1()
    {
        click(0);
    }

    public void daleClick2()
    {
        click(1);
    }

    public void daleClick3()
    {
        click(2);
    }

    public void daleClick4()
    {
        click(3);
    }

    void click(int numRespuesta)
    {
        if (respuestas[numRespuesta].text == multiplicacion.ToString())
        {
            a++;
            puntaje.text = a.ToString();
        }
        contador = tiempoCronometro;
        Iniciador();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (puntaje.text == maximoPuntaje.ToString() && b == true)
            {
                pantallaFinal.SetActive(true);
                foreach (Button boton in botonesRespuestas)
                {
                    boton.interactable = false;
                }

                b = false;
            }

            if (b)
            {
                contador = contador - Time.deltaTime;
                cronometroText.text = contador.ToString("0");

                if (contador <= 0)
                {
                    Iniciador();
                    contador = tiempoCronometro;
                    cronometroText.text = contador.ToString();
                }
            }
        }

        if (pasarEscena)
        {
            cambiarEscena(indiceEscena);
        }
    }

    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
    public void PauseGame()
    {
        isPaused = true;
        canvasPausa.SetActive(true);
        foreach (Button boton in botonesRespuestas)
        {
            boton.interactable = false;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        canvasPausa.SetActive(false);
        foreach (Button boton in botonesRespuestas)
        {
            boton.interactable = true;
        }
    }
}
