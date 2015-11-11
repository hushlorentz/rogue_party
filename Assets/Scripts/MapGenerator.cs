using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

  public int mapWidth = 50;
  public int mapHeight = 50;
  public GameObject wallObject;
  public int tileWidth = 1;
  public int tileHeight = 1;
  public string seed;
  public bool useRandomSeed;
  public int smoothPasses = 5;

  [Range(0,100)]
  public int randomFillPercent;

  private int[,] map;
  private GameObject wallParent;

  void Start() {
    map = new int[mapWidth, mapHeight];
    GenerateMap();
    DrawMap();
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      DestroyMap();
      GenerateMap();
      DrawMap();
    }
  }

  void DestroyMap() {
    Destroy(wallParent);
  }

  void GenerateMap() {
    if (useRandomSeed) {
      seed = Time.time.ToString();
    }

    System.Random pseudoRandom = new System.Random(seed.GetHashCode());

    for (int i = 0; i < mapHeight; i++) {
      for (int j = 0; j < mapWidth; j++) {
        if (i == 0 || j == 0 || i == mapHeight - 1 || j == mapWidth - 1) {
          map[j,i] = 1;
        } else {
          map[j,i] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
        }
      }
    } 

    for (int i = 0; i < smoothPasses; i ++) {
      SmoothMap();
    }
  }

  void SmoothMap() {
    for (int i = 0; i < mapHeight; i++) {
      for (int j = 0; j < mapWidth; j++) {
        int neighbourWallTiles = GetSurroundingWallCount(j,i);

        if (neighbourWallTiles > 4) {
          map[j,i] = 1;
        } else if (neighbourWallTiles < 4) {
          map[j,i] = 0;
        }
      }
    }
  }

  int GetSurroundingWallCount(int gridX, int gridY) {
    int wallCount = 0;
    for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
      for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
        if (neighbourX >= 0 && neighbourX < mapWidth && neighbourY >= 0 && neighbourY < mapHeight) {
          if (neighbourX != gridX || neighbourY != gridY) {
            wallCount += map[neighbourX,neighbourY];
          }
        }
        else {
          wallCount ++;
        }
      }
    }

    return wallCount;
  }

  void DrawMap() {
    wallParent = new GameObject("Walls");

    for (int i = 0; i < mapHeight; i++) {
      for (int j = 0; j < mapWidth; j++) {
        if (map[j, i] == 1) {
          Vector3 position = new Vector3();
          position.x = -mapWidth / 2 + j * tileWidth;
          position.y = -mapHeight / 2 + i * tileHeight;
          GameObject gameObject = (GameObject)Instantiate(wallObject, position, Quaternion.identity);
          gameObject.transform.parent = wallParent.transform;
        }
      }
    }
  }
}
