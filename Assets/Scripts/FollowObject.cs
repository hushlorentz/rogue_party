using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

  public GameObject followObject;

  // Use this for initialization
  void Start () {
    
  }

  // Update is called once per frame
  void Update () {
    if (followObject) {
      Vector3 newPos = followObject.transform.position; 
      newPos.z = -10;
      transform.position = newPos;
    }
  }
}
