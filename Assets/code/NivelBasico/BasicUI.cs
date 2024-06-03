using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicUI : MonoBehaviour
{
    [SerializeField] private Text m_question = null;
    [SerializeField] private List<OptionButtonNB> m_buttonList = null;

    public void Construct(QuestionNB q, Action<OptionButtonNB> callback)
    {
        m_question.text = q.text;
        for (int n = 0; n < m_buttonList.Count; n++)
        {
            m_buttonList[n].Construct(q.options[n], callback);
        }
    }
}
