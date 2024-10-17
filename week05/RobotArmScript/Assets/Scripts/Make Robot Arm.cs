using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRobotArm : MonoBehaviour
{
    public Transform arm, foreArm, digitUpperBase, digitUpperEnd, digitLowerBase, digitLowerEnd, shoulder, elbow, wristUpper, wristLower, knuckleUpper, knuckleLower;
    
    // Start is called before the first frame update
    void Start()
    {
        arm.localScale = new Vector3(2, 1, 1);
        foreArm.localScale = new Vector3(2, 1, 1);
        digitUpperBase.localScale = new Vector3(1, 0.5f, 1);
        digitUpperEnd.localScale = new Vector3(1, 0.5f, 1);
        digitLowerBase.localScale = new Vector3(1, 0.5f, 1);
        digitLowerEnd.localScale = new Vector3(1, 0.5f, 1);
        arm.localPosition = new Vector2(1, -0.5f);
        foreArm.localPosition = new Vector2(1, -0.5f);
        digitUpperBase.localPosition = new Vector2(0.5f, -0.25f);
        digitUpperEnd.localPosition = new Vector2(0.5f, 0.25f);
        digitLowerBase.localPosition = new Vector2(0.5f, 0.25f);
        digitLowerEnd.localPosition = new Vector2(0.5f, -0.25f);
        shoulder.localPosition = new Vector2(0, 0);
        elbow.localPosition = new Vector2(2, 0);
        wristUpper.localPosition = new Vector2(2, 0);
        wristLower.localPosition = new Vector2(2, -1);
        knuckleUpper.localPosition = new Vector2(1, -0.5f);
        knuckleLower.localPosition = new Vector2(1, 0.5f);
        shoulder.eulerAngles = -20 * Vector3.forward;
        elbow.eulerAngles = 40 * Vector3.forward;
        wristUpper.eulerAngles = 45 * Vector3.forward;
        knuckleUpper.eulerAngles = -75 * Vector3.forward;
        wristLower.eulerAngles = -45 * Vector3.forward;
        knuckleLower.eulerAngles = 70 * Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        elbow.localEulerAngles += Vector3.forward * Time.deltaTime * 60;
        shoulder.localEulerAngles -= Vector3.forward * Time.deltaTime * 80;
    }
}
