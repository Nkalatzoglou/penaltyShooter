using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBird : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float waitingTime = 2f;
    public List<Transform> positions;

    [Header("Effect")]
    public Transform effect;

    private int currentTargetIndex = 0;
    private bool isWaiting = false;

    public int BonusScore=50;

    void Start()
    {
        if (positions == null || positions.Count == 0)
        {
            Debug.LogError("No target positions assigned to FlyingBird script.");
        }

        if (effect != null)
        {
            effect.gameObject.SetActive(false); // Make sure effect is off initially
        }
    }

    void Update()
    {
        if (positions == null || positions.Count == 0 || isWaiting) return;

        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Transform target = positions[currentTargetIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > 0.05f)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            StartCoroutine(WaitAtPosition());
        }
    }

    IEnumerator WaitAtPosition()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitingTime);

        // Go to next target in circular order
        currentTargetIndex = (currentTargetIndex + 1) % positions.Count;
        isWaiting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            StartCoroutine(waitoDisactivate());
            puffEffect.instance.instantiateOnPos(other.gameObject.transform.position);
            GameManager.gameManager.addGoal_Bonus(BonusScore);
        }
    }

    IEnumerator waitoDisactivate()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
