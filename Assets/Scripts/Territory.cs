using UnityEngine;

public class Territory : MonoBehaviour
{
    private Renderer rend;

    private Color normalColor;
    private Color selectedColor = Color.yellow;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        normalColor = rend.material.color;
    }

    public void Select()
    {
        rend.material.color = selectedColor;
    }

    public void Deselect()
    {
        rend.material.color = normalColor;
    }
}