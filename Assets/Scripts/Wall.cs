using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

  public Color mainColour;

  // Use this for initialization
  void Start () {
    GetComponent<Renderer>().material.color = mainColour;

  }

  // Update is called once per frame
  void Update () {

  }
}
