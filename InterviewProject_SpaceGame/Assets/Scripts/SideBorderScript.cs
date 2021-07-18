using UnityEngine;

public class SideBorderScript : MonoBehaviour
{

    [SerializeField] private float mirrorPosition_x, mirrorPosition_y;

    [SerializeField] private bool upSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (upSide)
            TriggerEnter(collision, collision.gameObject.transform.position.x, mirrorPosition_y);
        else
            TriggerEnter(collision, mirrorPosition_x, collision.gameObject.transform.position.y);
    }

    private void TriggerEnter(Collider2D collision, float x, float y)
    {
        collision.gameObject.transform.position = new Vector2(x, y);
    }
}
