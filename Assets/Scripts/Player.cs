using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  private const int STATE_IDLE = 0;
  private const int STATE_MOVE = 1;

  public float moveSpeed;
  public Color mainColour;

  private Rigidbody2D rBody;
  private Collider2D col;
  private int state;
  
  // Use this for initialization
  void Start() {
    rBody = GetComponent<Rigidbody2D>();
    GetComponent<Renderer>().material.color = mainColour;
    col = GetComponent<Collider2D>();
    state = STATE_IDLE;
  }

  // FixedUpdate is called each physics time step. It is called at a set time interval.
  void FixedUpdate() {

    //if (state == STATE_IDLE) {
      //if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) {
        //StartCoroutine("move");
      //} else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0) {
        //StartCoroutine("move");
      //}
    //}
    //
    
    if ((int)Input.GetAxisRaw("Horizontal") == 0 && (int)Input.GetAxisRaw("Vertical") == 0) { return; }

    Vector2 newPosition = transform.position;
    newPosition.x += (int)Input.GetAxisRaw("Horizontal") * moveSpeed;
    newPosition.y += (int)Input.GetAxisRaw("Vertical") * moveSpeed;

    Vector2 lineCheck = transform.position;
    lineCheck.x += (GetComponent<Renderer>().bounds.extents.x + moveSpeed) * Input.GetAxisRaw("Horizontal");
    lineCheck.y += (GetComponent<Renderer>().bounds.extents.y + moveSpeed) * Input.GetAxisRaw("Vertical"); 

    col.enabled = false;
    RaycastHit2D hit = Physics2D.Linecast(transform.position, lineCheck);
    col.enabled = true;

    if (hit.transform == null) {
      Debug.DrawLine(transform.position, lineCheck, Color.blue);
      rBody.MovePosition(newPosition);
    } else {
      Debug.DrawLine(transform.position, lineCheck, Color.red);
    }
  }

  void Update() {
  }

  IEnumerator move() {
    int i = 0;
    state = STATE_MOVE;

    while (i < 10) {
      i++;
      yield return new WaitForSeconds(0.1f);
    }

    state = STATE_IDLE;
  }
}
