using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Text pregunta;
    public Text[] respuestas;
    public Text cronometroText;
    public Text puntaje;

    public Button[] botonesRespuestas;

    int numero1;
    int numero2;
    string preguntaFinal;
    int division;
    int respuestaAleatoria;
    int a;

    public int tiempoCronometro;
    private float contador;
    public int maximoPuntaje;

    // Start is called before the first frame update
    void Start()
    {
        puntaje.text = a.ToString();
        contador = tiempoCronometro;
        cronometroText.text = contador.ToString();
        Iniciador();
    }

    void Iniciador()
    {
        
        bool numerosValidos = false;

        while (!numerosValidos)
        {
            
            numero1 = Random.Range(4, 100);
            numero2 = Random.Range(2, 50);
            respuestaAleatoria = Random.Range(0, respuestas.Length);
            division = numero1 / numero2;

            if (numero2 > numero1 || numero1 % numero2 != 0 || numero1 == numero2)
            {
                continue;
            }
            else
            {
                preguntaFinal = numero1 + " / " + numero2;
                pregunta.text = preguntaFinal;
                numerosValidos = true;
            }

            for (int i = 0; i <= respuestas.Length - 1; i++)
            {
                if (i == respuestaAleatoria)
                {
                    respuestas[i].text = division.ToString();
                }
                else if (i != respuestaAleatoria)
                {
                    Debug.Log("no hace nada");
                    respuestas[i].text = Random.Range(4, 81).ToString();
                }
            }
        }

        
    }

    public void daleClick1()
    {
        if (respuestas[0].text == division.ToString())
        {
            a++; 
            puntaje.text = a.ToString();
        }
        contador = tiempoCronometro;
        Iniciador();
    }

    public void daleClick2()
    {
        if (respuestas[1].text == division.ToString())
        {
            a++;
            puntaje.text = a.ToString();
        }
        contador = tiempoCronometro;
        Iniciador();
    }

    public void daleClick3()
    {
        if (respuestas[2].text == division.ToString())
        {
            a++;
            puntaje.text = a.ToString();
        }
        contador = tiempoCronometro;
        Iniciador();
    }

    public void daleClick4()
    {
        if (respuestas[3].text == division.ToString())
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
        contador = contador - Time.deltaTime;

        if(puntaje.text == maximoPuntaje.ToString())
        {
            foreach (Button boton in botonesRespuestas)
            {
                boton.interactable = false;
            }
        }

        cronometroText.text = contador.ToString("0");
        if (contador <= 0)
        {
            contador = tiempoCronometro;
            cronometroText.text = contador.ToString();
        }
    }
}
