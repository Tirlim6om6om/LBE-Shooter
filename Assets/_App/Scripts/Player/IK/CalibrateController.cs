using UnityEngine;
using UnityEngine.InputSystem;

public class CalibrateController : MonoBehaviour
{
    [SerializeField] private InputActionReference calibrateActionReference = null;
    [SerializeField] private IKCalibrator calibrator = null;

    private void OnEnable()
    {
        if (calibrateActionReference != null && calibrateActionReference.action != null)
        {
            calibrateActionReference.action.performed += OnCalibrateAction;
            calibrateActionReference.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (calibrateActionReference != null && calibrateActionReference.action != null)
        {
            calibrateActionReference.action.performed -= OnCalibrateAction;
            calibrateActionReference.action.Disable();
        }
    }

    private void OnCalibrateAction(InputAction.CallbackContext context)
    {
        if (calibrator != null)
        {
            calibrator.Calibrate();
        }
    }
}