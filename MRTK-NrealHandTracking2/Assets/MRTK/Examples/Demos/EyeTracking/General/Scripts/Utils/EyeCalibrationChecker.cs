﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.Events;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos.EyeTracking
{
    /// <summary>
    /// Checks whether the user is calibrated and prompts a notification to encourage the user to calibrate.
    /// </summary>
    [AddComponentMenu("Scripts/MRTK/Examples/EyeCalibrationChecker")]
    public class EyeCalibrationChecker : MonoBehaviour
    {
        [Tooltip("For testing purposes, you can manually assign whether the user is eye calibrated or not.")]
        [SerializeField]
        private bool editorTestUserIsCalibrated = true;

        public UnityEvent OnEyeCalibrationDetected;
        public UnityEvent OnNoEyeCalibrationDetected;

        private bool? prevCalibrationStatus = null;

        private void Update()
        {
            bool? calibrationStatus;

            if (Application.isEditor)
            {
                calibrationStatus = editorTestUserIsCalibrated;
            }
            else
            {
                calibrationStatus = CoreServices.InputSystem?.EyeGazeProvider?.IsEyeCalibrationValid;
            }

            if (calibrationStatus.HasValue)
            {
                if (prevCalibrationStatus != calibrationStatus)
                {
                    if (!calibrationStatus.Value)
                    {
                        OnNoEyeCalibrationDetected.Invoke();
                    }
                    else
                    {
                        OnEyeCalibrationDetected.Invoke();
                    }
                    prevCalibrationStatus = calibrationStatus;
                }
            }
        }
    }
}
