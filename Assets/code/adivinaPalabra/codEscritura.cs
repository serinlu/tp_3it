using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class escribe : MonoBehaviour
{
    public Text letraCanvas;
    public Text pregunta;
    public Text respuesta;
    public Text puntajetxt;
    public Text vidastxt;
    public InputField input;
    public GameObject[] pantallafinal;
    public GameObject GUI;
    public int maxPuntaje; //puntaje maximo para ganar
    public int maxVidas; //vidas disponibles antes de perder
    string letras;
    public bool pasarEscena;
    public int indiceEscena;

    string[] a = { "Facebook, Tik tok, Ig son ...", "Tiene ocho patas", "El segundo cereal más cultivado del mundo", "Primera letra del abecedario" };
    string[] b = { "Animal para cargar cosas", "B_____ días!", "Accion al tomar agua", "Donde transportas el agua" };
    string[] c = { "Lo usas para llamar a tus amigos", "Animal de cuatro patas que usa montura", "Lugar donde vives", "Que te crece de la cabeza"};
    string[] d = { "Sumar, restar, multiplicar y ... ", "Tiene seis caras y es un cubo", "Forma parte de tu mano", "Necesito tomar una d____" };
    string[] e = { "Es grande y tiene una trompa", "Sale del tomacorriente", "Salida en ingles", "Sinónimo de grande"};
    string[] f = { "Alumbra la calle", "El tallarín es un ...", "País de Europa", "Para prender la hoguera"};
    string[] g = { "Animal que dice miau", "Pone huevos", "Carga carros con un gancho", "Sinónimo de triunfar" };

    bool a1,b1,c1,d1,e1,f1,g1;

    int aleatorioLetraString, puntos, vidasrestantes;
    // Start is called before the first frame update
    public void Start()
    {
        GUI.SetActive(true);
        pantallafinal[0].SetActive(false); //pantalla de victoria
        pantallafinal[1].SetActive(false); //pantalla de derrota
        puntos = 0;
        vidasrestantes = maxVidas;

        vidastxt.text = vidasrestantes.ToString();
        puntajetxt.text = puntos.ToString() + "/" + maxPuntaje.ToString();
        inicioEjercicio();
    }

    void asignaPreguntaRandom(string[] array) //asigna pregunta según la letra generada
    {
        int randomLetra = UnityEngine.Random.Range(0, array.Length);
        pregunta.text = array[randomLetra].ToString();
    }
    void inicioEjercicio()
    {
        letras = "ABCDEFG";
        aleatorioLetraString = UnityEngine.Random.Range(0, letras.Length);
        letraCanvas.text = letras[aleatorioLetraString].ToString();
        switch (letraCanvas.text)
        {
            case "A":
                a1 = true;
                asignaPreguntaRandom(a);
                break;
            case "B":
                b1 = true;
                asignaPreguntaRandom(b);
                break;
            case "C":
                c1 = true;
                asignaPreguntaRandom(c);
                break;
            case "D":
                d1 = true;
                asignaPreguntaRandom(d);
                break;
            case "E":
                e1 = true;
                asignaPreguntaRandom(e);
                break;
            case "F":
                f1 = true;
                asignaPreguntaRandom(f);
                break;
            case "G":
                g1 = true;
                asignaPreguntaRandom(g);
                break;
        }
    }
    public void pruebaInvoke()
    {
        Invoke("inicioEjercicio", float.Epsilon ); //churrito.Epsilon
        input.text = "";
    }
    void respuestaParametros(string[] pregunt, string respuesta1, string respuesta2, string respuesta3, string respuesta4)
    {
        if (pregunta.text == pregunt[0] && respuesta.text.ToLower().Trim() == respuesta1)
        {
            puntos++;
            puntajetxt.text = puntos.ToString() + "/" + maxPuntaje.ToString();
            Debug.Log("has acertado");
        }
        else if (pregunta.text == pregunt[1] && respuesta.text.ToLower().Trim() == respuesta2)
        {
            puntos++;
            puntajetxt.text = puntos.ToString() + "/" + maxPuntaje.ToString();
            Debug.Log("has acertado");
        }
        else if (pregunta.text == pregunt[2] && respuesta.text.ToLower().Trim() == respuesta3)
        {
            puntos++;
            puntajetxt.text = puntos.ToString() + "/" + maxPuntaje.ToString();
            Debug.Log("has acertado");
        }
        else if (pregunta.text == pregunt[3] && respuesta.text.ToLower().Trim() == respuesta4)
        {
            puntos++;
            puntajetxt.text = puntos.ToString() + "/" + maxPuntaje.ToString();
            Debug.Log("has acertado");
        }
        else
        {
            vidasrestantes--;
            vidastxt.text = vidasrestantes.ToString();
            Debug.Log("has fallado");
        }

        //Para determinar si gana o pierde:
        if (puntos == maxPuntaje)
        {
            GUI.SetActive(false);
            pantallafinal[0].SetActive(true);
        }
        else if (vidasrestantes == 0)
        {
            GUI.SetActive(false);
            pantallafinal[1].SetActive(true);
        }
    }
    public void valoresBooleanosFalsos()
    {
        a1 = false; b1 = false; c1 = false; d1 = false; e1 = false; f1 = false; g1 = false;
    }
    public void respuestaBoton()
    {
        if (a1)
        {
            respuestaParametros(a, "aplicaciones", "araña", "arroz", "a");
        }
        else if (b1)
        {
            respuestaParametros(b, "burro", "buenos", "beber", "botella");
        }
        else if (c1)
        {
            respuestaParametros(c, "celular", "caballo", "casa", "cabello");
        }
        else if (d1)
        {
            respuestaParametros(d, "dividir", "dado", "dedos", "ducha");
        }
        else if (e1)
        {
            respuestaParametros(e, "elefante", "electricidad", "exit", "enorme");
        }
        else if (f1)
        {
            respuestaParametros(f, "foco", "fideo", "francia", "fuego");
        }
        else if (g1)
        {
            respuestaParametros(g, "gato", "gallina", "grúa", "ganar");
        }
    }
    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
    private void Update()
    {
        if (pasarEscena)
        {
            cambiarEscena(indiceEscena);
        }
    }
    public void Reset()
    {
        puntos = 0;
        vidasrestantes = maxVidas;
        puntajetxt.text = puntos.ToString() + "/" + maxPuntaje.ToString();
        vidastxt.text = vidasrestantes.ToString();
        valoresBooleanosFalsos();
        Start();
    }
}
