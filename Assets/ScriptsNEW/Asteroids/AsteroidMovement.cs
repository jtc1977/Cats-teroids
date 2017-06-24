using UnityEngine;

/// <summary>
/// Contains stats and controls an asteroid.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AsteroidStatus))]
public class AsteroidMovement : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    
    [SerializeField] private float minHorizontalSpeed;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float minVerticalSpeed;
    [SerializeField] private float maxVerticalSpeed;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    #endregion

    #region Functions
    /// <summary>
    /// Prevent the horizontal speed from going beyond the bounds.
    /// </summary>
    /// <returns>The asteroid's horizontal speed.</returns>
    private float ConstrainedHorizontalSpeed()
    {
        if (rb.velocity.x > maxHorizontalSpeed)
            return maxHorizontalSpeed;
        else if (rb.velocity.x < -maxHorizontalSpeed)
            return -maxHorizontalSpeed;
        else if (rb.velocity.x < minHorizontalSpeed && rb.velocity.x >= 0)
            return minHorizontalSpeed;
        else if (rb.velocity.x > -minHorizontalSpeed && rb.velocity.x < 0)
            return -minHorizontalSpeed;
        // If the horizontal speed is within the bounds, return the asteroid's current horizontal velocity.
        return rb.velocity.x;
    }

    /// <summary>
    /// Prevent the vertical speed from going beyond the bounds. Always negative.
    /// </summary>
    /// <returns>The asteroid's vertical speed.</returns>
    private float ConstrainedVerticalSpeed()
    {
        if (rb.velocity.y > -minVerticalSpeed)
            return -minVerticalSpeed;
        else if (rb.velocity.y < -maxVerticalSpeed)
            return -maxVerticalSpeed;
        // If the vertical speed is within the bounds, return the asteroid's current vertical velocity.
        return rb.velocity.y;
    }

    /// <summary>
    /// Prevent the rotation speed from going beyond the bounds.
    /// </summary>
    /// <returns>The asteroid's rotation speed.</returns>
    private float ConstrainedRotationSpeed()
    {
        if (rb.angularVelocity > maxRotationSpeed)
            return maxRotationSpeed;
        else if (rb.angularVelocity < -maxRotationSpeed)
            return -maxRotationSpeed;
        else if (rb.angularVelocity < minRotationSpeed && rb.angularVelocity >= 0)
            return minRotationSpeed;
        else if (rb.angularVelocity > -minRotationSpeed && rb.angularVelocity < 0)
            return -minRotationSpeed;
        // If the rotation speed is within the bounds, return the asteroid's current rotation speed.
        return rb.angularVelocity;
    }
    #endregion

    #region MonoBehaviors
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        // Choose which direction the asteroid will start moving in. Less than 0 means left and greater means right.
        int movementDirection = Random.Range(-5, 5);
        float mX, mY;
        
        // Set the initial horizontal speed and direction of the asteroid.
        if (movementDirection >= 0)
            mX = Random.Range(minHorizontalSpeed, maxHorizontalSpeed);
        else
            mX = Random.Range(-maxHorizontalSpeed, -minHorizontalSpeed);

        // The vertical direction is always negative.
        mY = Random.Range(-maxVerticalSpeed, -minVerticalSpeed);
        rb.velocity = new Vector2(mX, mY);

        // Set the inital spin speed of the asteroid. Less than 0 means clockwise and greater means counter-clockwise.
        int spinDirection = Random.Range(-5, 5);
        if (spinDirection == 1)
            rb.angularVelocity = Random.Range(-maxRotationSpeed, -minRotationSpeed);
        else
            rb.angularVelocity = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private void FixedUpdate()
    {
        float mX = ConstrainedHorizontalSpeed();
        float mY = ConstrainedVerticalSpeed();
        rb.velocity = new Vector2(mX, mY);
        rb.angularVelocity = ConstrainedRotationSpeed();
    }
    #endregion
}