using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("要追蹤的物件")]
    public Transform target;
    [Header("追蹤速度"), Range(1, 100)]
    public float speed = 10;
    [Header("追蹤上限與下限：X 為下方限制，Y 為上方限制")]
    public Vector2 limit = new Vector2(0, 5);

    private void LateUpdate()
    {
        Track();
    }

    private void Track()
    {
        Vector3 pos = target.position;
        pos.z = -10;
        pos.y = Mathf.Clamp(pos.y, limit.x, limit.y);

        transform.position = Vector3.Lerp(transform.position, pos, 0.5f * Time.deltaTime * speed);
    }
}
