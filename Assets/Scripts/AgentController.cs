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
    /*start 함수*/
    public override void Initialize()
    {
        rocketController= GetComponent<RocketController>();
    }

    /*매 게임을 시작했을때 세팅 환경을 초기화 한다*/
    public override void OnEpisodeBegin()
    {
        rocketController.ResetRocket();
        episodeFinished = false;
        
    }
    
    /*나의 환경, 내가 처한 상태를 알려주는 함수*/
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

    /*AI가 나의 상태를 받아가서 내가 어떤 행동을 한다는 것을 알려줌*/
    public override void OnActionReceived(ActionBuffers actions)
    //행동의 주체 (결과 값이랑 햇갈리지 말고)
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
    
    /*이게임에 어떤 보상을 받았다.*/
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

    /*내가 직접 작동하는 최적화 코드(in은 콜 바이 레퍼런스)*/ 
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
