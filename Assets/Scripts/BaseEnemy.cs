using UnityEngine;
using System.Collections;

public class BaseEnemy : BaseEntity {

  public GameObject target;
  public GameObject player;
  public int range;
  public LayerMask wallLayer;
  public float wanderDistance = 1.0f;

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
          newPosition.x -= wanderDistance;
          break;
        case 1:
          newPosition.x += wanderDistance;
          break;
        case 2:
          newPosition.y -= wanderDistance;
          break;
        case 3:
          newPosition.y += wanderDistance;
          break;
      }

      if (!hasCollision(transform.position, newPosition, out hit, wallLayer)) {
        StartCoroutine(moveTo(newPosition, waitTime));
      }
    }
  }

  private void handleTargetAction() {
    RaycastHit2D hit;

    if (distanceToPlayer().sqrMagnitude > rangeSquared || hasCollision(transform.position, player.transform.position, out hit, wallLayer)) {
      target = null;
      finishTurn();
    } else {
      Debug.DrawLine(transform.position, player.transform.position, Color.red);
      int xMove = 0;
      int yMove = 0;
      Vector2 diff = distanceToPlayer();

      if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) {
        xMove = target.transform.position.x > transform.position.x ? 1 : -1;
      } else {
        yMove = target.transform.position.y > transform.position.y ? 1 : -1;
      }

      Vector2 newPosition = transform.position;
      newPosition.x += xMove * wanderDistance;
      newPosition.y += yMove * wanderDistance;

      //check for collision
      if (!hasCollision(transform.position, newPosition, out hit)) {
        StartCoroutine(moveTo(newPosition));
      } else {
        finishTurn();
      }
    }
  }

  private Vector2 distanceToPlayer() {
    return player.transform.position - transform.position;
  }
}
