using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizDB2 : MonoBehaviour
{
    [SerializeField] private List<Preguntas2> m_preguntasList = null;

    private List<Preguntas2> m_backup = null;
    private void Awake()
    {
        m_backup = m_preguntasList.ToList();
    }    
    public Preguntas2 GetRandom(bool remove = true) {
        if (m_preguntasList.Count == 0)
            RestoreBackup();
        
        int index = Random.Range(0, m_preguntasList.Count);

        if (!remove)
            return m_preguntasList[index];

        Preguntas2 q = m_preguntasList[index];   
        m_preguntasList.RemoveAt(index);

        return q;   
    }
    private void RestoreBackup()
    { 
        m_preguntasList = m_backup.ToList(); 
    }
}
