using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectEvent: MonoBehaviour, IPointerEnterHandler { // required interface when using the OnPointerEnter method.

	public EventSystem eventSystem;
	public GameObject selectedObject;
	private bool buttonSelected;

    public void OnPointerEnter(PointerEventData eventData)
    {
		GameObject textButton = eventData.pointerEnter;
		GameObject Button = textButton.transform.parent.gameObject;
		selectedObject = Button;

		eventSystem.SetSelectedGameObject(selectedObject);
    }

	void Start () 
	{
		eventSystem.SetSelectedGameObject(selectedObject);
		buttonSelected = true;
	}
	
	void Update () 
	{
		if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
		{
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}
	}

	private void OnDisable()
	{
		buttonSelected = false;
	}
}