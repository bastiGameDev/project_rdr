using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f; // Скорость передвижения игрока
    public float rotationSpeed = 3f; // Скорость вращения камеры

    public float maxVerticalAngle = 90f; // Максимальный угол вертикального вращения
    public float minVerticalAngle = -90f; // Минимальный угол вертикального вращения

    private CharacterController characterController;
    private Transform cameraTransform;

    private float verticalRotation = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked; // Блокируем и скрываем курсор
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"); // Получаем ввод по горизонтали
        float verticalMovement = Input.GetAxis("Vertical"); // Получаем ввод по вертикали
        float mouseX = Input.GetAxis("Mouse X"); // Получаем ввод с мыши по горизонтали
        float mouseY = Input.GetAxis("Mouse Y"); // Получаем ввод с мыши по вертикали

        // Вращение по горизонтали
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);

        // Вращение по вертикали
        verticalRotation -= mouseY * rotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Перемещение игрока
        Vector3 movement = (transform.forward * verticalMovement + transform.right * horizontalMovement) * movementSpeed * Time.deltaTime;
        characterController.Move(movement);
    }
}
