using UnityEngine;
using System.Collections;

public class Player : BaseEntity {

  protected override void action() {

    Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    inputVector.Normalize();
    RaycastHit2D hit;

    if (state == STATE_IDLE) {
      Vector2 newPosition = (Vector2)transform.position + inputVector * moveSpeed * Time.deltaTime;

      Vector2 checkPosition = (Vector2)transform.position + inputVector * col.bounds.extents.x;
      Debug.DrawLine(transform.position, checkPosition, Color.yellow);

      //check for collision
      //if (!hasCircleCollision(transform.position, col.bounds.extents.x, inputVector, moveSpeed * Time.deltaTime, out hit)) {
        rBody.MovePosition(newPosition); 
      //}
    }
  }
}
