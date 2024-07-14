using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalState : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float stateTimer = 0f;
    private float minStateTime = 2f;
    public float sightRange = 3f;
    public string CurrentState = "Idle";


    private List<string> States = new List<string> { "Moving", "Idle" };

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        minStateTime = Random.Range(2f, 5f);
    }

    private void SetState(string state)
    {
        CurrentState = state;
        stateTimer = 0f;
        minStateTime = Random.Range(2f, 5f);
        animator.SetTrigger(CurrentState);
    }

    private void SetRandomState()
    {
        CurrentState = States[Random.Range(0, States.Count)];
        // Change the animal's behavior based on the selected state
        if (CurrentState == "Moving")
        {
            GetRandomHeading();
        }
        SetState(CurrentState);
    }



    private void Update()
    {
        CheckForPlayer();

        if (stateTimer >= minStateTime)
        {
            SetRandomState();
        }
        // Update the state timer
        stateTimer += Time.deltaTime;

        // Check if the state timer has reached the minimum state time

        switch (CurrentState)
        {
            case "Eating":
                Eat();
                break;
            case "Rolling":
                RollAround();
                break;
            case "Moving":
                Move();
                break;
            case "Idle":
                RunIdle();
                break;
            case "Attack":
                Debug.Log("Animal is attacking");
                break;
        }
    }

    private void RunIdle()
    {
        // Implement the idle behavior here
        Debug.Log("Animal is idle");
    }

    private void GetRandomHeading()
    {
        float randomAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        var offset = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);

        Debug.DrawRay(offset, transform.forward * sightRange, Color.magenta);
        Physics.Raycast(offset, transform.forward, out RaycastHit hit, sightRange);

        var tag = hit.collider?.gameObject.tag ?? "Untagged";
        if (tag == "Player" || tag == "Obstacle")
        {
            // Run away from the player
            transform.rotation *= Quaternion.Euler(0f, Random.Range(45f, 180f), 0f);
            SetState("Moving");
        }

    }

    private void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void Eat()
    {
        // Implement the eating behavior here
        Debug.Log("Animal is eating");
    }

    private void RollAround()
    {
        // Implement the rolling behavior here
        Debug.Log("Animal is rolling around");
    }

}
