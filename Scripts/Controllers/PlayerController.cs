using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	public Interactible focus;
	public LayerMask movementMask;
	Camera cam;
	PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
    }

	// Update is called once per frame
	void Update()
	{
		// check if hovering over UI
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitPoint;

			if (Physics.Raycast(ray, out hitPoint, 100, movementMask))
			{
				//move to what we hit
				motor.MoveToPoint(hitPoint.point);
				//stop focusing any objects
				RemoveFocus();
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitPoint;

			if (Physics.Raycast(ray, out hitPoint, 100))
			{
				Interactible interactible = hitPoint.collider.GetComponent<Interactible>();
				//set interactible as our focus
				if(interactible != null)
				{
					SetFocus(interactible);
				}
			}
		}
	}

	void SetFocus (Interactible newFocus)
	{
		if(newFocus != focus)
		{
			if(focus != null)
				focus.OnDefocused();    //defocus previous focus
			focus = newFocus;
			motor.FollowTarget(newFocus);
		}	
		newFocus.OnFocused(transform);
	}

	void RemoveFocus ()
	{
		if (focus != null)
			focus.OnDefocused();
		focus = null;
		motor.StopFollowingTarget();
	}
}
