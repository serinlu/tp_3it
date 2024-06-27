using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]

public class escribe : MonoBehaviour
{
    private AudioSource m_audioSource;
    public AudioClip m_correctSound;
    public AudioClip m_incorrectSound;

    public Text letraCanvas;
    public Text pregunta;
    public Text respuesta;
    public Text correctastxt;
    public Text incorrectastxt;
    public InputField input;
    public GameObject finalCanvas;
    public GameObject canvasPausa;
    public GameObject[] pantallafinal;
    public GameObject GUI;
    public int maxCorrectas; //respuestas correctas para ganar
    public int maxIncorrectas; //respuestas incorrectas antes de perder
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

    int aleatorioLetraString, correctasC, incorrectasC;
    // Start is called before the first frame update
    public void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        GUI.SetActive(true);
        pantallafinal[0].SetActive(false); //pantalla de victoria
        pantallafinal[1].SetActive(false); //pantalla de derrota
        canvasPausa.SetActive(false);
        correctasC = 0;
        incorrectasC = 0;

        incorrectastxt.text = incorrectasC.ToString();
        correctastxt.text = correctasC.ToString();
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
        bool correct = false;
        if (pregunta.text == pregunt[0] && respuesta.text.ToLower().Trim() == respuesta1)
        {
            correctasC++;
            correctastxt.text = correctasC.ToString();
            Debug.Log("has acertado");
            correct = true;
        }
        else if (pregunta.text == pregunt[1] && respuesta.text.ToLower().Trim() == respuesta2)
        {
            correctasC++;
            correctastxt.text = correctasC.ToString();
            Debug.Log("has acertado");
            correct = true;
        }
        else if (pregunta.text == pregunt[2] && respuesta.text.ToLower().Trim() == respuesta3)
        {
            correctasC++;
            correctastxt.text = correctasC.ToString();
            Debug.Log("has acertado");
            correct = true;
        }
        else if (pregunta.text == pregunt[3] && respuesta.text.ToLower().Trim() == respuesta4)
        {
            correctasC++;
            correctastxt.text = correctasC.ToString();
            Debug.Log("has acertado");
            correct = true;
        }
        else
        {
            incorrectasC++;
            incorrectastxt.text = incorrectasC.ToString();
            Debug.Log("has fallado");
        }

        //Determina que sonido elegir según si ha sido un acierto o no
        if (m_audioSource.isPlaying)
        {
            m_audioSource.Stop();
        }

        m_audioSource.clip = correct ? m_correctSound : m_incorrectSound;
        m_audioSource.Play();

        //Para determinar si gana o pierde:
        if (correctasC == maxCorrectas)
        {
            finalCanvas.SetActive(true);
            GUI.SetActive(false);
            pantallafinal[0].SetActive(true);
        }
        else if (incorrectasC == maxIncorrectas)
        {
            finalCanvas.SetActive(true);
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
        correctasC = 0;
        incorrectasC = maxIncorrectas;
        correctastxt.text = correctasC.ToString();
        incorrectastxt.text = incorrectasC.ToString();
        valoresBooleanosFalsos();
        Start();
    }
    public void PauseGame()
    {
        GUI.SetActive(false);
        canvasPausa.SetActive(true);
    }

    public void ResumeGame()
    {
        canvasPausa.SetActive(false);
        GUI.SetActive(true);
    }
}
