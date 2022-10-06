using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using NRKernal;
using Unity.Profiling;
using UnityEngine;

namespace ARFukuoka.MixedReality.Toolkit.Nreal.Input
{
    [MixedRealityController(SupportedControllerType.ArticulatedHand, new[] { Handedness.Left, Handedness.Right })]
    public class NrealController : BaseHand
    {
        /// <summary>
        /// Constructor for a Nreal Articulated Hand
        /// </summary>
        /// <param name="trackingState">Tracking state for the controller</param>
        /// <param name="controllerHandedness">Handedness of this controller (Left or Right)</param>
        /// <param name="inputSource">The origin of user input for this controller</param>
        /// <param name="interactions">The controller interaction map between physical inputs and the logical representation in MRTK</param>
        public NrealController(
            Microsoft.MixedReality.Toolkit.TrackingState trackingState,
            Handedness controllerHandedness,
            IMixedRealityInputSource inputSource = null,
            MixedRealityInteractionMapping[] interactions = null)
            : base(trackingState, controllerHandedness, inputSource, interactions, new ArticulatedHandDefinition(inputSource, controllerHandedness))
        { }

        // Joint poses of the MRTK hand based on the Nreal hand data
        private readonly Dictionary<TrackedHandJoint, MixedRealityPose> jointPoses = new Dictionary<TrackedHandJoint, MixedRealityPose>();

        #region IMixedRealityHand Implementation

        /// <inheritdoc/>
        public override bool TryGetJoint(TrackedHandJoint joint, out MixedRealityPose pose) => jointPoses.TryGetValue(joint, out pose);

        #endregion IMixedRealityHand Implementation

        private ArticulatedHandDefinition handDefinition;
        internal ArticulatedHandDefinition HandDefinition => handDefinition ?? (handDefinition = Definition as ArticulatedHandDefinition);

        /// <summary>
        /// If true, the current joint pose supports far interaction via the default controller ray.  
        /// </summary>
        public override bool IsInPointingPose => HandDefinition.IsInPointingPose;

        /// <summary>
        /// If true, the hand is in air tap gesture, also called the pinch gesture.
        /// </summary>
        public bool IsPinching => HandDefinition.IsPinching;

        // Array of TrackedHandJoint names
        private static readonly TrackedHandJoint[] TrackedHandJointEnum = (TrackedHandJoint[])Enum.GetValues(typeof(TrackedHandJoint));

        // The Nreal AttachmentHand contains the joint poses for the current Nreal hand in frame. There is one AttachmentHand, either 
        // left or right, associated with a NrealMotionArticulatedHand.
        private NRHand nrealHand = null;

      


        private List<TrackedHandJoint> metacarpals = new List<TrackedHandJoint>
        {
            TrackedHandJoint.IndexMetacarpal,
            TrackedHandJoint.MiddleMetacarpal,
            TrackedHandJoint.RingMetacarpal,
        };

        /// <summary>
        /// Set the Nreal hands required for retrieving joint pose data.  A Nreal AttachmentHand contains AttachmentPointFlags which are equivalent to 
        /// MRTK's TrackedHandJoint.  The Nreal AttachmentHand contains all joint poses for a hand except the metacarpals.  The Nreal Hand is 
        /// used to retrieve the metacarpal joint poses.
        /// </summary>
        internal void SetAttachmentHands(NRHand nrhand)
        {
            // Set the nreal hand with the corresponding handedness
            nrealHand = nrhand;          
        }


        /// <summary>
        /// Adds the joint poses calculated from the NRSDK to the jointPoses Dictionary.
        /// </summary>
        private void SetJointPoses()
        {
            foreach (TrackedHandJoint joint in TrackedHandJointEnum)
            {
                if (nrealHand != null && nrealHand.GetHandState().isTracked)
                {
                    IsPositionAvailable = IsRotationAvailable = true;

                    HandJointID jointID = ConvertMRTKJointToNrealJoint(joint);
                    // Get the pose of the nreal joint
                    HandState hand= nrealHand.GetHandState();
                    Pose p= hand.jointsPoseDict[jointID];
                    Vector3 forward = p.up;
                    Vector3 up = p.forward;
                    
                    // Set the pose calculated by the Nreal to a mixed reality pose
                    MixedRealityPose pose = new MixedRealityPose(p.position, Quaternion.LookRotation(forward, -up));
                    jointPoses[joint] = pose;
                }
                else
                {
                    IsPositionAvailable = IsRotationAvailable = false;

                    jointPoses[joint] = MixedRealityPose.ZeroIdentity;
                }
            }
        }

       

        /// <summary>
        /// Converts a TrackedHandJoint to a Nreal AttachmentPointFlag. An AttachmentPointFlag is Nreal's version of MRTK's TrackedHandJoint.
        /// </summary>
        /// <param name="joint">TrackedHandJoint to be mapped to a Nreal Hand Joint</param>
        /// <returns>Nreal AttachmentPointFlag pose</returns>
        static internal HandJointID ConvertMRTKJointToNrealJoint(TrackedHandJoint joint)
        {
            switch (joint)
            {
                case TrackedHandJoint.Palm: return HandJointID.Palm;//AttachmentPointFlags.Palm;
                case TrackedHandJoint.Wrist: return HandJointID.Wrist;//AttachmentPointFlags.Wrist;
                   
                case TrackedHandJoint.ThumbProximalJoint: return HandJointID.ThumbProximal;//AttachmentPointFlags.ThumbProximalJoint;
                case TrackedHandJoint.ThumbDistalJoint: return HandJointID.ThumbDistal;//AttachmentPointFlags.ThumbDistalJoint;
                case TrackedHandJoint.ThumbTip: return HandJointID.ThumbTip;//AttachmentPointFlags.ThumbTip;
                case TrackedHandJoint.ThumbMetacarpalJoint: return HandJointID.ThumbMetacarpal;

                case TrackedHandJoint.IndexKnuckle: return HandJointID.IndexProximal;//AttachmentPointFlags.IndexKnuckle;
                case TrackedHandJoint.IndexMiddleJoint: return HandJointID.IndexMiddle;//.IndexMiddleJoint;
                case TrackedHandJoint.IndexDistalJoint: return HandJointID.IndexDistal;//AttachmentPointFlags.IndexDistalJoint;
                case TrackedHandJoint.IndexTip: return HandJointID.IndexTip;//AttachmentPointFlags.IndexTip;
           
                case TrackedHandJoint.MiddleKnuckle: return HandJointID.MiddleProximal;//AttachmentPointFlags.MiddleKnuckle;
                case TrackedHandJoint.MiddleMiddleJoint: return HandJointID.MiddleMiddle;//AttachmentPointFlags.MiddleMiddleJoint;
                case TrackedHandJoint.MiddleDistalJoint: return HandJointID.MiddleDistal;//AttachmentPointFlags.MiddleDistalJoint;
                case TrackedHandJoint.MiddleTip: return HandJointID.MiddleTip;//AttachmentPointFlags.MiddleTip;

                case TrackedHandJoint.RingKnuckle: return HandJointID.RingProximal;//AttachmentPointFlags.RingKnuckle;
                case TrackedHandJoint.RingMiddleJoint: return HandJointID.RingMiddle;//AttachmentPointFlags.RingMiddleJoint;
                case TrackedHandJoint.RingDistalJoint: return HandJointID.RingDistal;//AttachmentPointFlags.RingDistalJoint;
                case TrackedHandJoint.RingTip: return HandJointID.RingTip;//AttachmentPointFlags.RingTip;

                case TrackedHandJoint.PinkyKnuckle: return HandJointID.PinkyProximal;//AttachmentPointFlags.PinkyKnuckle;
                case TrackedHandJoint.PinkyMiddleJoint: return HandJointID.PinkyMiddle;//AttachmentPointFlags.PinkyMiddleJoint;
                case TrackedHandJoint.PinkyDistalJoint: return HandJointID.PinkyDistal;//AttachmentPointFlags.PinkyDistalJoint;
                case TrackedHandJoint.PinkyTip: return HandJointID.PinkyTip;//AttachmentPointFlags.PinkyTip;
                case TrackedHandJoint.PinkyMetacarpal: return HandJointID.PinkyMetacarpal;

                // Metacarpals are not included in AttachmentPointFlags
                default: return HandJointID.Wrist;//AttachmentPointFlags.Wrist;
            }
        }

        private static readonly ProfilerMarker UpdateStatePerfMarker = new ProfilerMarker("[MRTK] NrealMotionArticulatedHand.UpdateState");

        /// <summary>
        /// Updates the joint poses and interactions for the articulated hand.
        /// </summary>
        public void UpdateState()
        {
          
            using (UpdateStatePerfMarker.Auto())
            {
                // Get and set the joint poses provided by the Nreal Controller 
                SetJointPoses();

                // Update hand joints and raise event via handDefinition
                HandDefinition?.UpdateHandJoints(jointPoses);

                UpdateInteractions();

                UpdateVelocity();

               
            }
        }

        /// <summary>
        /// Updates the visibility of the hand ray and raises input system events based on joint pose data.
        /// </summary>
        protected void UpdateInteractions()
        {
            MixedRealityPose pointerPose = jointPoses[TrackedHandJoint.Palm];
            MixedRealityPose gripPose = jointPoses[TrackedHandJoint.Palm];
            MixedRealityPose indexPose = jointPoses[TrackedHandJoint.IndexTip];
            // Only update the hand ray if the hand is in pointing pose
            if (IsInPointingPose)
            {
                HandRay.Update(pointerPose.Position, GetPalmNormal(), CameraCache.Main.transform, ControllerHandedness);
                Ray ray = HandRay.Ray;

                pointerPose.Position = jointPoses[TrackedHandJoint.IndexKnuckle].Position;//ray.origin;
                pointerPose.Rotation = Quaternion.LookRotation(ray.direction);
            }

            CoreServices.InputSystem?.RaiseSourcePoseChanged(InputSource, this, gripPose);
           
            for (int i = 0; i < Interactions?.Length; i++)
            {
                switch (Interactions[i].InputType)
                {
                    case DeviceInputType.SpatialPointer:
                        Interactions[i].PoseData = pointerPose;
                        if (Interactions[i].Changed)
                        {
                            CoreServices.InputSystem?.RaisePoseInputChanged(InputSource, ControllerHandedness, Interactions[i].MixedRealityInputAction, pointerPose);
                        }
                        break;
                    case DeviceInputType.SpatialGrip:
                        Interactions[i].PoseData = gripPose;
                        if (Interactions[i].Changed)
                        {
                            CoreServices.InputSystem?.RaisePoseInputChanged(InputSource, ControllerHandedness, Interactions[i].MixedRealityInputAction, gripPose);
                        }
                        break;
                    case DeviceInputType.Select:
                    case DeviceInputType.TriggerPress:
                    case DeviceInputType.GripPress:
                        Interactions[i].BoolData = IsPinching;
                        if (Interactions[i].Changed)
                        {
                            if (Interactions[i].BoolData)
                            {
                                CoreServices.InputSystem?.RaiseOnInputDown(InputSource, ControllerHandedness, Interactions[i].MixedRealityInputAction);
                            }
                            else
                            {
                                CoreServices.InputSystem?.RaiseOnInputUp(InputSource, ControllerHandedness, Interactions[i].MixedRealityInputAction);
                            }
                        }
                        break;
                    case DeviceInputType.IndexFinger:
                        HandDefinition?.UpdateCurrentIndexPose(Interactions[i]);
                        break;
                    case DeviceInputType.ThumbStick:
                        HandDefinition?.UpdateCurrentTeleportPose(Interactions[i]);
                        break;
                }
            }
        }
    }
}
