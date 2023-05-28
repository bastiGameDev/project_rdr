using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.5f; // Начальная скорость передвижения игрока
    public float maxSpeed = 1f; // Максимальная скорость передвижения игрока
    public float accelerationTime = 2f; // Время ускорения до максимальной скорости
    public float rotationSpeed = 0.05f; // Скорость вращения камеры

    public float maxVerticalAngle = 75f; // Максимальный угол вертикального вращения
    public float minVerticalAngle = -75f; // Минимальный угол вертикального вращения

    public float cameraShakeAmount = 0.1f; // Величина тряски камеры при движении
    public float cameraShakeDamping = 5f; // Плавность тряски камеры
    public float cameraShakeSmoothTime = 10f; // Плавность начала тряски камеры

    public AudioClip footstepSound; // Звук ходьбы

    private CharacterController characterController;
    private Transform cameraTransform;
    private AudioSource audioSource;

    private float verticalRotation = 0f;
    private float currentSpeed;
    private float speedSmoothVelocity;

    private Vector3 cameraOriginalPosition;
    private Vector3 cameraShakeOffset;
    private Vector3 cameraShakeVelocity;

    private bool isMoving = false; // Флаг движения игрока
    private float stopRotationDelay = 2f; // Задержка перед применением затухания вращения камеры
    private float stopRotationTimer = 0f; // Таймер задержки

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = transform.Find("Camera"); // Ищем камеру внутри игрока
        Cursor.lockState = CursorLockMode.Locked; // Блокируем и скрываем курсор

        cameraOriginalPosition = cameraTransform.localPosition;

        audioSource = gameObject.AddComponent<AudioSource>(); // Добавляем компонент AudioSource
        audioSource.spatialBlend = 1f; // Устанавливаем пространственную смесь на 3D
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
            isMoving = true;
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref speedSmoothVelocity, accelerationTime);
            isMoving = false;
        }

        // Применение тряски камеры
        float shakeAmountX = Mathf.Sin(Time.time * currentSpeed) * cameraShakeAmount;
        float shakeAmountZ = Mathf.Cos(Time.time * currentSpeed) * cameraShakeAmount;
        cameraShakeOffset = Vector3.SmoothDamp(cameraShakeOffset, new Vector3(shakeAmountX, 0f, shakeAmountZ), ref cameraShakeVelocity, cameraShakeSmoothTime);

        // Применение физики падения и перемещение игрока
        movement *= currentSpeed;
        movement.y = Physics.gravity.y; // Применение гравитации
        characterController.SimpleMove(movement);

        // Применение затухания вращения камеры при остановке
        if (!isMoving)
        {
            stopRotationTimer += Time.deltaTime;

            if (stopRotationTimer >= stopRotationDelay)
            {
                Quaternion targetRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
                cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, targetRotation, 1f - Mathf.Exp(-rotationSpeed * Time.deltaTime));
            }
        }
        else
        {
            stopRotationTimer = 0f;
        }

        // Применение тряски камеры
        cameraTransform.localPosition = cameraOriginalPosition + cameraShakeOffset;

        // Проигрывание звука ходьбы
        if (movement.magnitude > 0f && currentSpeed > 0.1f && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(footstepSound);
        }
        else if (!isMoving || currentSpeed <= 2f)
        {
            audioSource.Stop();
        }
    }
}
