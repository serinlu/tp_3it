using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor = Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    public GameObject[] pantalla;
    public Button[] boton;
    public Text puntaje1, puntaje2;
    public int maxPuntajeCorrecto;
    public int maxPuntajeIncorrecto;
    private QuizDB m_quizDB = null;
    private QuizUI m_quizUI = null;
    private AudioSource m_audioSource = null;
    int a = 0;
    int b = 0;
    private void Start()
    {


        pantalla[0].SetActive(false);
        pantalla[1].SetActive(false);

        m_quizDB = GameObject.FindObjectOfType<QuizDB>();
        m_quizUI = GameObject.FindObjectOfType<QuizUI>();
        m_audioSource = GetComponent<AudioSource>();
        foreach (Button botones in boton)
        {
            botones.interactable = true;
        }
        NextQuestion();
    }
    private void NextQuestion()
    {
        m_quizUI.Construct(m_quizDB.GetRandom(), GiveAnswer);
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
            a = a + 1;
            puntaje1.text = a.ToString();
        }
        else
        {
            b = b + 1;
            puntaje2.text = b.ToString();
        }
        NextQuestion();
        if (maxPuntajeCorrecto == a)
        {
            pantalla[1].SetActive(true);
            foreach (Button botones in boton)
            {
                botones.interactable = false;
            }
        }
        else if (maxPuntajeIncorrecto == b)
        {
            pantalla[0].SetActive(true);
            foreach (Button botones in boton)
            {
                botones.interactable = false;
            }
        }
    }
    public void Repetir()
    {
        a = 0;
        b = 0;
        puntaje1.text = a.ToString();
        puntaje2.text = b.ToString();
        pantalla[0].SetActive(false);
        pantalla[1].SetActive(false);

        Start();
    }
    private void GameOver()
    {
        SceneManager.LoadScene(9);
    }
    
}
