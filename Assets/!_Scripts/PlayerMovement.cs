using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки")]
    public float maxRotationSpeed = 200f; // максимальная скорость вращения
    public float sensitivity = 0.5f;      // чувствительность вращения

    private bool isDragging = false;
    private float lastMouseX;

    void Update()
    {
        // Нажали — начали слежение
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
            isDragging = true;
        }

        // Отпустили — остановили
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Управляем во время зажатия
        if (isDragging)
        {
            float currentMouseX = Input.mousePosition.x;
            float deltaX = currentMouseX - lastMouseX;

            float rotationSpeed = Mathf.Clamp(deltaX * sensitivity, -maxRotationSpeed, maxRotationSpeed);
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            lastMouseX = currentMouseX;
        }
    }
}
