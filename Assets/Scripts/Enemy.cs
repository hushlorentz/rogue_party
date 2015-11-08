using UnityEngine;
using System.Collections;

public class Enemy : BaseEntity {

  public GameObject target;

  // Use this for initialization
  public override void Start() {
    base.Start();
    if (target == null) {
      target = GameObject.FindWithTag("Player");
    }
  }

  protected override void action() {
    Debug.DrawLine(transform.position, target.transform.position, Color.green);

    int xMove = 0;
    int yMove = 0;

    Vector2 diff = target.transform.position - transform.position;

    if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) {
      xMove = target.transform.position.x > transform.position.x ? 1 : -1;
    } else {
      yMove = target.transform.position.y > transform.position.y ? 1 : -1;
    }

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
        } else {
          finishTurn();
        }
      }
    } else {
      finishTurn();
    }
  }
}
