using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI_3 : MonoBehaviour
{
    [SerializeField] private Text m_question = null;
    [SerializeField] private List<OptionButton_3> m_buttonList = null;

    public void Construct(Question_3 q, Action<OptionButton_3> callback)
    {
        m_question.text = q.Text;

        for(int n = 0; n < m_buttonList.Count; n++)
        {
            m_buttonList[n].Construct(q.Options[n], callback);
        }
    }
}
