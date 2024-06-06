using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManagerTimer : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    [SerializeField] private int m_totalQuestions;
    [SerializeField] private Text QuestionNumber;
    [SerializeField] private Text Results;
    [SerializeField] private Text Score;
    [SerializeField] private GameObject[] pantallasfinales;

    private QuizDBTimer m_quizDB = null;
    private QuizUITimer m_quizUI = null;
    private AudioSource m_audioSource = null;
    private double m_score = 0;
    private int m_counterQuestion = 0;
    private int m_correctAnswers = 0;
    private int m_incorrectAnswers = 0;
    private string m_commentary = "";
    private float startTime;
    private Question currentQuestion;

    private void Start()
    {
        startTime = Time.time;
        m_quizDB = GameObject.FindFirstObjectByType<QuizDBTimer>();
        m_quizUI = GameObject.FindFirstObjectByType<QuizUITimer>();
        m_audioSource = GetComponent<AudioSource>();
        pantallasfinales[0].SetActive(true);
        pantallasfinales[1].SetActive(false);

        QuestionNumber.text = (m_counterQuestion + 1).ToString();
        m_correctAnswers = 0;
        m_incorrectAnswers = 0;
        m_commentary = "";

        Score.text = 0 + "/" + m_totalQuestions;

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
        }
        else
        {
            m_incorrectAnswers++;
        }

        m_counterQuestion++;
        UpdateUI();

        if (m_counterQuestion == m_totalQuestions)
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
            if (m_audioSource.isPlaying)
                m_audioSource.Stop();
            m_audioSource.clip = m_correctSound;
            m_audioSource.Play();
        }
        else
        {
            m_incorrectAnswers++;
            if (m_audioSource.isPlaying)
                m_audioSource.Stop();
            m_audioSource.clip = m_incorrectSound;
            m_audioSource.Play();
        }

        yield return new WaitForSeconds(m_waitTime);

        m_quizUI.ClearInput();
        m_quizUI.FocusInput();
        m_counterQuestion++;
        UpdateUI();

        if (m_counterQuestion == m_totalQuestions)
        {
            GameResults();
        }
        else
        {
            NextQuestion();
        }
    }

    private void UpdateUI()
    {
        Score.text = m_correctAnswers.ToString() + "/" + m_totalQuestions;
        QuestionNumber.text = (m_counterQuestion + 1).ToString();
    }

    private void GameResults()
    {
        CreateResults();
        pantallasfinales[0].SetActive(false);
        pantallasfinales[1].SetActive(true);
    }

    private void CreateResults()
    {
        float endTime = Time.time;
        float totalTime = endTime - startTime;
        m_commentary = "Tiempo: " + totalTime.ToString("F2") + "s";

        m_score = (m_correctAnswers / (double)m_totalQuestions) * 100;
        m_commentary += "\nPuntaje: " + m_score.ToString("F2") + "%";

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
        m_counterQuestion = 0;
        m_commentary = "";

        Score.text = 0 + "/" + m_totalQuestions;

        pantallasfinales[0].SetActive(true);
        pantallasfinales[1].SetActive(false);

        Results.text = m_commentary;

        NextQuestion();
    }
}
