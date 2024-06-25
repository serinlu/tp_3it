using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestionType
{
    MultipleChoice,
    TextInput
}

public class Question : MonoBehaviour
{
    public string text = null;
    public Sprite imagenSprite = null;
    public List<Option> options = null; // Solo para preguntas de opción múltiple
    public string correctAnswer = null; // Solo para preguntas de entrada de texto
    public QuestionType questionType; // Tipo de pregunta
    public Vector2 imageSize = new Vector2(100, 100); // Tamaño predeterminado de la imagen

}

