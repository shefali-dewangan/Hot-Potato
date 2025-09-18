using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMushroom : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private float initialY;

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.y = initialY;
            // Move object, taking into account original offset.
            transform.position = newPosition;
        }
    }

    private void OnMouseDown()
    {
        // Record the difference between the objects centre, and the clicked point on the camera plane.
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;

        initialY = transform.position.y;
    }

    private void OnMouseUp()
    {
        // Stop dragging.
        dragging = false;
    }
}