using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	public RawImage screwdriverImage;
	public RawImage keycardImage;
	public RawImage passwordImage;
	public RawImage eyeImage;

	public PlayerController playerController;

    void Update()
	{
		screwdriverImage.enabled = playerController.hasScrewDriver;
		screwdriverImage.GetComponentInChildren<Text>().enabled = playerController.hasScrewDriver;  

		keycardImage.enabled = playerController.hasFirstKeycard;
		keycardImage.GetComponentInChildren<Text>().enabled = playerController.hasFirstKeycard;

		passwordImage.enabled = playerController.hasPassword;
		passwordImage.GetComponentInChildren<Text>().enabled = playerController.hasPassword;

		eyeImage.enabled = playerController.hasEye;
		eyeImage.GetComponentInChildren<Text>().enabled = playerController.hasEye;
	}
}
