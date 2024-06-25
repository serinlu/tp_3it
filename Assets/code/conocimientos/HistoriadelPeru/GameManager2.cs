using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager2 : MonoBehaviour //control del juego
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    private QuizDB2 m_quizDB = null;
    private QuizUI2 m_quizUI = null;
    public Text puntajeCorrect;
    public Text puntajeIncorrect;
    public GameObject[] pantallas;
    public Button[] botones;
    public int maxpuntajeCorrect;
    public int maxpuntajeIncorrect;
    int correct = 0;
    int incorrect = 0;
    public bool pasarEscena;
    public int indiceEscena;
    private AudioSource m_audioSource = null;   
    private void Start()
    {
        m_quizDB = GameObject.FindFirstObjectByType<QuizDB2>();
        m_quizUI = GameObject.FindFirstObjectByType<QuizUI2>();
        m_audioSource = GetComponent<AudioSource>();
        pantallas[0].SetActive(false);
        pantallas[1].SetActive(false);
        foreach (Button boton in botones)
        {
            boton.interactable = true;
        }
        NextQuestion();
    }
    private void NextQuestion()
    {
        m_quizUI.Construtc(m_quizDB.GetRandom(),GiveAnswer);
    }
    private void GiveAnswer(OptionButton2 optionButton)
    { 
        StartCoroutine(GiveAnswerRoutine(optionButton));    
    }
    private IEnumerator GiveAnswerRoutine(OptionButton2 optionButton)
    { 
        if(m_audioSource.isPlaying)
            m_audioSource.Stop();

        m_audioSource.clip = optionButton.Opciones.correct ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Opciones.correct ? m_correctColor : m_incorrectColor);

        m_audioSource.Play();   

        yield return new WaitForSeconds(m_waitTime);

        if (optionButton.Opciones.correct)
        {
            correct = correct + 1;
            puntajeCorrect.text = correct.ToString();
        }

        else
        {
            incorrect = incorrect + 1;
            puntajeIncorrect.text = incorrect.ToString();
        }
        NextQuestion();

        if (correct == maxpuntajeCorrect)
        {
            pantallas[2].SetActive(false);
            pantallas[1].SetActive(true);
        }
        else if (incorrect == maxpuntajeIncorrect)
        {
            pantallas[2].SetActive(false);
            pantallas[0].SetActive(true);
        }

        if (pasarEscena)
        {
            cambiarEscena(indiceEscena);
        }
    }
    public void RepetirEjercicio()
    {
        correct = 0;
        incorrect = 0;
        puntajeCorrect.text = correct.ToString();
        puntajeIncorrect.text = incorrect.ToString();
        pantallas[0].SetActive(false); // Oculta el canvas de victoria
        pantallas[1].SetActive(false); // Oculta el canvas de victoria
        pantallas[2].SetActive(true);

        Start();
    }
    //private void GameOver()
    //{
    //    SceneManager.LoadScene(0);
    //}
    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
