using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;
using Assets.Scripts.Evaluation.Input;
using Assets.Scripts.Evaluation.Input.Interface;
using Assets.Scripts.Evaluation.Utils;
using Assets.Scripts.Evaluation.Model.Twitter;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Presenters;
using Assets.Scripts.Evaluation.Mock;
using Assets.Scripts.Evaluation.Model.Facebook;
using Assets.Scripts.Evaluation.Model.Filesystem;
using Assets.Scripts.Evaluation.Model.Entities;
using Assets.Scripts.Evaluation.Input.Webcam;
using Assets.Scripts.Evaluation.Gestures;
using Assets.Scripts.Evaluation.Presenters.Tasks;
using Assets.Scripts.Evaluation.Input.Devices;
using Assets.Scripts.Evaluation.Interactables;
using Assets.Scripts.Evaluation.Gestures.ONNX;
using Assets.Scripts.Evaluation.Views;
using Assets.Scripts.Evaluation.Input.VRController;
using Assets.Scripts.Evaluation.Gestures.Interface;
using Assets.Scripts.Evaluation.Input.Replay;
using Assets.Scripts.Evaluation.Input.Replay.Playback;
using Assets.Scripts.Evaluation.Input.Replay.Webcam;
using Assets.Scripts.Evaluation.Input.Replay.VRController;

public class EvaluationInstaller : MonoInstaller
{
    public GameObject m_FacebookMapItemPrefab;
    public GameObject m_TwitterMapItemPrefab;
    public GameObject m_VideoMapItemPrefab;
    public Transform m_FacebookMapCanvas;
    public Transform m_TwitterMapCanvas;
    public Transform m_VideoMapCanvas;
    public DisplayDeviceType m_DisplayDeviceType = DisplayDeviceType.Monitor;    

    public override void InstallBindings()
    {        
        BindInput();
        BindCommon();
        BindReplay();
        BindWebcam();                           // do not remove for vr. it should be just like that
        BindHandGestureRecognition();           // do not remove for vr. it should be just like that
        //BindVRGestureRecognition();
        BindDatabaseData();
    }
    private void BindReplay()
    {        
        switch (m_DisplayDeviceType)
        {
            case DisplayDeviceType.Monitor:
                BindReplayKeyboardMouseWebcam();
                break;
            case DisplayDeviceType.VR:
                BindReplayKeyboardMouseVR();
                break;
        }
    }

    private void BindReplayKeyboardMouseVR()
    {
        //Container.Bind<IRecorder>().To<NullRecorder>().AsSingle();
        Container.Bind<IStateCapturer>().To<VRControllerStateCapturer>().AsSingle();
        Container.Bind<InputRecorder>().ToSelf().AsSingle();
        Container.Bind<Recorder>().ToSelf().AsSingle();
        Container.Bind<ITickable>().To<Recorder>().FromResolve();
        Container.BindTickableExecutionOrder<Recorder>(-1);      // large values have higher priority. here we want low priority
        Container.Bind<IRecorder>().To<Recorder>().FromResolve();
        Container.Bind<IPlayer>().To<NullPlayer>().AsSingle();
        //throw new NotImplementedException();
    }

    private void BindReplayKeyboardMouseWebcam()
    {
        Container.Bind<IStateCapturer>().To<WebcamStateCapturer>().AsSingle();
        Container.Bind<InputRecorder>().ToSelf().AsSingle();
        Container.Bind<Recorder>().ToSelf().AsSingle();
        Container.Bind<WebcamPlayer>().ToSelf().AsSingle();
        Container.Bind<ITickable>().To<Recorder>().FromResolve();
        Container.BindTickableExecutionOrder<Recorder>(-1);      // large values have higher priority. here we want low priority
        Container.Bind<IRecorder>().To<Recorder>().FromResolve();
        Container.Bind<IPlayer>().To<WebcamPlayer>().FromResolve();

        // BindPlayer
        Container.BindTickableExecutionOrder<WebcamPlayer>(100);
        Container.Bind<IMouse2D>().To<WebcamPlayer>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<IAction>().To<WebcamPlayer>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<ITickable>().To<WebcamPlayer>().FromResolve();
        Container.Bind<IGestureAction>().To<WebcamPlayer>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<INavigation>().To<WebcamPlayer>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<IRayProvider>().To<NullRayProvider>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<InputInterfaceName>().FromInstance(InputInterfaceName.ReplayWebcam).WhenInjectedInto<InputInterfaceSelector>();
    }
    private void BindHandGestureRecognition()
    {
        Container.Bind<HandGestures>().ToSelf().AsSingle();
    }

    private void BindInput()
    {
        Container.Bind<DisplayDeviceType>().FromInstance(m_DisplayDeviceType);
        Container.BindInterfacesAndSelfTo<InteractionInvoker>().AsSingle();

        switch (m_DisplayDeviceType)
        {
            case DisplayDeviceType.Monitor:
                BindInputKeyboardWebcam();
                break;
            case DisplayDeviceType.VR:
                BindInputVR();
                break;
        }                
    }

    //private void BindInputKeyboardWebcam()
    //{
    //    /*
    //    Container.Bind<KeyboardMouse>().ToSelf().AsSingle();                
    //    Container.Bind<IMouse2D>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
    //    Container.Bind<IAction>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
    //    Container.Bind<INavigation>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
    //    Container.Bind<IRayProvider>().To<Mouse2DRayProvider>().FromComponentInHierarchy().AsCached().WhenInjectedInto<InputInterfaceSelector>();
    //    Container.Bind<string>().FromInstance("Keyboard / Mouse Input").WhenInjectedInto<InputInterfaceSelector>();

    //    Container.BindInterfacesAndSelfTo<InputInterfaceSelector>().AsSingle().When(context => context.ObjectType != typeof(InputInterfaceSelector) &&
    //             context.ObjectType != typeof(Mouse2DRayProvider));
    //    */

    //    Container.Bind<KeyboardMouse>().ToSelf().AsSingle();
    //    //Container.Bind<IMouse2D>().To<KeyboardMouse>().FromResolve();
    //    //Container.Bind<IAction>().To<KeyboardMouse>().FromResolve();

    //    Container.Bind<WebcamHands>().ToSelf().AsSingle();
    //    Container.Bind<IMouse2D>().To<WebcamHands>().FromResolve();        
    //    Container.Bind<IAction>().To<WebcamHands>().FromResolve();
    //    Container.Bind<ITickable>().To<WebcamHands>().FromResolve();
    //    Container.Bind<IGestureAction>().To<WebcamHands>().FromResolve();
    //    Container.Bind<INavigation>().To<KeyboardMouse>().FromResolve();
    //    Container.Bind<IRayProvider>().To<Mouse2DRayProvider>().FromComponentInHierarchy().AsCached();
    //    Container.BindTickableExecutionOrder<WebcamHands>(100);
    //}

    private void BindInputKeyboardWebcam()
    {
        Container.Bind<KeyboardMouse>().ToSelf().AsSingle();
        Container.Bind<WebcamHands>().ToSelf().AsSingle();
        Container.Bind<NullRayProvider>().ToSelf().AsSingle();

        Container.Bind<IMouse2D>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<IAction>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<IGestureAction>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<INavigation>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<IRayProvider>().To<NullRayProvider>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<InputInterfaceName>().FromInstance(InputInterfaceName.KeyboardMouse).WhenInjectedInto<InputInterfaceSelector>();

        Container.Bind<IMouse2D>().To<WebcamHands>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<IAction>().To<WebcamHands>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<ITickable>().To<WebcamHands>().FromResolve();
        Container.Bind<IGestureAction>().To<WebcamHands>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<INavigation>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<IRayProvider>().To<NullRayProvider>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
        Container.Bind<InputInterfaceName>().FromInstance(InputInterfaceName.WebcamGestures).WhenInjectedInto<InputInterfaceSelector>();
        Container.BindTickableExecutionOrder<WebcamHands>(100);

        Container.Bind<InputInterfaceSelector>().ToSelf().AsSingle();
        Container.Bind<IMouse2D>().To<InputInterfaceSelector>().FromResolve().WhenNotInjectedInto<InputInterfaceSelector>();
        Container.Bind<IAction>().To<InputInterfaceSelector>().FromResolve().WhenNotInjectedInto<InputInterfaceSelector>();
        Container.Bind<IGestureAction>().To<InputInterfaceSelector>().FromResolve().WhenNotInjectedInto<InputInterfaceSelector>();
        Container.Bind<INavigation>().To<InputInterfaceSelector>().FromResolve().WhenNotInjectedInto<InputInterfaceSelector>();
        Container.Bind<ITickable>().To<InputInterfaceSelector>().FromResolve();
        Container.Bind<IRayProvider>().To<Mouse2DRayProvider>().FromComponentInHierarchy().AsCached().WhenNotInjectedInto<InputInterfaceSelector>();                
    }

    private void BindInputVR()
    {
        Container.Bind<VRController>().ToSelf().AsSingle();
        Container.Bind<IMouse2D>().To<NullMouse2D>().AsSingle();
        Container.Bind<IAction>().To<VRController>().FromResolve();
        //Container.Bind<IGestureAction>().To<NullGestureAction>().AsSingle();
        Container.Bind<IGestureAction>().To<VRController>().FromResolve();
        Container.Bind<INavigation>().To<VRController>().FromResolve();
        Container.Bind<IRayProvider>().To<VRControllerRayProvider>().FromComponentInHierarchy().AsCached();
        Container.Bind<InputInterfaceSelector>().ToSelf().AsSingle();
    }

    private void BindVRGestureRecognition()
    {
        
        Container.Bind<SensorTracker>().FromComponentInHierarchy().AsSingle();
        Container.Bind<VRGestureRecordTrigger>().FromComponentInHierarchy().AsCached();
        Container.Bind<VRControllerGestures>().ToSelf().AsSingle();
        Container.BindInterfacesAndSelfTo<VRGestureProvider>().AsSingle();
        //Container.Bind<IInitializable>().To<VRGestureProvider>().AsSingle();
        //Container.Bind<IVRControllerGestureProvider>().To<VRGestureProvider>().AsSingle();
    }

    private void BindWebcam()
    {
        Container.BindInterfacesAndSelfTo<LibHandsCore>().AsSingle();
        Container.BindInterfacesAndSelfTo<WebcamProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<LibHandsWebcamProviderCore>().AsSingle();        
        Container.Bind<IInitializable>().To<WebcamWidgetPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<IDisposable>().To<WebcamWidgetPresenter>().FromComponentInHierarchy().AsCached();        
    }

    private void BindCommon()
    {
        Container.Bind<IInitializable>().To<DisplayDevice>().FromComponentInHierarchy().AsCached();
        Container.BindInterfacesAndSelfTo<TopWidgetTracker>().AsSingle();
        Container.Bind<KeyboardMouseInputRecorder>().ToSelf().FromComponentInHierarchy().AsCached();
        
        Container.Bind<HandTrackView>().FromComponentInHierarchy().AsCached();      // required for injecting KeyboardMouseWebcamManager. Not used in VR

        Container.Bind<Raycaster>().FromComponentInHierarchy().AsCached();
        Container.Bind<Repository<TweetInfo>>().ToSelf().AsSingle();
        Container.Bind<Repository<FbInfo>>().ToSelf().AsSingle();
        Container.Bind<Repository<FSInfo>>().ToSelf().AsSingle();
        Container.Bind<Repository<EntityInfo>>().ToSelf().AsSingle();

        Container.Bind<Teleport>().ToSelf().FromComponentInHierarchy().AsCached();

        Container.BindFactory<FbMapPresenter, FbMapPresenter.Factory>().FromComponentInNewPrefab(m_FacebookMapItemPrefab).
            UnderTransform(m_FacebookMapCanvas).AsSingle();
        Container.BindFactory<TwitterMapPresenter, TwitterMapPresenter.Factory>().FromComponentInNewPrefab(m_TwitterMapItemPrefab).
            UnderTransform(m_TwitterMapCanvas).AsSingle();
        Container.BindFactory<VideoMapPresenter, VideoMapPresenter.Factory>().FromComponentInNewPrefab(m_VideoMapItemPrefab).
            UnderTransform(m_VideoMapCanvas).AsSingle();

        Container.Bind<AddEntityToMapPresenter>().ToSelf().AsSingle();
        Container.Bind<IInitializable>().To<Mouse2DLock>().AsSingle();
        Container.Bind<IInitializable>().To<TwitterWidgetPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<IInitializable>().To<HUDPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<IInitializable>().To<FacebookPhotoWidgetPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<IInitializable>().To<FilesystemWidgetPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<IInitializable>().To<EntitysystemWidgetPresenter>().FromComponentInHierarchy().AsCached();        
        Container.Bind<IInitializable>().To<AddEntityToMapPresenter>().FromResolve();        
        Container.Bind<IInitializable>().To<TaskManagerPresenter>().FromComponentInHierarchy().AsCached();

        Container.Bind<ITickable>().To<ActionEventProvider>().AsSingle();
        Container.Bind<ITickable>().To<EntityHUDPresenter>().AsSingle();
        Container.Bind<ITickable>().To<MockNotifications>().AsSingle();         // debug/testing
        //Container.Bind<ITickable>().To<MockEntityHUD>().AsSingle();         // debug/testing
        Container.Bind<ITickable>().To<AddEntityToMapPresenter>().FromResolve();         // debug/testing
        Container.Bind<IDisposable>().To<VideoPlayerPresenter>().FromComponentInHierarchy().AsCached();
        Container.Bind<IDisposable>().To<VideoMapPresenter>().FromComponentInHierarchy().AsCached();        
    }
    private void BindDatabaseData()
    {
        Container.Bind<IEnumerable<TweetInfo>>().FromInstance(TwitterDataset.CreateTwitterDataset()).AsSingle();
        Container.Bind<IEnumerable<FbInfo>>().FromInstance(FBDataset.CreateFbDataset()).AsSingle();
        Container.Bind<IEnumerable<FSInfo>>().FromInstance(FSDataset.CreateFSDataset()).AsSingle();
        Container.Bind<IEnumerable<EntityInfo>>().FromInstance(EntityDataset.CreateEntityDataset()).AsSingle();
    }
}