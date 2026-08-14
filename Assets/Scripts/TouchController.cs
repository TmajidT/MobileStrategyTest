using UnityEngine;
using UnityEngine.InputSystem;

public class TouchController : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(cubePrefab, hit.point, Quaternion.identity);
            }
        }
    }
}