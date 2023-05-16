using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawController : MonoBehaviour
{
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate() {
        if(player.transform.position.x < transform.position.x){
            JointMotor2D hingeMotor = new JointMotor2D();
            hingeMotor.motorSpeed = -10f;
            hingeMotor.maxMotorTorque = 10f;
            gameObject.GetComponent<HingeJoint2D>().motor = hingeMotor;
        }else{
            JointMotor2D hingeMotor = new JointMotor2D();
            hingeMotor.motorSpeed = 10f;
            hingeMotor.maxMotorTorque = 10f;
            gameObject.GetComponent<HingeJoint2D>().motor = hingeMotor;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Player"))
            gameObject.GetComponent<HingeJoint2D>().enabled = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Player"))
            gameObject.GetComponent<HingeJoint2D>().enabled = false;
    }

}
