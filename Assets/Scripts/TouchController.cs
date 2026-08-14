using UnityEngine;
using UnityEngine.InputSystem;

public class TouchController : MonoBehaviour
{
    void Update()
    {
        // Touch
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasPressedThisFrame)
            {
                Vector2 touchPosition = touch.position.ReadValue();
                HandleInput(touchPosition);
            }
        }

        // Mouse
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            HandleInput(mousePosition);
        }
    }

    private void HandleInput(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Territory territory = hit.collider.GetComponent<Territory>();

            if (territory != null)
            {
                territory.Select();
            }
        }
    }
}