using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private float timeOfShift;
    [SerializeField] private float SpeedMultiplayer;

    private void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * Horizontal + transform.forward * Vertical;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            move *= Speed * SpeedMultiplayer; 
        }
        else
        {
            move *= Speed;
        }

        Controller.Move(move * Time.deltaTime);
    }
}
