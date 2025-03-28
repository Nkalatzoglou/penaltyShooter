using UnityEngine;

public class LookAndEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject effectObject;
    public GameObject target;
    public bool isRotating = false;

    public float rotationSpeed = 120f; // Degrees per second

    private float timer = 0f;

    void Start()
    {
        if (effectObject != null)
            effectObject.SetActive(false);
    }

    void Update()
    {
        target=GameManager.gameManager.currentBall.gameObject;
        // === Sound & Effect Timer ===
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            if (audioSource != null)
            audioSource.pitch=Random.Range(0.7f, 1.1f);
                audioSource.Play();

            if (effectObject != null)
                effectObject.SetActive(true);

            timer = 0f;
        }

        // === Rotation Logic (Y-axis only) ===
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                float targetY = Quaternion.LookRotation(direction).eulerAngles.y;
                float currentY = transform.rotation.eulerAngles.y;

                // Interpolate the angle manually
                float newY = Mathf.MoveTowardsAngle(currentY, targetY, rotationSpeed * Time.deltaTime);

                isRotating = Mathf.Abs(Mathf.DeltaAngle(currentY, targetY)) > 0.1f;

                transform.rotation = Quaternion.Euler(0f, newY, 0f);
            }
            else
            {
                isRotating = false;
            }
        }
        else
        {
            isRotating = false;
        }
    }
}
