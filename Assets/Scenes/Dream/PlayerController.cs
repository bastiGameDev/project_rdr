using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f; // �������� ������������ ������
    public float rotationSpeed = 3f; // �������� �������� ������

    public float maxVerticalAngle = 90f; // ������������ ���� ������������� ��������
    public float minVerticalAngle = -90f; // ����������� ���� ������������� ��������

    private CharacterController characterController;
    private Transform cameraTransform;

    private float verticalRotation = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked; // ��������� � �������� ������
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"); // �������� ���� �� �����������
        float verticalMovement = Input.GetAxis("Vertical"); // �������� ���� �� ���������
        float mouseX = Input.GetAxis("Mouse X"); // �������� ���� � ���� �� �����������
        float mouseY = Input.GetAxis("Mouse Y"); // �������� ���� � ���� �� ���������

        // �������� �� �����������
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);

        // �������� �� ���������
        verticalRotation -= mouseY * rotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // ����������� ������
        Vector3 movement = (transform.forward * verticalMovement + transform.right * horizontalMovement) * movementSpeed * Time.deltaTime;
        characterController.Move(movement);
    }
}
