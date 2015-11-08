using UnityEngine;
using System.Collections;

public abstract class BaseEntity : MonoBehaviour {

  public interface EntityListener {
    void entityFinished();
  }

  public float moveStep = 0.1f;
  public Color mainColour;
  public bool isActive;

  protected const int STATE_IDLE = 0;
  protected const int STATE_MOVE = 1;
  protected GameManager gameManager;
  protected Rigidbody2D rBody;
  protected Collider2D col;
  protected int state;
  private EntityListener listener;

  public virtual void Start() {
    state = STATE_IDLE;
    isActive = false;
    rBody = GetComponent<Rigidbody2D>();
    GetComponent<Renderer>().material.color = mainColour;
    col = GetComponent<Collider2D>();
    gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
  }

  void Update() {
    if (isActive) {
      action();
    }
  }

  public void setListener(EntityListener listener) {
    this.listener = listener;
  }

  abstract protected void action();

  protected IEnumerator moveTo(Vector2 dest) {
    state = STATE_MOVE;

    while ((dest - (Vector2)transform.position).sqrMagnitude > float.Epsilon) {
      Vector2 newPosition = Vector2.MoveTowards(transform.position, dest, moveStep * Time.deltaTime);
      rBody.MovePosition(newPosition);
      yield return null;
    }

    rBody.MovePosition(dest);
    finishTurn();
  }

  protected void finishTurn() {
    state = STATE_IDLE;

    if (listener != null) {
      listener.entityFinished();
    }
  }
}
