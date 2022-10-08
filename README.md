# MRTK-Profiles-for-NrealLight

## What's Included
<b>MRTK-NrealHandTracking:</b> <br>
It's unity project to try hand tracking and MRTK together without installation of NRSDK and MRTK by yourself. <br> <br>
<b>DemoAPK:</b> <br>
It's installer of demo application to trya hand tracking feature. Just install the app to try this out! <br> <br>
<b>UnityPackage:</b> <br>
It's uity package including MRTK profiles, scripts and prefabs to use hand tracking and MRTK. <br>
Please install it into your own Uity project if you make hand tracking work MRTK. (See dependency.)<br>

## Dependencies:
I have tested the combination of following SDKs in my environment. Other combinations probably work as well.<br>
These SDK must be installed before hand.<br>
(1) NRSDK Unity SDK 1.9.5<br>
https://developer.nreal.ai/download
<br><br>
(2) MRTK-Unity v2.8.2<br>
https://github.com/microsoft/MixedRealityToolkit-Unity/releases/tag/v2.8.2
<br>
*You don't need to download & install MRTK if you start development with the Unity project of this repository.
<br>
## Version of Unity
Unity 2020.3.39f1
<br>
## Tutorial
https://youtu.be/-bFwu-2yyqw
<br>
[![](https://img.youtube.com/vi/-bFwu-2yyqw/0.jpg)](https://www.youtube.com/watch?v=-bFwu-2yyqw)
<br>Video was created by Robi-TheXRGuy
<br>
<br>
<b>Notice!!</b><br>
Please use following prefabs instead of NRealInput and NRealCameraRig.<br>
・NRInput_MRTK<br>
・NRCameraRig_MRTK<br>
<br>
## Trouble Shoot
If your application doesn't work on device, especialy on consumer version, please replace AndroidManifest.xml file placed in Plugin/Android folder with following xml file.<br>
https://github.com/TakashiYoshinaga/MRTK-Profiles-for-NrealLight/releases/download/v0.0.6/AndroidManifest.xml
<br><br>
If hand interaction doesn't work immediately after applying this profile to your project , please reboot unity editor.
<br>
<br>
<b>This is still in the process of development. <br>
Your contribution and pull requests are welcome!</b>
