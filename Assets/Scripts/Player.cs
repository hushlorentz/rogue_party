using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  private const int STATE_IDLE = 0;
  private const int STATE_MOVE = 1;

  public float moveStep;
  public Color mainColour;

  private GameManager gameManager;
  private Rigidbody2D rBody;
  private Collider2D col;
  private int state;
  
  // Use this for initialization
  void Start() {
    rBody = GetComponent<Rigidbody2D>();
    GetComponent<Renderer>().material.color = mainColour;
    col = GetComponent<Collider2D>();
    state = STATE_IDLE;
    gameManager = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>();
  }

  // FixedUpdate is called each physics time step. It is called at a set time interval.
  void FixedUpdate() {

    int xMove = (int)Input.GetAxisRaw("Horizontal");
    int yMove = (int)Input.GetAxisRaw("Vertical");

    if (xMove != 0 || yMove != 0) {
      if (state == STATE_IDLE) {
        //get new position, then start the move coroutine
        if (xMove != 0) {
          yMove = 0; //no diagonal movement
        }

        Vector2 newPosition = transform.position;
        newPosition.x += xMove * gameManager.tileWidth;
        newPosition.y += yMove * gameManager.tileHeight;

        //check for collision
        col.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, newPosition);
        col.enabled = true;

        if (hit.transform == null) {
          StartCoroutine(moveTo(newPosition));
        }
      }
    }
  }

  IEnumerator moveTo(Vector2 dest) {
    state = STATE_MOVE;

    while ((dest - (Vector2)transform.position).sqrMagnitude > float.Epsilon) {
      Vector2 newPosition = Vector2.MoveTowards(transform.position, dest, moveStep);
      rBody.MovePosition(newPosition);
      yield return null;
    }

    rBody.MovePosition(dest);

    state = STATE_IDLE;
  }
}
