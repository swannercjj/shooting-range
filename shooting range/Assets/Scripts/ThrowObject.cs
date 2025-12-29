using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowObject : MonoBehaviour
{
	[Header("References")]
	public Transform cam;
	public Transform attackPoint;
	public GameObject objectToThrow;

	[Header("Settings")]
	public int totalThrows;
	public float throwCooldown;

	[Header("Throwing")]
	public KeyCode throwKey = KeyCode.Mouse0;
	public float throwForce;
	public float throwUpwardForce;

	bool readyToThrow = true;

	void Update()
	{
		if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
		{
			Throw();
		}
	}

	private void Throw()
	{
		readyToThrow = false;

		GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

		Vector3 throwDirection = cam.transform.forward;
		RaycastHit hit;

		if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
		{
			throwDirection = (hit.point - attackPoint.position).normalized;
		}

		Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

		Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
		projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

		totalThrows--;
		Invoke(nameof(ResetThrow), throwCooldown);

	}

	private void ResetThrow() 
	{
		readyToThrow = true;
	}
}
