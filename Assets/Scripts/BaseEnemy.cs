using UnityEngine;
using System.Collections;

public class BaseEnemy : BaseEntity {

  public GameObject target;
  public GameObject player;
  public int range;
  public LayerMask wallLayer;

  private int rangeSquared;

  // Use this for initialization
  public override void Start() {
    base.Start();

    player = GameObject.FindWithTag("Player");
    rangeSquared = range * range;
  }

  protected override void action() {
    switch (state) {
      case STATE_IDLE:
        handleIdle();
        break;
    }
  }

  private void handleIdle() {
    int xMove = 0;
    int yMove = 0;

    if (target == null) {
      handleNoTargetAction();
    } else {
      handleTargetAction();
    }
  }

  private void handleNoTargetAction() {
    RaycastHit2D hit;

    if (distanceToPlayer().sqrMagnitude <= rangeSquared && !hasCollision(transform.position, player.transform.position, out hit, wallLayer)) {
      target = player;
      finishTurn();
    } else {
      int moveDir = Random.Range(0, 4);
      Vector2 newPosition = transform.position;

      switch (moveDir) {
        case 0:
          newPosition.x -= gameManager.tileWidth;
          break;
        case 1:
          newPosition.x += gameManager.tileWidth;
          break;
        case 2:
          newPosition.y -= gameManager.tileHeight;
          break;
        case 3:
          newPosition.y += gameManager.tileHeight;
          break;
      }

      StartCoroutine(moveTo(newPosition, waitTime));
    }
  }

  private void handleTargetAction() {
    RaycastHit2D hit;

    if (distanceToPlayer().sqrMagnitude > rangeSquared || hasCollision(transform.position, player.transform.position, out hit, wallLayer)) {
      target = null;
      finishTurn();
    } else {
      Debug.DrawLine(transform.position, player.transform.position, Color.red);
      /*
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
      */
      finishTurn();
    }
  }

  private Vector2 distanceToPlayer() {
    return player.transform.position - transform.position;
  }
}
