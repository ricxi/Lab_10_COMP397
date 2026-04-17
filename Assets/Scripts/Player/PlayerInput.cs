using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;
// using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 20.0f;
    [SerializeField] private float gravity = -8.0f;
    [SerializeField] private float rotationSpeed = 17.0f;
    [SerializeField] private float mouseSensY = 17.0f;
    [SerializeField] private float mobileScale = 10.0f;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;

    private Vector3 velocity;
    private float camXRotation;

    private InputAction move;
    private InputAction look;
    private InputAction jump;

    private void OnValidate() => this.ValidateRefs();
    
    private void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        jump = InputSystem.actions.FindAction("Player/Jump");

#if !UNITY_ANDROID
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    private void OnEnable()
    {
        jump.started += Jump;
    }

    private void OnDisable()
    {
        jump.started -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        AudioController.Instance.PlayJumpSFX();
        EventChannelManager.Instance.voidEvent.RaiseEvent();
    }

    private void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;

        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;

        controller.Move(movement);

#if UNITY_ANDROID
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * mobileScale * Time.deltaTime);
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * rotationSpeed * -1;
#else
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
#endif

        camXRotation = Mathf.Clamp(camXRotation, -80f, 50f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

	public void ChangeMouseSensibility(float value)
	{
		Debug.Log($"Value changed - {value}");
		mouseSensY = value;
		rotationSpeed = value;
	}
}
