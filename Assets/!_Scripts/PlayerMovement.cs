using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("���������")]
    public float maxRotationSpeed = 200f; // ������������ �������� ��������
    public float sensitivity = 0.5f;      // ���������������� ��������

    private bool isDragging = false;
    private float lastMouseX;

    void Update()
    {
        // ������ � ������ ��������
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseX = Input.mousePosition.x;
            isDragging = true;
        }

        // ��������� � ����������
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ��������� �� ����� �������
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
