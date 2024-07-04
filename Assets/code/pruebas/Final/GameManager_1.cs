using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager_1 : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_delay = 0.0f;

    private QuizDB_1 m_quizDB = null;
    private QuizUI_1 m_quizUI = null;
    private AudioSource m_audioSource = null;

    public Text puntajeCorrect;
    public Text puntajeIncorrect;
    public GameObject pantallaFinal;
    public GameObject canvasPrincipal;
    public GameObject canvasPausa;
    public GameObject canvasPrevio;
    public Text mensajeFinal;

    public Text resultadoCorrecto;
    int porcentajeCorrecto;

    public Text resultadoIncorrecto;
    int porcentajeIncorrecto;

    public Text preguntasNoRespondidas;

    public Text tiempoFinal;
    public Text conclusion;
    public Text restantes;
    int restantesInt = 30;
    public Button[] botones;
    int correct = 0;
    int incorrect = 0;

    public bool pasarEscena;
    public int indiceEscena;

    public float TiempoTranscurrido = 0;
    public bool TimerIsRunning = false;
    public Text TimeText;

    private void Start()
    {
        canvasPrevio.SetActive(true);
        canvasPrincipal.SetActive(false);
        canvasPausa.SetActive(false);
        pantallaFinal.SetActive(false);
        TimerIsRunning = false;
    }
    public void Inicio()
    {
        pantallaFinal.SetActive(false);
        canvasPrevio.SetActive(false);
        canvasPausa.SetActive(false);
        canvasPrincipal.SetActive(true);
        restantes.text = restantesInt.ToString();
        m_quizDB = GameObject.FindFirstObjectByType<QuizDB_1>();
        m_quizUI = GameObject.FindFirstObjectByType<QuizUI_1>();
        m_audioSource = GetComponent<AudioSource>();

        TimerIsRunning = true;
        foreach (Button boton in botones)
        {
            boton.interactable = true;
        }
        NextQuestion();
    }

    private void NextQuestion()
    {
        m_quizUI.Construct(m_quizDB.GetRandom(), GiveAnswer);
        restantes.text = restantesInt.ToString();
    }

    private void GiveAnswer(OptionButton_1 optionButton)
    {
        StartCoroutine(GiveAnswerRoutine(optionButton));
    }

    private IEnumerator GiveAnswerRoutine(OptionButton_1 optionButton)
    {
        if (m_audioSource.isPlaying) m_audioSource.Stop();

        m_audioSource.clip = optionButton.Option.IsCorrect ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Option.IsCorrect ? m_correctColor : m_incorrectColor);

        m_audioSource.Play();

        yield return new WaitForSeconds(m_delay);

        if (optionButton.Option.IsCorrect)
        {
            correct++;
            puntajeCorrect.text = correct.ToString();
            restantesInt--;
        }
        else
        {
            incorrect++;
            puntajeIncorrect.text = incorrect.ToString();
            restantesInt--;
        }
        NextQuestion();

        if(restantesInt == 0) GameOver();

        if (pasarEscena) cambiarEscena(indiceEscena);
    }

    private void Update()
    {
        if (TimerIsRunning)
        {
            if (TiempoTranscurrido < 300)
            {
                TiempoTranscurrido += Time.deltaTime;
                DisplayTime(TiempoTranscurrido);
            }
            else
            {
                TiempoTranscurrido = 300;
                TimerIsRunning = false;
                GameOver();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void GameOver()
    {
        foreach (Button boton in botones)
        {
            boton.interactable = false;
        }
        canvasPrincipal.SetActive(false);
        MostrarPantallaFinal();
    }

    private void MostrarPantallaFinal()
    {
        pantallaFinal.SetActive(true);
        mensajeFinal.text = "Ha finalizado la prueba!";
        porcentajeCorrecto = ((correct * 100) / (correct + incorrect));
        resultadoCorrecto.text = correct.ToString() + " (" + porcentajeCorrecto.ToString() + "%)";
        porcentajeIncorrecto = ((incorrect * 100) / (correct + incorrect));
        resultadoIncorrecto.text = incorrect.ToString() + " (" + porcentajeIncorrecto.ToString() + "%)";
        preguntasNoRespondidas.text = (30 - (correct + incorrect)).ToString();
        int minutes = Mathf.FloorToInt(TiempoTranscurrido / 60);
        int seconds = Mathf.FloorToInt(TiempoTranscurrido % 60);
        tiempoFinal.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (porcentajeCorrecto == 100)
        {
            conclusion.text = "Excelente! Has respondido todas las preguntas correctamente!";
        }
        else if (porcentajeIncorrecto == 100)
        {
            conclusion.text = "Debes seguir practicando.";
        }
        else if (porcentajeCorrecto >= 75 && porcentajeCorrecto <= 95)
        {
            conclusion.text = "Muy bien! Sigue practicando!";
        }
        else if (porcentajeCorrecto < 70 && porcentajeCorrecto <= 50)
        {
            conclusion.text = "Sigue practicando!";
        }
        else if (porcentajeCorrecto < 50 && porcentajeCorrecto <= 5)
        {
            conclusion.text = "Debes seguir practicando.";
        }
    }

    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
    public void PauseGame()
    {
        if (TimerIsRunning)
        {
            Time.timeScale = 0;
            TimerIsRunning = false;
            canvasPrincipal.SetActive(false);
            canvasPausa.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (!TimerIsRunning)
        {
            Time.timeScale = 1;
            TimerIsRunning = true;
            canvasPausa.SetActive(false);
            canvasPrincipal.SetActive(true);
        }
    }
}
