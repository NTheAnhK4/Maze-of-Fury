
using UnityEngine;

public class PlayerMovement : ChildBehavior
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 3f;
    protected override void LoadComponentInParent()
    {
        base.LoadComponentInParent();
        player = transform.parent;
    }

    public void Move(Vector3 direction, float speed)
    {
        player.transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void Update()
    {
        Vector3 direction = InputManager.Instance.GetDirection();
        Move(direction,moveSpeed);
    }
}
