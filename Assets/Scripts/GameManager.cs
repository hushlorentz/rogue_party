using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour, BaseEntity.EntityListener {

  public GameObject wallObject;
  public int tileWidth = 1;
  public int tileHeight = 1;
  public int mapWidth = 20;
  public int mapHeight = 20;
  
  private Player player;
  private Enemy enemy;
  private bool isPlayersTurn;

  // Use this for initialization
  void Start() {
    int tileStartX = -mapWidth / 2;
    int tileStartY = -mapHeight / 2;

    GameObject wallContainer = new GameObject();

    for (int i = 0; i < mapHeight; i++) {
      for (int j = 0; j < mapWidth; j++) {
        if (i == 0 || j == 0 || i == mapHeight - 1 || j == mapWidth - 1) {
          Vector2 wallPosition = new Vector2(tileStartX + i * tileWidth, tileStartY + j * tileHeight);
          GameObject wall = (GameObject)Instantiate(wallObject, wallPosition, Quaternion.identity);
          wall.transform.parent = wallContainer.transform;
        }
      }
    } 

    player = GameObject.FindWithTag("Player").GetComponent<Player>();
    player.setListener(this);
    isPlayersTurn = true;

    enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    enemy.setListener(this);
  }

  void Update() {
    if (isPlayersTurn) {
      if (!player.isActive) {
        player.isActive = true;
      }
    } else {
      if (!enemy.isActive) {
        enemy.isActive = true;
      }
    }
  }

  public void entityFinished() {
    if (isPlayersTurn) {
      player.isActive = false;
      isPlayersTurn = false;
    } else {
      enemy.isActive = false;
      isPlayersTurn = true;
    }
  }
}
