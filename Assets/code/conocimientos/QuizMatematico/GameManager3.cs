using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class GameManager3 : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    [SerializeField] private int correctasMax = 0; // Número de aciertos requeridos para ganar
    [SerializeField] private int incorrectasMax = 0; // Número de fallos requeridos para perder
    [SerializeField] private Text correctastxt;
    [SerializeField] private Text incorrectastxt;
    [SerializeField] private GameObject[] pantallasfinales;
    

    private QuizDB3 m_quizDB = null;
    private QuizUI3 m_quizUI = null;
    private AudioSource m_audioSource = null;
    private int m_correctAnswers = 0; // Contador de aciertos
    private int m_incorrectAnswers = 0;
    public bool pasarEscena;
    public int indiceEscena;

    private void Start()
    {
        m_quizDB = GameObject.FindFirstObjectByType<QuizDB3>();
        m_quizUI = GameObject.FindFirstObjectByType<QuizUI3>();
        m_audioSource = GetComponent<AudioSource>();
        pantallasfinales[0].SetActive(false); //pantalla de victoria
        pantallasfinales[1].SetActive(false); //pantalla de derrota
        pantallasfinales[2].SetActive(true); //pantalla de juego

        correctastxt.text = m_correctAnswers.ToString();
        incorrectastxt.text = m_incorrectAnswers.ToString();

        NextQuestion();
    }

    private void NextQuestion()
    {
        m_quizUI.Construct(m_quizDB.GetRandom(), GiveAnswer);
    }

    private void GiveAnswer(OpcionesBoton3 opcionBoton)
    {
        StartCoroutine(GiveAnswerRoutine(opcionBoton));
    }

    private IEnumerator GiveAnswerRoutine(OpcionesBoton3 opcionBoton)
    {
        if(m_audioSource == true)
            m_audioSource.Stop();

        m_audioSource.clip = opcionBoton.Opcion.correct ? m_correctSound : m_incorrectSound;
        opcionBoton.SetColor(opcionBoton.Opcion.correct ? m_correctColor : m_incorrectColor);

        m_audioSource.Play();

        yield return new WaitForSeconds(m_waitTime);

        if (opcionBoton.Opcion.correct)
        {
            m_correctAnswers++;
            correctastxt.text = m_correctAnswers.ToString();
            if (m_correctAnswers == correctasMax)
            {
                GameWin();
            }
            else
                NextQuestion();
        }
        else
        {
            m_incorrectAnswers++;
            incorrectastxt.text = m_incorrectAnswers.ToString();
            if (m_incorrectAnswers == incorrectasMax)
            {
                GameOver();
            }
            else
                NextQuestion();
        }
        if (pasarEscena)
        {
            cambiarEscena(indiceEscena);
        }
    }

    private void GameOver()
    {
        pantallasfinales[2].SetActive(false);
        pantallasfinales[0].SetActive(true);
    }

    private void GameWin()
    {
        pantallasfinales[2].SetActive(false);
        pantallasfinales[1].SetActive(true);
    }

    public void RestartGame()
    {
        m_correctAnswers = 0;
        m_incorrectAnswers = 0;

        correctastxt.text = m_correctAnswers.ToString();
        incorrectastxt.text = m_incorrectAnswers.ToString();

        // Volver a activar la pantalla de juego y desactivar las otras pantallas
        pantallasfinales[0].SetActive(false);
        pantallasfinales[1].SetActive(false);
        pantallasfinales[2].SetActive(true);

        NextQuestion();
    }

    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
    public void PauseGame()
    {
        pantallasfinales[0].SetActive(false);
        pantallasfinales[2].SetActive(true);
    }

    public void ResumeGame()
    {
        pantallasfinales[2].SetActive(false);
        pantallasfinales[0].SetActive(true);
    }
}
