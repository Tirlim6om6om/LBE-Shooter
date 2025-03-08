using Mirror;
using RootMotion.FinalIK;
using UnityEngine;

namespace Tirlim.Multiplayer
{
    public class NetworkCalibration : NetworkBehaviour
    {
        [SerializeField] private IKCalibrator calibrator;

        public override void OnStartAuthority()
        {
            calibrator.OnIKCalibrate += OnCalibrated;
            calibrator.Calibrate();
        }

        public override void OnStopAuthority()
        {
            calibrator.OnIKCalibrate -= OnCalibrated;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if(!isOwned)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                calibrator.Calibrate();
            }
        }
#endif
        
        private void OnCalibrated(VRIKCalibrator.CalibrationData data)
        {
            CalibrateCmd(DataConverter(data));
        }

        [Command]
        private void CalibrateCmd(CalibrateData data)
        {
            CalibrateRpc(data);
        }

        [ClientRpc(includeOwner = false)]
        private void CalibrateRpc(CalibrateData data)
        {
            calibrator.Calibrate(DataUnconvertor(data));
        }

        #region Data converter

        private CalibrateData DataConverter(VRIKCalibrator.CalibrationData dt)
        {
            CalibrateData newData = new CalibrateData();

            //targets
            newData.head = GetTarget(dt.head);
            newData.leftHand = GetTarget(dt.leftHand);
            newData.rightHand = GetTarget(dt.rightHand);

            //other
            newData.scale = dt.scale;
            newData.pelvisTargetRight = dt.pelvisTargetRight;
            newData.pelvisPositionWeight = dt.pelvisPositionWeight;
            newData.pelvisRotationWeight = dt.pelvisRotationWeight;

            return newData;
        }

        private VRIKCalibrator.CalibrationData DataUnconvertor(CalibrateData dt) {
            VRIKCalibrator.CalibrationData newData = new VRIKCalibrator.CalibrationData();

            //Таргеты
            newData.head = GetTarget(dt.head);
            newData.leftHand = GetTarget(dt.leftHand);
            newData.rightHand = GetTarget(dt.rightHand);

            //Скейлы
            newData.scale = dt.scale;
            newData.torsoScale = dt.torsoScale;
            newData.legScale = dt.legScale;
            newData.leftArmScale = dt.leftArmScale;
            newData.rightArmScale = dt.rightArmScale;
            
            //Прочее
            newData.pelvisTargetRight = dt.pelvisTargetRight;
            newData.pelvisPositionWeight = dt.pelvisPositionWeight;
            newData.pelvisRotationWeight = dt.pelvisRotationWeight;

            return newData;
        }
        
        private CalibrateData.Target GetTarget(VRIKCalibrator.CalibrationData.Target target)
        {
            return new CalibrateData.Target(target.used, target.localPosition, target.localRotation);
        }
        
        private VRIKCalibrator.CalibrationData.Target GetTarget(CalibrateData.Target target)
        {
            return new VRIKCalibrator.CalibrationData.Target(target.used, target.localPosition, target.localRotation);
        }

        #endregion
    }
}
