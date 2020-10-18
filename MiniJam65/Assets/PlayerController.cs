using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float horizontalInput;
	private float verticalInput;
	private float finalSpeed;
	private bool isRunning;
	private bool canRun = true;
	private float canRunTimer;

	public float speed = 5.0f;
	public float runningSpeedMultiplier = 2.0f;
	public float runningDebounce = 3.0f;
	public float stamina = 10.0f;
	public bool hasFirstKeycard = false;
	public bool hasPassword = false;
	public bool hasEye = false;


	private Transform playerTransform;

	void Start()
    {
		playerTransform = gameObject.GetComponent<Transform>();
		canRunTimer = runningDebounce;
    }

	void Update()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		isRunning = Input.GetKey(KeyCode.LeftShift);

		if (stamina <= 0)
		{
			canRun = false;
		}

		if (isRunning && canRun)
		{
			finalSpeed = speed * runningSpeedMultiplier;
			stamina -= Time.deltaTime;
			Debug.Log("Correndo");
		}
		else
        {
			if (!canRun && canRunTimer > 0)
			{
				canRunTimer -= Time.deltaTime;
			}
			else if (canRunTimer <= 0)
			{
				canRun = true;
				canRunTimer = runningDebounce;
			}

			finalSpeed = speed;
			stamina += Time.deltaTime;
			Debug.Log("Andando");
		}

		playerTransform.Translate(new Vector3(horizontalInput, 0, verticalInput).normalized * finalSpeed * Time.deltaTime);
	}
}
