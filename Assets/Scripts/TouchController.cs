using UnityEngine;
using UnityEngine.InputSystem;

public class TouchController : MonoBehaviour
{
    [SerializeField] private float cameraDragSpeed = 0.02f;
    [SerializeField] private float dragThreshold = 20f;

    private Vector2 startInputPosition;
    private Vector2 previousInputPosition;

    private bool isDragging;

    void Update()
    {
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasPressedThisFrame)
            {
                startInputPosition = touch.position.ReadValue();
                previousInputPosition = startInputPosition;

                isDragging = false;
            }

            if (touch.press.isPressed)
            {
                Vector2 currentPosition = touch.position.ReadValue();

                Vector2 totalDelta = currentPosition - startInputPosition;

                if (!isDragging &&
                    totalDelta.magnitude > dragThreshold)
                {
                    isDragging = true;
                }

                if (isDragging)
                {
                    Vector2 delta = currentPosition - previousInputPosition;

                    MoveCamera(delta);

                    previousInputPosition = currentPosition;
                }
            }

            if (touch.press.wasReleasedThisFrame)
            {
                if (!isDragging)
                {
                    HandleTap(touch.position.ReadValue());
                }

                isDragging = false;
            }
        }

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                startInputPosition = Mouse.current.position.ReadValue();
                previousInputPosition = startInputPosition;

                isDragging = false;
            }

            if (Mouse.current.leftButton.isPressed)
            {
                Vector2 currentPosition = Mouse.current.position.ReadValue();

                Vector2 totalDelta = currentPosition - startInputPosition;

                if (!isDragging &&
                    totalDelta.magnitude > dragThreshold)
                {
                    isDragging = true;
                }

                if (isDragging)
                {
                    Vector2 delta = currentPosition - previousInputPosition;

                    MoveCamera(delta);

                    previousInputPosition = currentPosition;
                }
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                if (!isDragging)
                {
                    HandleTap(Mouse.current.position.ReadValue());
                }

                isDragging = false;
            }
        }
    }

    private void HandleTap(Vector2 screenPosition)
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

    private void MoveCamera(Vector2 delta)
    {
        Vector3 movement = new Vector3(
            -delta.x,
            0,
            -delta.y
        );

        Camera.main.transform.position +=
            movement * cameraDragSpeed;
    }
}