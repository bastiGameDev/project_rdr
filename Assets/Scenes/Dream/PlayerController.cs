using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.5f; // ��������� �������� ������������ ������
    public float maxSpeed = 1f; // ������������ �������� ������������ ������
    public float accelerationTime = 2f; // ����� ��������� �� ������������ ��������
    public float rotationSpeed = 0.05f; // �������� �������� ������

    public float maxVerticalAngle = 75f; // ������������ ���� ������������� ��������
    public float minVerticalAngle = -75f; // ����������� ���� ������������� ��������

    public float cameraShakeAmount = 0.1f; // �������� ������ ������ ��� ��������
    public float cameraShakeDamping = 5f; // ��������� ������ ������
    public float cameraShakeSmoothTime = 10f; // ��������� ������ ������ ������

    public AudioClip footstepSound; // ���� ������

    private CharacterController characterController;
    private Transform cameraTransform;
    private AudioSource audioSource;

    private float verticalRotation = 0f;
    private float currentSpeed;
    private float speedSmoothVelocity;

    private Vector3 cameraOriginalPosition;
    private Vector3 cameraShakeOffset;
    private Vector3 cameraShakeVelocity;

    private bool isMoving = false; // ���� �������� ������
    private float stopRotationDelay = 2f; // �������� ����� ����������� ��������� �������� ������
    private float stopRotationTimer = 0f; // ������ ��������

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = transform.Find("Camera"); // ���� ������ ������ ������
        Cursor.lockState = CursorLockMode.Locked; // ��������� � �������� ������

        cameraOriginalPosition = cameraTransform.localPosition;

        audioSource = gameObject.AddComponent<AudioSource>(); // ��������� ��������� AudioSource
        audioSource.spatialBlend = 1f; // ������������� ���������������� ����� �� 3D
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

        // ���������� ������ ������
        float shakeAmountX = Mathf.Sin(Time.time * currentSpeed) * cameraShakeAmount;
        float shakeAmountZ = Mathf.Cos(Time.time * currentSpeed) * cameraShakeAmount;
        cameraShakeOffset = Vector3.SmoothDamp(cameraShakeOffset, new Vector3(shakeAmountX, 0f, shakeAmountZ), ref cameraShakeVelocity, cameraShakeSmoothTime);

        // ���������� ������ ������� � ����������� ������
        movement *= currentSpeed;
        movement.y = Physics.gravity.y; // ���������� ����������
        characterController.SimpleMove(movement);

        // ���������� ��������� �������� ������ ��� ���������
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

        // ���������� ������ ������
        cameraTransform.localPosition = cameraOriginalPosition + cameraShakeOffset;

        // ������������ ����� ������
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
