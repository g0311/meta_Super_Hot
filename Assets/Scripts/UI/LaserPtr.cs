using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserPtr : MonoBehaviour
{
    private LineRenderer layser;
    private RaycastHit Collided_object;
    private GameObject currentObject;
    public float raycastDistance = 100f;

    // Start is called before the first frame update
    void Start()
    {
        layser = this.gameObject.AddComponent<LineRenderer>();

        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);

        layser.material = material;
        layser.positionCount = 2;

        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
    }

    void Update()
    {
        layser.SetPosition(0, transform.position);
        Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);
        if (Physics.Raycast(transform.position, transform.forward, out Collided_object, raycastDistance))
        {
            layser.SetPosition(1, Collided_object.point);

            if (Collided_object.collider.gameObject.CompareTag("Button"))
            {
                Debug.Log("Button!!");
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    Collided_object.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    Collided_object.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                    currentObject = Collided_object.collider.gameObject;
                }
            }
        }
        else
        {
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));
            if (currentObject != null)
            {
                currentObject.GetComponent<Button>().OnPointerExit(null);
                currentObject = null;
            }
        }
    }
    private void LateUpdate()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(0, 195, 255, 0.5f);
        }
        else if (OVRInput.GetUp(OVRInput.Button.One))
        {
            layser.material.color = new Color(255, 0, 0, 0.5f);
        }
    }

}
