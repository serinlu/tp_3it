using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Text pregunta;
    int numero1;
    int numero2;
    string preguntaFinal;
    int division;
    public Text[] respuestas;
    int contador;

    // Start is called before the first frame update
     void Start()
    {
        bool numerosValidos = false;

        while (!numerosValidos)
        {
            numero1 = Random.Range(4, 50);
            numero2 = Random.Range(2, 50);
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

            division = numero1 / numero2;

            List<int> opcionesRespuesta = new List<int>();

            opcionesRespuesta.Add(division);

            for (int i = 0; i < 2; i++)
            {
                int opcionAleatoria = division + Random.Range(-10, 11);

                while (opcionesRespuesta.Contains(opcionAleatoria) || opcionAleatoria == division || opcionAleatoria < 0) 
                {
                    opcionAleatoria = division + Random.Range(-10, 11);
                }

                opcionesRespuesta.Add(opcionAleatoria);
            }

            opcionesRespuesta = opcionesRespuesta.OrderBy(x => Random.value).ToList();

            for (int i = 0; i < respuestas.Length; i++)
            {
                if (i == 0) respuestas[i].text = opcionesRespuesta[i].ToString();
                else respuestas[i].text = opcionesRespuesta[i].ToString();
            }
        }

        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
