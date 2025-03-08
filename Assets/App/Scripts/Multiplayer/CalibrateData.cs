using UnityEngine;

namespace Tirlim.Multiplayer
{
    public struct CalibrateData
    {
        public struct Target
        {
            public bool used;
            public Vector3 localPosition;
            public Quaternion localRotation;

            public Target(bool used, Vector3 pos, Quaternion rot)
            {
                this.used = used;
                localPosition = pos;
                localRotation = rot;
            }
        }

        public float scale;
        public float torsoScale;
        public float legScale;
        public float leftArmScale;
        public float rightArmScale;
        
        
        public Target head, leftHand, rightHand, pelvis, leftFoot, rightFoot, leftLegGoal, rightLegGoal;
        public Vector3 pelvisTargetRight;
        public float pelvisPositionWeight;
        public float pelvisRotationWeight;
    }
}