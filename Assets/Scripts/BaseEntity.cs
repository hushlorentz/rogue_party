using UnityEngine;
using System.Collections;

public abstract class BaseEntity : MonoBehaviour {

  public interface EntityListener {
    void entityFinished();
  }

  public float moveStep = 0.1f;
  public Color mainColour;
  public bool isActive;
  public float waitTime = 0.0f;

  protected const int STATE_IDLE = 0;
  protected const int STATE_MOVE = 1;
  protected GameManager gameManager;
  protected Rigidbody2D rBody;
  protected Collider2D col;
  public int state;
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

  protected IEnumerator moveTo(Vector2 dest, float waitTime = 0.0f) {
    state = STATE_MOVE;

    yield return new WaitForSeconds(waitTime);

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

  protected bool hasCollision(Vector2 pos, Vector2 dest, out RaycastHit2D hit, int layer = Physics2D.DefaultRaycastLayers) {
    col.enabled = false;
    hit = Physics2D.Linecast(pos, dest, layer);
    col.enabled = true;

    return hit.transform != null;
  }
}
