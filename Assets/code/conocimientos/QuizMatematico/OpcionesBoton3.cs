using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class OpcionesBoton3 : MonoBehaviour
{
    private Text m_text = null;
    private Button m_button = null;
    private Image m_image = null;
    private Color m_originalColor = Color.black;

    public Opcion3 Opcion { get; set; }

    public void Awake()
    {
        m_button = GetComponent<Button>();
        m_image = GetComponent<Image>();
        m_text = transform.GetChild(0).GetComponent<Text>();

        m_originalColor = m_image.color;
    }

    public void Construct(Opcion3 opcion, Action<OpcionesBoton3> callback)
    {
        m_text.text = opcion.text;

        m_button.OnDeselect(null);
        m_button.onClick.RemoveAllListeners();

        m_button.interactable = true;
        m_image.color = m_originalColor;

        Opcion = opcion;

        m_button.onClick.AddListener(delegate 
        {
            callback(this);
        });
    }

    public void SetColor(Color c)
    {
        m_image.color = c;
    }
   
}
