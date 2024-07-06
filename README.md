# MRTK-Profiles-for-NrealLight

## What's Included
<b>MRTK-NrealHandTracking:</b> <br>
Unity project to try hand tracking and MRTK together without installation of NRSDK and MRTK by yourself. <br> <br>
<b>UnityPackage:</b> <br>
Uity package including MRTK profiles, scripts and prefabs to use hand tracking and MRTK. <br>
- Please install it into your own Uity project if you make hand tracking feature work with MRTK. (See dependency.)<br>
- Additionally, please replace file `MixedRealityInputSystem.cs` of MRTK with the same name file stored in this repository.

## Dependencies:
I have tested the combination of following SDKs in my environment but other combinations probably work as well.<br>
These SDK must be installed before hand.<br><br>
(1) NRSDK Unity SDK 2.2.1<br>
<br>
(2) MRTK-Unity v2.8.2<br>
https://github.com/microsoft/MixedRealityToolkit-Unity/releases/tag/v2.8.2
<br>
*You don't need to download & install MRTK if you start development with the Unity project of this repository.
<br>
## Version of Unity
Unity 2022.3.20f1
<br>
## Tutorial
https://youtu.be/-bFwu-2yyqw
<br>
[![](https://img.youtube.com/vi/-bFwu-2yyqw/0.jpg)](https://www.youtube.com/watch?v=-bFwu-2yyqw)
<br>Video was created by Robi-TheXRGuy
<br>
<br>
<b>Notice!!</b><br>
(1) Please use following prefabs instead of NRealInput and NRealCameraRig.<br>
・NRInput_MRTK<br>
・NRCameraRig_MRTK<br>
<br>
(2) Use following version if you are using DevKit.  
https://github.com/TakashiYoshinaga/MRTK-Profiles-for-NrealLight/releases/tag/v1.0.2

## Trouble Shoot
If hand interaction doesn't work immediately after applying this profile to your project , please reboot unity editor.
<br>
<br>
<b>This is still in the process of development. <br>
Your contribution and pull requests are welcome!</b>
