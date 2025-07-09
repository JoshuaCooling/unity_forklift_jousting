using UnityEngine;

public class ForkMovement : MonoBehaviour
{
    public float speed = 25f;
    public float maxHeight = 15f;
    public float minHeight = 0f;

    // Add this to distinguish between players
    public string inputAxis = "Mouse ScrollWheel"; // Default for Player 1

    void Update()
    {
        float move = Input.GetAxis(inputAxis);

        Vector3 movement = new Vector3(0, move, 0) * speed * Time.deltaTime;
        float newY = transform.position.y + movement.y;

        if (newY <= maxHeight && newY >= minHeight)
        {
            transform.Translate(movement);
        }
    }
}
