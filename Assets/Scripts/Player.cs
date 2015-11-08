﻿using UnityEngine;
using System.Collections;

public class Player : BaseEntity {

  protected override void action() {

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
}
