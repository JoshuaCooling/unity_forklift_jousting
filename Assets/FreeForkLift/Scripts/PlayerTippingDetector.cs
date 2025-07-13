using UnityEngine;

public class PlayerTippingDetector : MonoBehaviour
{
    private bool hasScored = false;

    void Update()
    {
        if (!hasScored && IsTippedOver())
        {
            hasScored = true;
            ScoreManager.Instance.AddEnemyScore(1);  // Add to the ENEMY score
            hasScored = false;
        }
    }

    bool IsTippedOver()
    {
        float xRot = Mathf.Abs(transform.eulerAngles.x);
        float zRot = Mathf.Abs(transform.eulerAngles.z);

        if (xRot > 180) xRot = 360 - xRot;
        if (zRot > 180) zRot = 360 - zRot;

        return xRot > 75 || zRot > 75;
    }
}
