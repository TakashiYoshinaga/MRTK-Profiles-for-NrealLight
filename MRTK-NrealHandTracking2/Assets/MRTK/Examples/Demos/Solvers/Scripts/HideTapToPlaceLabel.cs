﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    /// <summary>
    /// Class to toggle the visibility of a label on a Tap to Place object in the TapToPlaceExample scene.
    /// </summary>
    public class HideTapToPlaceLabel : MonoBehaviour
    {
        private TapToPlace tapToPlace;
        public GameObject placeableObjectLabel;
        void Start()
        {
            tapToPlace = gameObject.GetComponent<TapToPlace>();
            if (tapToPlace != null && placeableObjectLabel != null)
            {
                AddTapToPlaceListeners();
            }
        }

        /// <summary>
        /// Add listeners to Tap to Place events to show a label on a placeable object while it is not being placed.
        /// </summary>
        private void AddTapToPlaceListeners()
        {
            if (tapToPlace != null)
            {
                tapToPlace.OnPlacingStarted.AddListener(() =>
                {
                    placeableObjectLabel.SetActive(false);
                });

                tapToPlace.OnPlacingStopped.AddListener(() =>
                {
                    placeableObjectLabel.SetActive(true);
                });
            }
        }
    }
}