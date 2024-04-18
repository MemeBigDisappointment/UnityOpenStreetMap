using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float scrollSpeed = 1000f;
    public float rotateSpeed = 500f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Moving WASD
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= 2;
        }else if (Input.GetKeyUp(KeyCode.LeftShift)){
            moveSpeed /= 2; 
        }
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);
        //Scrolling
        var scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 scrollDirection = transform.forward * scrollInput * scrollSpeed * Time.deltaTime;
        transform.position += scrollDirection;
        //Roating on -X and Y axis
        if (Input.GetMouseButton(1))
        {
            var rotateInputX = Input.GetAxis("Mouse X");
            var rotateInputY = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, rotateInputX * rotateSpeed * Time.deltaTime);
            transform.Rotate(Vector3.left, rotateInputY * rotateSpeed * Time.deltaTime);
        }
    }
}
