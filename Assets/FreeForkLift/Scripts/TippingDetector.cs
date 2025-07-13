using UnityEngine;

public class TippingDetector : MonoBehaviour
{
    private bool hasScored = false;

    void Update()
    {
        if (!hasScored && IsTippedOver())
        {
            hasScored = true;
            ScoreManager.Instance.AddPlayerScore(1);  // Add a point
            hasScored = false;
            //GetComponent<Renderer>().material.color = Color.red;  // Optional: visual feedback
        }
    }

    bool IsTippedOver()
    {
        float xRot = Mathf.Abs(transform.eulerAngles.x);
        float zRot = Mathf.Abs(transform.eulerAngles.z);

        if (xRot > 180) xRot = 360 - xRot;
        if (zRot > 180) zRot = 360 - zRot;

        return xRot > 45 || zRot > 45;
    }
}
