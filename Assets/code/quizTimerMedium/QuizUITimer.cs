using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUITimer : MonoBehaviour
{
    [SerializeField] private Text m_question = null;
    [SerializeField] private Image m_questionImage = null;
    [SerializeField] private List<OptionButton> m_buttonList = null;
    [SerializeField] private InputField m_answerInputField = null; // Campo de entrada de texto
    [SerializeField] private Button m_submitButton = null; // Botón para enviar la respuesta de entrada de texto

    private Action<string> textCallback;

    public void Construct(Question q, Action<OptionButton> callback, Action<string> textCallback)
    {
        this.textCallback = textCallback;
        m_question.text = q.text;

        if (q.imagenSprite != null)
        {
            m_questionImage.sprite = q.imagenSprite;
            m_questionImage.gameObject.SetActive(true);
            RectTransform rectTransform = m_questionImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = q.imageSize;
        }
        else
        {
            m_questionImage.gameObject.SetActive(false);
        }

        if (q.questionType == QuestionType.MultipleChoice)
        {
            ShowButtons(q, callback);
            m_answerInputField.gameObject.SetActive(false);
            m_submitButton.gameObject.SetActive(false);
        }
        else if (q.questionType == QuestionType.TextInput)
        {
            m_answerInputField.gameObject.SetActive(true);
            m_submitButton.gameObject.SetActive(true);
            HideButtons();
            m_submitButton.onClick.RemoveAllListeners();
            m_submitButton.onClick.AddListener(() => textCallback(m_answerInputField.text));

            m_answerInputField.onEndEdit.RemoveAllListeners();
            m_answerInputField.onEndEdit.AddListener(OnEndEdit);
        }
    }

    private void ShowButtons(Question q, Action<OptionButton> callback)
    {
        for (int n = 0; n < m_buttonList.Count; n++)
        {
            if (n < q.options.Count)
            {
                m_buttonList[n].Construct(q.options[n], callback);
                m_buttonList[n].gameObject.SetActive(true);
            }
            else
            {
                m_buttonList[n].gameObject.SetActive(false);
            }
        }
    }

    private void HideButtons()
    {
        foreach (OptionButton button in m_buttonList)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ClearInput()
    {
        m_answerInputField.text = "";
    }

    public void FocusInput()
    {
        m_answerInputField.Select();
        m_answerInputField.ActivateInputField();
    }

    private void OnEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            textCallback?.Invoke(text);
        }
    }
}
