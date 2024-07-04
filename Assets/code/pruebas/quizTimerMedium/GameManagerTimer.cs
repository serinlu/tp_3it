using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManagerTimer : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    [SerializeField] private Text Results;
    [SerializeField] private Text Score;
    [SerializeField] private Text Incorrectas;
    [SerializeField] private Text Restantes;
    [SerializeField] private Text TimeText;
    [SerializeField] private GameObject[] pantallasfinales;

    private QuizDBTimer m_quizDB = null;
    private QuizUITimer m_quizUI = null;
    private AudioSource m_audioSource = null;
    private int m_totalQuestions = 20;
    private double m_score = 0;
    private int restantesInt = 20;
    private int m_correctAnswers = 0;
    private int m_incorrectAnswers = 0;
    private string m_commentary = "";
    private bool TimerIsRunning = true;
    public float TiempoTranscurrido = 0;
    private Question currentQuestion;

    private void Start()
    {
        pantallasfinales[0].SetActive(false);
        pantallasfinales[1].SetActive(false);
        pantallasfinales[2].SetActive(false);
        pantallasfinales[3].SetActive(true);
        TimerIsRunning = false;
    }
    public void Inicio()
    {
        pantallasfinales[0].SetActive(true);
        pantallasfinales[1].SetActive(false);
        pantallasfinales[2].SetActive(false);
        pantallasfinales[3].SetActive(false);
        TimerIsRunning = true;
        m_quizDB = GameObject.FindFirstObjectByType<QuizDBTimer>();
        m_quizUI = GameObject.FindFirstObjectByType<QuizUITimer>();
        m_audioSource = GetComponent<AudioSource>();

        m_correctAnswers = 0;
        m_incorrectAnswers = 0;
        m_commentary = "";

        Score.text = m_correctAnswers.ToString();
        Restantes.text = restantesInt.ToString();
        NextQuestion();
    }

    private void NextQuestion()
    {
        currentQuestion = m_quizDB.GetRandom();
        if (currentQuestion.questionType == QuestionType.MultipleChoice)
        {
            m_quizUI.Construct(currentQuestion, GiveAnswer, null);
        }
        else if (currentQuestion.questionType == QuestionType.TextInput)
        {
            m_quizUI.Construct(currentQuestion, null, GiveTextAnswer);
        }
    }

    private void GiveAnswer(OptionButton optionButton)
    {
        StartCoroutine(GiveAnswerRoutine(optionButton));
    }

    private IEnumerator GiveAnswerRoutine(OptionButton optionButton)
    {
        if (m_audioSource.isPlaying)
            m_audioSource.Stop();

        m_audioSource.clip = optionButton.Option.correct ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Option.correct ? m_correctColor : m_incorrectColor);

        m_audioSource.Play();

        yield return new WaitForSeconds(m_waitTime);

        if (optionButton.Option.correct)
        {
            m_correctAnswers++;
            restantesInt--;
        }
        else
        {
            m_incorrectAnswers++;
            restantesInt--;
        }

        Update();

        if (restantesInt == 0)
        {
            GameResults();
        }
        else
        {
            NextQuestion();
        }
    }

    private void GiveTextAnswer(string answer)
    {
        StartCoroutine(GiveTextAnswerRoutine(answer));
    }

    private IEnumerator GiveTextAnswerRoutine(string answer)
    {
        if (currentQuestion.correctAnswer.Trim().ToLower() == answer.Trim().ToLower())
        {
            m_correctAnswers++;
            restantesInt--;
            if (m_audioSource.isPlaying)
                m_audioSource.Stop();
            m_audioSource.clip = m_correctSound;
            m_audioSource.Play();
        }
        else
        {
            m_incorrectAnswers++;
            restantesInt--;
            if (m_audioSource.isPlaying)
                m_audioSource.Stop();
            m_audioSource.clip = m_incorrectSound;
            m_audioSource.Play();
        }

        yield return new WaitForSeconds(m_waitTime);

        m_quizUI.ClearInput();
        m_quizUI.FocusInput();
        Update();

        if (restantesInt == 0)
        {
            GameResults();
        }
        else
        {
            NextQuestion();
        }
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
                GameResults();
            }
        }
        Score.text = m_correctAnswers.ToString();
        Restantes.text = restantesInt.ToString();
        Incorrectas.text = m_incorrectAnswers.ToString();
    }

    private void GameResults()
    {
        CreateResults();
        pantallasfinales[0].SetActive(false);
        pantallasfinales[1].SetActive(true);
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CreateResults()
    {
        int porcentajeCorrecto = (m_correctAnswers * 100) / m_totalQuestions;
        int porcentajeIncorrecto = (m_incorrectAnswers * 100) / m_totalQuestions;
        int minutes = Mathf.FloorToInt(TiempoTranscurrido / 60);
        int seconds = Mathf.FloorToInt(TiempoTranscurrido % 60);
        m_commentary = "Tiempo: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        m_score = (m_correctAnswers / (double)m_totalQuestions) * 100;
        m_commentary += "\nCorrectas: " + m_correctAnswers.ToString() + " (" + porcentajeCorrecto.ToString() + "%)" + 
            "\nIncorrectas: " + m_incorrectAnswers.ToString() + " (" + porcentajeIncorrecto.ToString() + "%)" +
            "\nNo respondidas: " + restantesInt.ToString();

        if (m_score == 100) m_commentary += "\nNota: ¡Excelente!";
        else if (m_score >= 90) m_commentary += "\nNota: ¡Bien hecho!";
        else if (m_score >= 75) m_commentary += "\nNota: ¡Estás en buen camino!";
        else m_commentary += "\nNota: Debes mejorar.";

        Results.text = m_commentary;
    }

    public void RestartGame()
    {
        m_correctAnswers = 0;
        m_incorrectAnswers = 0;
        m_commentary = "";

        Score.text = m_correctAnswers.ToString();

        pantallasfinales[0].SetActive(true);
        pantallasfinales[1].SetActive(false);

        Results.text = m_commentary;

        NextQuestion();
    }
    public void PauseGame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pantallasfinales[0].SetActive(false);
            pantallasfinales[2].SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pantallasfinales[2].SetActive(false);
            pantallasfinales[0].SetActive(true);
        }
    }
    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
