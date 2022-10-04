using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private Transform field;
    [SerializeField] private Vector2 size;
    [SerializeField] private Vector2 bordersSize;

    [HideInInspector]
    [SerializeField] private Vector2 sizeBoundsX, sizeBoundsY;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector2 size = this.size - bordersSize;
        Vector3 cubeSize = new Vector3(size.x, transform.localScale.y, size.y);

        Gizmos.DrawWireCube(transform.position, cubeSize);
    }

    public Vector3 GetFieldPosition(Vector2 lerpPosition)
    {
        return new Vector3(
            Mathf.Lerp(sizeBoundsX.x, sizeBoundsX.y, lerpPosition.x),
            transform.position.y,
            Mathf.Lerp(sizeBoundsY.x, sizeBoundsY.y, lerpPosition.y));
    }

    private void OnValidate()
    {
        if (field == null) return;


        field.localScale = new Vector3(this.size.x, field.localScale.y, this.size.y);

        Vector2 size = new Vector2(-this.size.x, -this.size.y);

        sizeBoundsX.x = size.x + bordersSize.x;
        sizeBoundsX.y = -size.x - bordersSize.x;

        sizeBoundsY.x = size.y + bordersSize.y;
        sizeBoundsY.y = -size.y - bordersSize.y;

        sizeBoundsX /= 2f;
        sizeBoundsY /= 2f;
    }
}
