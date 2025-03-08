using System;
using System.Collections;
using RootMotion.FinalIK;
using UnityEngine;

public class IKCalibrator : MonoBehaviour
{
    public float HandDistance { get; private set; }
    public event Action<VRIKCalibrator.CalibrationData> OnIKCalibrate; 

    [Tooltip("VRIK")] 
    [SerializeField] private VRIK[] kinematics;
    [SerializeField] private VRIKCalibrator.Settings settings;

    [Header("Кости")] 
    [SerializeField] private Transform headTarget;
    [SerializeField] private Transform handRTarget;
    [SerializeField] private Transform handLTarget;
    
    [Header("Коэффиценты")] 
    [SerializeField] private Vector3 offsetHead = new Vector3(0, -0.1f, -0.1f);

    private IKSolverVR[] _solverSave;
    private VRIKCalibrator.CalibrationData data;
    private bool _skinEnabled;
    private float _distIkHand;
    private float[] _twistLocalY;

    private void Awake()
    {
        _distIkHand = (kinematics[0].references.rightForearm.localPosition.y
                       + kinematics[0].references.rightUpperArm.localPosition.y
                       + kinematics[0].references.rightHand.localPosition.y) * 100;
        HandDistance = _distIkHand * Scale() * kinematics[0].solver.rightArm.armLengthMlp;
    }

    public void Calibrate(VRIKCalibrator.CalibrationData dt = null)
    {
        foreach (var kinematic in kinematics)
        {
            kinematic.references.root.localPosition = new Vector3(kinematic.references.root.localPosition.x, 0,
                kinematic.references.root.localPosition.z);
        }

        CalibrateHead();
        StartCoroutine(Calibrating(dt));
    }

    private IEnumerator Calibrating(VRIKCalibrator.CalibrationData dt = null)
    {
        if (_solverSave == null)
        {
            _solverSave = new IKSolverVR[kinematics.Length];
        }

        yield return null;
        for (int i = 0; i < kinematics.Length; ++i)
        {
            yield return null;

            if (dt != null)
            {
                CalibrateVrik(kinematics[i], dt);
            }
            else
            {
                CalibrateVrik(kinematics[i]);
            }

            yield return null;

            CalibrateHands();
            
            OnIKCalibrate?.Invoke(data);
        }
    }
    


    #region Calibration parts of body

    /// <summary>
    /// Калибровка головы
    /// </summary>
    private void CalibrateHead()
    {
        headTarget.localPosition = offsetHead * Scale();
    }

    private void CalibrateHands()
    {
        //Проверка что руки подняты
        if (kinematics[0].solver.rightArm.position.y < headTarget.parent.localPosition.y * 0.75
            || kinematics[0].solver.leftArm.position.y < headTarget.parent.localPosition.y * 0.75)
            return;

        Vector3 startPosR = kinematics[0].references.rightShoulder.position;
        Vector3 startPosL = kinematics[0].references.leftShoulder.position;
        float multiplyPose = 0.95f;

        if (kinematics[0].references.pelvis.InverseTransformPoint(kinematics[0].solver.rightArm.position).z * 100 > 0.2f
            & kinematics[0].references.pelvis.InverseTransformPoint(kinematics[0].solver.leftArm.position).z * 100 >
            0.2f)
        {
            startPosR = kinematics[0].references.rightUpperArm.position;
            startPosL = kinematics[0].references.leftUpperArm.position;
            multiplyPose = 1.1f;
        }

        float distR = Vector3.Distance(startPosR, kinematics[0].solver.rightArm.position);
        float distL = Vector3.Distance(startPosL, kinematics[0].solver.leftArm.position);
        float coef = Mathf.Clamp((distR + distL) * 0.5f * multiplyPose / _distIkHand, 0.8f, 1.2f);
        SetLongHands(coef);
    }

    private void SetLongHands(float coef)
    {
        HandDistance = coef * _distIkHand;

        kinematics[0].solver.rightArm.armLengthMlp = coef;
        kinematics[0].solver.leftArm.armLengthMlp = coef;
    }

    private float Scale()
    {
        return headTarget.parent.localPosition.y / 1.75f;
    }

    #endregion

    #region Calibrating

    /// <summary>
    /// Калибрует VR IK по внутренним настройкам
    /// </summary>
    /// <param name="vrik"></param>
    protected void CalibrateVrik(VRIK vrik)
    {
        data = VRIKCalibrator.Calibrate(vrik, settings,
            GetTracker(headTarget),
            null,
            GetTracker(handLTarget),
            GetTracker(handRTarget));
    }

    /// <summary> Первоначальная калибровка рига с учетом настроек </summary>
    /// <param name="vrik"></param>
    /// <param name="calibrationData"></param>
    protected void CalibrateVrik(VRIK vrik, VRIKCalibrator.CalibrationData calibrationData)
    {
        VRIKCalibrator.Calibrate(vrik, calibrationData,
            GetTracker(headTarget),
            null,
            GetTracker(handLTarget),
            GetTracker(handRTarget));
    }

    private Transform GetTracker(Transform tracker)
    {
        if (tracker && tracker.gameObject.activeInHierarchy)
        {
            return tracker;
        }

        return null;
    }

    #endregion
    
}

