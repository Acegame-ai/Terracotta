using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField]private MobileJoystick playerJoystick;

    [Header("Settings")]
    [SerializeField]private float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = playerJoystick.GetMoveVector() * speed * Time.deltaTime;
    }
}
