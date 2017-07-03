//using UnityEngine;
//using System.Collections;
//public class GoForward : Goal {
//    Vector2 tankPos;
//    Vector3 truePos;
//    // Update is called once per frame
//    void Update()
//    {

//    }
//    public override void Init(GameObject brain, GameObject tank)
//    {
//        this.brain = brain;
//        this.tank = tank;
//        tankPos = StageManager.I.CalcStateMapIndex(tank.transform.position);
//        Debug.Log(tank + "あいう" + tankPos);
//        truePos = StageManager.I.TOP_LEFT_POSITION + new Vector3(0.6f,0,-0.6f)+new Vector3(tankPos.y * 1.8f, 0.3f,-tankPos.x * 1.8f);
//        Debug.Log(tank + "かきく" + StageManager.I.CalcStateMapIndex(truePos));
//    }
//    public override void Activate()
//    {
//        Debug.Log("あくち");
//        //StartCoroutine(WaitMethod());
        
//        brain.GetComponent<AIBrain>().SetUp(true);
//        StartCoroutine(DelayMethod(0.45f));
//    }
//    private IEnumerator DelayMethod(float waitTime)
//    {
//        yield return new WaitForSeconds(waitTime);
//        Debug.Log("あくちお");
//        brain.GetComponent<AIBrain>().SetUp(false);
//        truePos = truePos + new Vector3(0,0, 1.8f+0.6f);
//        while ((truePos - tank.transform.position).sqrMagnitude > 0.01)
//        {
//            Debug.Log(tank+","+truePos +"" +tank.transform.position);
//            yield return new WaitForSeconds(1f / 60f);
//            bool _isMoveUp = truePos.z > tank.transform.position.z;
//            bool _isMoveLeft = truePos.x < tank.transform.position.x;
//            bool _isMoveRight = truePos.x < tank.transform.position.x ;
//            bool _isMoveDown = truePos.z > tank.transform.position.z ;
//            brain.GetComponent<AIBrain>().SetUp(_isMoveUp);
//            brain.GetComponent<AIBrain>().SetLeft(_isMoveLeft);
//            brain.GetComponent<AIBrain>().SetRight(_isMoveRight);
//            brain.GetComponent<AIBrain>().SetDown(_isMoveDown);

//        }
//        brain.GetComponent<AIBrain>().SetUp(false);
//        brain.GetComponent<AIBrain>().SetLeft(false);
//        brain.GetComponent<AIBrain>().SetRight(false);
//        brain.GetComponent<AIBrain>().SetDown(false);
//        isTerminated = true;
//        Debug.Log("owari");
//        yield break;
//    }
//    private IEnumerator WaitMethod()
//    {

//        while ((truePos - tank.transform.position).sqrMagnitude > 0.01)
//        {
//            yield return new WaitForSeconds(1f / 60f);
//            bool _isMoveUp = truePos.z - tank.transform.position.z > 0.2;
//            bool _isMoveLeft = truePos.x - tank.transform.position.x < 0.2;
//            bool _isMoveRight = truePos.x - tank.transform.position.x > -0.2;
//            bool _isMoveDown = truePos.z - tank.transform.position.z < -0.2;
//            brain.GetComponent<AIBrain>().SetUp(_isMoveUp);
//            brain.GetComponent<AIBrain>().SetLeft(_isMoveLeft);
//            brain.GetComponent<AIBrain>().SetRight(_isMoveRight);
//            brain.GetComponent<AIBrain>().SetDown(_isMoveDown);

//        }
//        brain.GetComponent<AIBrain>().SetUp(false);
//        brain.GetComponent<AIBrain>().SetLeft(false);
//        brain.GetComponent<AIBrain>().SetRight(false);
//        brain.GetComponent<AIBrain>().SetDown(false);
//        yield break;
//    }
//}
