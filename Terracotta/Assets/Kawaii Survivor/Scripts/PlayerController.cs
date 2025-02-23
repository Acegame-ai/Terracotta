using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private MobileJoystick playerJoystick;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
    }

    // Update is called once per frame
    private void Update()
    {
        rb.velocity = playerJoystick.GetMoveVector() * speed * Time.deltaTime;
    }
}
