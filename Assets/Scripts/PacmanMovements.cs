using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovements : MonoBehaviour {

    private float moveX, moveY;
    private float moveSpeed = 1f;

    private SpriteRenderer sprite;
    private Rigidbody2D rigidBody;
    [SerializeField] private GameObject rightLimit;
    [SerializeField] private GameObject leftLimit;

    // Start is called before the first frame update
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update() {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // Invert flip position
        this.Flip();
    }


    // Invert sprites
    private void Flip() {
        float angle;
        if (moveX != 0) {
            angle = moveX > 0 ? 0f : 180f;
            SetRotation(angle);
        } else if (moveY != 0) {
            angle = moveY > 0 ? 90f : 270;
            SetRotation(angle);
        } 
    }


    private void SetRotation(float angle) {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    private void FixedUpdate(){
        if (moveX != 0) {
            rigidBody.velocity = new Vector2(moveX * moveSpeed, 0);
        } else if (moveY != 0) {
            rigidBody.velocity = new Vector2(0, moveY * moveSpeed);
        } 
    }


    private void OnTriggerEnter2D(Collider2D other) {
        float y = gameObject.transform.position.y;

        if (other.gameObject.CompareTag("Right")) {
            float x = leftLimit.transform.position.x;
            gameObject.transform.position = new Vector3(x+0.2f, y, 0f);
        }

        else if (other.gameObject.CompareTag("Left")) {
            float x = rightLimit.transform.position.x;
            gameObject.transform.position = new Vector3(x-0.1f, y, 0f);
        }
    }
}