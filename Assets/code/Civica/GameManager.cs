using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor= Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    private QuizDB m_quizDB = null;
    private QuizUI m_quizUI = null;
    private AudioSource m_audioSource = null;

    private void Start()
    {
        m_quizDB = GameObject.FindObjectOfType<QuizDB>();
        m_quizUI = GameObject.FindObjectOfType<QuizUI>();
        m_audioSource = GetComponent<AudioSource>();

        NextQuestion();
    }

    private void NextQuestion()
    {
        m_quizUI.Construct(m_quizDB.GetRandom(),GiveAnswer);
    }

    private void GiveAnswer(OptionButton optionButton)
    {
        StartCoroutine(GiveAnswerRoutin(optionButton));
    }

    private IEnumerator GiveAnswerRoutin(OptionButton optionButton)
    {
        if (m_audioSource.isPlaying)
        {
            m_audioSource.Stop();
        }

        m_audioSource.clip = optionButton.Option.correcto ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Option.correcto ? m_correctColor : m_incorrectColor);

        m_audioSource.Play();

        yield return new WaitForSeconds(m_waitTime);
        if (optionButton.Option.correcto)
            NextQuestion();
        else
            GameOver();

    }
    
    private void GameOver()
    {
        SceneManager.LoadScene(9);
    }

    
}
