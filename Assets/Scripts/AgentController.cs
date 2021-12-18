using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentController : Agent
{
    public RocketController rocketController;
    public bool episodeFinished = false;
    /*start �Լ�*/
    public override void Initialize()
    {
        rocketController= GetComponent<RocketController>();
    }

    /*�� ������ ���������� ���� ȯ���� �ʱ�ȭ �Ѵ�*/
    public override void OnEpisodeBegin()
    {
        rocketController.ResetRocket();
        episodeFinished = false;
        
    }
    
    /*���� ȯ��, ���� ó�� ���¸� �˷��ִ� �Լ�*/
    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 rocketPosition = rocketController.transform.localPosition;
        Vector3 rocketVelocity = rocketController.rigidbody.velocity;

        sensor.AddObservation(rocketPosition.x); 
        sensor.AddObservation(rocketPosition.y);
        sensor.AddObservation(rocketPosition.z);

        sensor.AddObservation(rocketVelocity.x);
        sensor.AddObservation(rocketVelocity.y);
        sensor.AddObservation(rocketVelocity.z);
    }

    /*AI�� ���� ���¸� �޾ư��� ���� � �ൿ�� �Ѵٴ� ���� �˷���*/
    public override void OnActionReceived(ActionBuffers actions)
    //�ൿ�� ��ü (��� ���̶� �ް����� ����)
    { 
        if (actions.DiscreteActions[0] == 1)
        {
            rocketController.OnEngine();
        }
        else
        {
            rocketController.OffEngine();
        }
    }
    
    /*�̰��ӿ� � ������ �޾Ҵ�.*/
    public void EndEpisode(float reword)
    {
        SetReward(reword);
        episodeFinished = true;
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        EndEpisode();
    }

    /*���� ���� �۵��ϴ� ����ȭ �ڵ�(in�� �� ���� ���۷���)*/ 
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.Space))
        {
            discreteActions[0] = 1;
        }
        else
        {
            discreteActions[0] = 0;
        }
    }
}
