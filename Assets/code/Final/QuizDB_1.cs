using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDB_1 : MonoBehaviour
{
    [SerializeField] private List<Question_1> m_questionList = null;

    [SerializeField] private List<Question_1> m_backup;

    private void Awake()
    {
        m_backup = m_questionList.ToList();
    }
    public Question_1 GetRandom(bool remove = true)
    {
        if (m_questionList.Count == 0) RestoreBackup();
        int index = Random.Range(0, m_questionList.Count);

        if(!remove) return m_questionList[index];

        Question_1 q = m_questionList[index];
        m_questionList.RemoveAt(index);
        return q;
    }
    private void RestoreBackup()
    {
        m_questionList = m_backup.ToList();
    }
}
