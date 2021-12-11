using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public AgentController agentController;
    public Renderer floorRenderer;
    public float force = 20f;
    public bool engineOn = false;

    public Vector3 initialPosition = new Vector3(0, 10, 0);

    public bool reset = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        agentController = GetComponent<AgentController>();
    }

    public void ResetRocket()
    {
        reset = true;
    }

    public void OnEngine()
    {
        engineOn = true;
    }

    public void OffEngine()
    {
        engineOn = false;
    }

    private void FixedUpdate()
    {
        if (agentController.episodeFinished)
        {
            return;
        }

        if (reset)
        {
            transform.position = initialPosition;
            rigidbody.velocity = Vector3.zero;

            reset = false;
            floorRenderer.material.color = new Color(56/ 255f, 56 / 255f, 56 / 255f);
            return;
        }

        if (engineOn)
        {
            rigidbody.AddForce(Vector3.up * force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (agentController.episodeFinished)
        {
            return;
        }

        if (collision.relativeVelocity.y > 10f)
        {
            floorRenderer.material.color = Color.red;
            agentController.EndEpisode(0f);
        }
        else
        {
            floorRenderer.material.color = Color.green;
            agentController.EndEpisode(1f);
        }
    }
}
