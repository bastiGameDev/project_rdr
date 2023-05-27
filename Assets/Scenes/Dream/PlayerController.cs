using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2.8f; // ��������� �������� ������������ ������
    public float maxSpeed = 4f; // ������������ �������� ������������ ������
    public float accelerationTime = 1f; // ����� ��������� �� ������������ ��������
    public float rotationSpeed = 1f; // �������� �������� ������

    public float maxVerticalAngle = 90f; // ������������ ���� ������������� ��������
    public float minVerticalAngle = -90f; // ����������� ���� ������������� ��������

    public float cameraShakeAmount = 0.1f; // �������� ������ ������ ��� ��������
    public float cameraShakeDamping = 3f; // ��������� ������ ������
    public float cameraShakeSmoothTime = 0.1f; // ��������� ������ ������ ������

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
        cameraTransform = transform.Find("Camera"); // ���� ������ ������ ������
        Cursor.lockState = CursorLockMode.Locked; // ��������� � �������� ������

        cameraOriginalPosition = cameraTransform.localPosition;
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
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref speedSmoothVelocity, accelerationTime);
        }

        movement *= currentSpeed * Time.deltaTime;

        // ���������� ������ ������
        float shakeAmountX = Mathf.Sin(Time.time * currentSpeed) * cameraShakeAmount;
        float shakeAmountZ = Mathf.Cos(Time.time * currentSpeed) * cameraShakeAmount;
        cameraShakeOffset = Vector3.SmoothDamp(cameraShakeOffset, new Vector3(shakeAmountX, 0f, shakeAmountZ), ref cameraShakeVelocity, cameraShakeSmoothTime);

        movement += cameraShakeOffset;
        characterController.Move(movement);

        // ���������� ������ ������
        cameraTransform.localPosition = cameraOriginalPosition + cameraShakeOffset;
    }
}
