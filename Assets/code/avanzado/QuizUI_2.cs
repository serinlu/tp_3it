using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI_2 : MonoBehaviour
{
    [SerializeField] private Text m_question = null;
    [SerializeField] private List<OptionButton_2> m_buttonList = null;

    public void Construct(Question_2 q, Action<OptionButton_2> callback)
    {
        m_question.text = q.Text;

        for (int n = 0; n < m_buttonList.Count; n++)
        {
            m_buttonList[n].Construct(q.Options[n], callback);
        }
    }
}
