using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples {
    public class Movement : MonoBehaviour
    {
        public enum CharacterState {
            None,
            Idle,
            Walk_Left,
            Walk_Right,
            Walk_Up,
            Walk_Down
        }

        [Header("Components")]
        public  Transform obj;

        [Header("Controls")]
        public string XAxis = "Horizontal";
        public string YAxis = "Vertical";

        [Header("Moving")]
        public float walkSpeed = 1.5f;
        public float runSpeed = 7f;
        public float gravityScale = 6.6f;

        [Header("Animation")]
        public SkeletonAnimationHandleExample animationHandle;

        Vector2 input = default(Vector2);
        Vector3 velocity = default(Vector3);

        CharacterState previousState, currentState;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Debug.Log(h);
            if ((h - v) != 0) {
                if (h < 0) {currentState = CharacterState.Walk_Left;}
                else if (h > 0) {currentState = CharacterState.Walk_Right;}
                else if (v < 0) {currentState = CharacterState.Walk_Down;}
                else if (v > 0) {currentState = CharacterState.Walk_Up;}
                if (previousState != currentState) {
                    previousState = currentState;
                    HandleStateChanged();
                }
            } else {
                currentState = CharacterState.Idle;
                HandleStateChanged();
            }
            Vector3 tempVect = new Vector3(h, v, 0);
            tempVect = tempVect.normalized * walkSpeed * Time.deltaTime;
            obj.transform.position += tempVect;
        }

        void HandleStateChanged () {
        // When the state changes, notify the animation handle of the new state.
            string stateName = null;
            switch (currentState) {
                case CharacterState.Idle:
                    stateName = "idle/f_l_idle";
                    break;
                case CharacterState.Walk_Up:
                    stateName = "walk/b_r_walk";
                    break;
                case CharacterState.Walk_Down:
                    stateName = "walk/f_l_walk";
                    break;
                case CharacterState.Walk_Left:
                    stateName = "walk/f_l_walk";
                    break;
                case CharacterState.Walk_Right:
                    stateName = "walk/b_r_walk";
                    break;
                default:
                    break;
            }

            animationHandle.PlayAnimationForState(stateName, 0);
        }
    }
}