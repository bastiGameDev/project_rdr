using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2.8f; // Начальная скорость передвижения игрока
    public float maxSpeed = 4f; // Максимальная скорость передвижения игрока
    public float accelerationTime = 1f; // Время ускорения до максимальной скорости
    public float rotationSpeed = 1f; // Скорость вращения камеры

    public float maxVerticalAngle = 90f; // Максимальный угол вертикального вращения
    public float minVerticalAngle = -90f; // Минимальный угол вертикального вращения

    public float cameraShakeAmount = 0.1f; // Величина тряски камеры при движении
    public float cameraShakeDamping = 3f; // Плавность тряски камеры
    public float cameraShakeSmoothTime = 0.1f; // Плавность начала тряски камеры

    private CharacterController characterController;
    private Transform cameraTransform;

    private float verticalRotation = 0f;
    private float currentSpeed;
    private float speedSmoothVelocity;

    private Vector3 cameraOriginalPosition;
    private Vector3 cameraShakeOffset;
    private Vector3 cameraShakeVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = transform.Find("Camera"); // Ищем камеру внутри игрока
        Cursor.lockState = CursorLockMode.Locked; // Блокируем и скрываем курсор

        cameraOriginalPosition = cameraTransform.localPosition;
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
        Vector3 movement = (transform.forward * verticalMovement + transform.right * horizontalMovement);

        if (movement.magnitude > 0f)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, maxSpeed, ref speedSmoothVelocity, accelerationTime);
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref speedSmoothVelocity, accelerationTime);
        }

        movement *= currentSpeed * Time.deltaTime;

        // Применение тряски камеры
        float shakeAmountX = Mathf.Sin(Time.time * currentSpeed) * cameraShakeAmount;
        float shakeAmountZ = Mathf.Cos(Time.time * currentSpeed) * cameraShakeAmount;
        cameraShakeOffset = Vector3.SmoothDamp(cameraShakeOffset, new Vector3(shakeAmountX, 0f, shakeAmountZ), ref cameraShakeVelocity, cameraShakeSmoothTime);

        movement += cameraShakeOffset;
        characterController.Move(movement);

        // Применение тряски камеры
        cameraTransform.localPosition = cameraOriginalPosition + cameraShakeOffset;
    }
}
