using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

  public GameObject wallObject;
  public int tileWidth = 1;
  public int tileHeight = 1;
  public int mapWidth = 20;
  public int mapHeight = 20;

  // Use this for initialization
  void Start () {

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
  }

  // Update is called once per frame
  void Update () {

  }
}
