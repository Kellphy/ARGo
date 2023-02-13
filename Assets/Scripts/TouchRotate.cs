using UnityEngine;

public class TouchRotate : MonoBehaviour
{
	float touchSensitivity = 2f;
	bool isActive = false;
	Color activeColor;
	public GameObject model;

	void Update()
	{
		if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.transform.CompareTag("Alphabet"))
				{
					isActive = !isActive;
				}
			}
		}

		if (isActive)
		{
			activeColor = Color.green;
			if (Input.touchCount == 1)
			{
				var screenTouch = Input.GetTouch(0);
				if (screenTouch.phase == TouchPhase.Moved)
				{
					model.transform.Rotate(
						screenTouch.deltaPosition.y * touchSensitivity * Time.deltaTime,
						-screenTouch.deltaPosition.x * touchSensitivity * Time.deltaTime,
						0,
						Space.World); //rotate
					//model.transform.Translate(directionSpeed * Time.deltaTime, 0, 0, Space.World);  //move 
				}

				if (screenTouch.phase == TouchPhase.Ended)
				{
					isActive = false;
				}
			}
		}
		else
		{
			activeColor = Color.white;
		}

		model.GetComponent<MeshRenderer>().material.color = activeColor;
	}
}
