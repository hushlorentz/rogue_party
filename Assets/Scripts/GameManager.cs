using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour, BaseEntity.EntityListener {

  public bool turnBasedMovement = false;
  public MapGenerator mapGenerator;
  
  private Player player;
  private Enemy enemy;
  private bool isPlayersTurn;

  // Use this for initialization
  void Start() {
    player = GameObject.FindWithTag("Player").GetComponent<Player>();
    player.setListener(this);
    isPlayersTurn = true;

    enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    enemy.setListener(this);
  }

  void Update() {
    if (turnBasedMovement) {
      if (isPlayersTurn) {
        if (!player.isActive) {
          player.isActive = true;
        }
      } else {
        if (!enemy.isActive) {
          enemy.isActive = true;
        }
      }
    } else {
      player.isActive = true;
      enemy.isActive = true;
    }
  }

  public void entityFinished() {
    if (turnBasedMovement) {
      if (isPlayersTurn) {
        player.isActive = false;
        isPlayersTurn = false;
      } else {
        enemy.isActive = false;
        isPlayersTurn = true;
      }
    }
  }
}
