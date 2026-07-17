using UnityEngine;

public class FlyState : StateBase
{
    public GameObject Wing;
    private IsGroundControl isGround;
    private Vector3 currentVelocity;
    float yVelocity = 0f;
    float gravity = -3f;
    SphereCollider sphereCollider;

    private void OnEnable()
    {
        animator.SetBool("Jump", true);
        rbody.mass = 0.3f;
        rbody.AddForce(Vector3.up * 200);
        isGround = GetComponentInChildren<IsGroundControl>();
        Wing.SetActive(true);
        sphereCollider = GetComponentInChildren<SphereCollider>();
        sphereCollider.radius = 0.3f;
    }

    private void OnDisable()
    {
        animator.SetBool("Jump", false);
        rbody.mass = 1;
        Wing.SetActive(false);
        sphereCollider.radius = 0.02f;
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log(isGround.IsGround);

        yVelocity += gravity * Time.deltaTime;
        transform.position += Vector3.up * yVelocity * Time.deltaTime;

        if (isGround.IsGround)
        {
            yVelocity = 0f;
            ChangeState<IdleState>();
        }

        if (InputManager.Instance.Move != Vector2.zero)
        {
            var dir = Quaternion.LookRotation(GetComponentInChildren<Camera>().transform.forward)
                * new Vector3(InputManager.Instance.Move.x, 0, InputManager.Instance.Move.y).normalized;

            GetComponentInChildren<Animator>().transform.rotation = Quaternion.LookRotation(dir);
            transform.position += dir * 4 * Time.deltaTime;
        }
    }

    protected void LateUpdate()
    {
    }
}
