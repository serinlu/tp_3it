using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManagerNBasic : MonoBehaviour
{

    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    public GameObject[] pantalla;
    public Canvas canva;
    public Button[] boton;
    public Text puntaje1, puntaje2,porcentaje,porcentajeInc,tiempo,Result;
    public Text temporizador;
    public int maxPuntajeCorrecto;
    public int maxPuntajeIncorrecto;
    private BasicDB m_basicDB = null;
    private BasicUI m_basicUI = null;
    private AudioSource m_audioSource = null;
    int a = 0;
    int b = 0;
    private float tiempoInicial;
    private float tiempoTranscurrido;
    private bool cronometroActivo;
    string tiempoFormato;


    private void Start()
    {

        IniciarCronometro();

        pantalla[0].SetActive(false);
        canva.enabled = true;

        m_basicDB = GameObject.FindObjectOfType<BasicDB>();
        m_basicUI = GameObject.FindObjectOfType<BasicUI>();
        m_audioSource = GetComponent<AudioSource>();
        foreach (Button botones in boton)
        {
            botones.interactable = true;
        }
        NextQuestion();
    }
    private void Update()
    {
        if (cronometroActivo)
        {
            tiempoTranscurrido = Time.time - tiempoInicial;
            temporizador.text = tiempoFormato;
            ActualizarTiempo();


        }
    }
    private void NextQuestion()
    {
        m_basicUI.Construct(m_basicDB.GetRandom(), GiveAnswer);
    }
    private void GiveAnswer(OptionButtonNB optionButton)
    {
        StartCoroutine(GiveAnswerRoutine(optionButton));
    }
    private IEnumerator GiveAnswerRoutine(OptionButtonNB optionButton)
    {
        if (m_audioSource.isPlaying)
            m_audioSource.Stop();

        m_audioSource.clip = optionButton.Option.correct ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Option.correct ? m_correctColor : m_incorrectColor);

        m_audioSource.Play();

        yield return new WaitForSeconds(m_waitTime);

        if (optionButton.Option.correct)
        {
            a = a + 1;
            puntaje1.text = a.ToString();
        }
        else
        {
            b = b + 1;
            puntaje2.text = b.ToString();
        }
        NextQuestion();
        
        int cantPreg = m_basicDB.CantPreguntas();
        int cant = m_basicDB.CantidadPreguntas;
        int Porct_Aciertos = 0;
        int Porct_Errados = 0;
        if (cantPreg==0)
        {
            pantalla[0].SetActive(true);
            canva.enabled = false;
            Porct_Aciertos = (a*100)/(cant-1);
            Porct_Errados = 100 - Porct_Aciertos;
            porcentaje.text=Porct_Aciertos.ToString()+"%";
            porcentajeInc.text= Porct_Errados.ToString()+"%";   
            DetenerCronometro();
            tiempo.text = tiempoFormato;
        }
        if (Porct_Aciertos <= 50)
        {
            Result.text = "TIENES QUE MEJORAR";
        }
        else if(Porct_Aciertos>50 && Porct_Aciertos <= 75)
        {
            Result.text = "vAS POR BUEN CAMINO";
        }
        else if (Porct_Aciertos >75  && Porct_Aciertos <= 99)
        {
            Result.text = "BIEN HECHO";
        }
        else if (Porct_Aciertos==100)
        {
            Result.text = "EXCELENTE";
        }

    }
    public void Repetir()
    {
        a = 0;
        b = 0;
        puntaje1.text = a.ToString();
        puntaje2.text = b.ToString();
        pantalla[0].SetActive(false);

        Start();
        ReiniciarCronometro();
    }

    public void IniciarCronometro()
    {
        tiempoInicial = Time.time;
        cronometroActivo = true;
    }

    public void DetenerCronometro()
    {
        cronometroActivo = false;
    }

    public void ReiniciarCronometro()
    {
        tiempoInicial = Time.time;
        tiempoTranscurrido = 0f;
        ActualizarTiempo();
    }
    private void ActualizarTiempo()
    {
        TimeSpan tiempoSpan = TimeSpan.FromSeconds(tiempoTranscurrido);
        tiempoFormato = string.Format("{0:D2}:{1:D2}", tiempoSpan.Minutes, tiempoSpan.Seconds);
        tiempo.text = tiempoFormato;
    }
    private void GameOver()
    {
        SceneManager.LoadScene(9);
    }
}
