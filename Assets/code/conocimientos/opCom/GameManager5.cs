using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager5 : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor= Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    private QuizDB5 m_quizDB = null;
    private QuizUI5 m_quizUI = null;
    private AudioSource m_audioSource = null;
    public Text puntajeCorrect;
    public Text puntajeIncorrect;
    public GameObject[]pantallas;
    public Button[] botones; 
    public int maxpuntajeCorrect;
    public int maxpuntajeIncorrect;
    int correct = 0;
    int incorrect = 0;

    private void Start()
    {
        m_quizDB = GameObject.FindFirstObjectByType<QuizDB5>();
        m_quizUI = GameObject.FindFirstObjectByType<QuizUI5>();
        m_audioSource = GetComponent<AudioSource>();
        pantallas[0].SetActive(false);
        pantallas[1].SetActive(false);
        pantallas[2].SetActive(true);
        foreach (Button boton in botones)
        {
            boton.interactable = true;
        }
        NextQuestion();
    }

    private void NextQuestion()
    {
        m_quizUI.Construct(m_quizDB.GetRandom(),GiveAnswer);
    }

    private void GiveAnswer(OptionButton5 optionButton)
    {
        StartCoroutine(GiveAnswerRoutin(optionButton));
    }

    private IEnumerator GiveAnswerRoutin(OptionButton5 optionButton)
    {
        if (m_audioSource.isPlaying)
        {
            m_audioSource.Stop();
        }

        //m_audioSource.clip = optionButton.Option.correcto ? m_correctSound : m_incorrectSound;
        //optionButton.SetColor(optionButton.Option.correcto ? m_correctColor : m_incorrectColor);

        //m_audioSource.Play();
        //if(optionButton.Option.correcto)
        //{
        //    correct = correct + 1;
        //    puntajeCorrect.text=correct.ToString();
        //}
        //else
        //{
        //    incorrect=incorrect+ 1;
        //    puntajeIncorrect.text=incorrect.ToString();
        //}

        //yield return new WaitForSeconds(m_waitTime);
        //if (optionButton.Option.correcto)
        //    NextQuestion();
        //else
        //    GameOver();

        m_audioSource.clip = optionButton.Option.correcto ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Option.correcto ? m_correctColor : m_incorrectColor);
       
        m_audioSource.Play();

        yield return new WaitForSeconds(m_waitTime);

        if (optionButton.Option.correcto)
        {
            correct = correct + 1;
            puntajeCorrect.text=correct.ToString();
        }
        else
        {
            incorrect = incorrect + 1;
            puntajeIncorrect.text=incorrect.ToString();
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
    //    SceneManager.LoadScene(9);
    //}


}
