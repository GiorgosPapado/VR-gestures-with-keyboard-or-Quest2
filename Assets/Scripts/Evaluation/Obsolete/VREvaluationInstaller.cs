//using UnityEngine;
//using System;
//using System.Collections.Generic;
//using Zenject;
//using Assets.Scripts.Evaluation.Input;
//using Assets.Scripts.Evaluation.Input.Interface;
//using Assets.Scripts.Evaluation.Utils;
//using Assets.Scripts.Evaluation.Model.Twitter;
//using Assets.Scripts.Utils;
//using Assets.Scripts.Evaluation.Presenters;
//using Assets.Scripts.Evaluation.Mock;
//using Assets.Scripts.Evaluation.Model.Facebook;
//using Assets.Scripts.Evaluation.Model.Filesystem;
//using Assets.Scripts.Evaluation.Model.Entities;
//using Assets.Scripts.Evaluation.Input.Webcam;
//using Assets.Scripts.Evaluation.Gestures;
//using Assets.Scripts.Evaluation.Presenters.Tasks;
//using Assets.Scripts.Evaluation.Input.Devices;
//using Valve.VR;

//public class VREvaluationInstaller : MonoInstaller
//{
//    public GameObject m_FacebookMapItemPrefab;
//    public GameObject m_TwitterMapItemPrefab;
//    public GameObject m_VideoMapItemPrefab;
//    public Transform m_FacebookMapCanvas;
//    public Transform m_TwitterMapCanvas;
//    public Transform m_VideoMapCanvas;
//    public override void InstallBindings()
//    {
//        Container.BindInterfacesAndSelfTo<LibHandsCore>().AsSingle();
//        Container.BindInterfacesAndSelfTo<WebcamProvider>().AsSingle();
//        Container.Bind<KeyboardMouse>().ToSelf().AsSingle();
//        Container.Bind<VRController>().ToSelf().AsSingle();

//        Container.Bind<IMouse2D>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<Mouse2DRayProvider>();
//        Container.Bind<IRayProvider>().To<Mouse2DRayProvider>().FromComponentInHierarchy().AsCached().WhenInjectedInto<InputInterfaceSelector>();
//        Container.Bind<IRayProvider>().To<VRControllerRayProvider>().FromComponentInHierarchy().AsCached().WhenInjectedInto<InputInterfaceSelector>();

//        Container.Bind<IMouse2D>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
//        Container.Bind<IAction>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
//        Container.Bind<INavigation>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();

//        Container.Bind<IMouse2D>().To<KeyboardMouse>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
//        Container.Bind<IAction>().To<VRController>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();
//        Container.Bind<INavigation>().To<VRController>().FromResolve().WhenInjectedInto<InputInterfaceSelector>();

//        Container.BindInterfacesAndSelfTo<InputInterfaceSelector>().AsSingle().When(context => context.ObjectType != typeof(InputInterfaceSelector) && 
//        context.ObjectType != typeof(Mouse2DRayProvider));

//        //Container.BindInterfacesAndSelfTo<KeyboardMouse>().AsSingle().WhenInjectedInto<InputInterfaceSelector>();
//        //Container.BindInterfacesAndSelfTo<VRController>().AsSingle().WhenInjectedInto<InputInterfaceSelector>();
//        //Container.BindInterfacesAndSelfTo<InputInterfaceSelector>().AsSingle().WhenNotInjectedInto<InputInterfaceSelector>();

//        //Container.Bind<IWebcamProviderCore>().To<UnityWebcamProviderCore>().AsSingle();
//        Container.BindInterfacesAndSelfTo<LibHandsWebcamProviderCore>().AsSingle();
//        Container.Bind<Raycaster>().FromComponentInHierarchy().AsCached();
//        Container.Bind<Repository<TweetInfo>>().ToSelf().AsSingle();
//        Container.Bind<Repository<FbInfo>>().ToSelf().AsSingle();
//        Container.Bind<Repository<FSInfo>>().ToSelf().AsSingle();
//        Container.Bind<Repository<EntityInfo>>().ToSelf().AsSingle();
        
//        Container.Bind<Teleport>().ToSelf().FromComponentInHierarchy().AsCached();

//        Container.BindFactory<FbMapPresenter, FbMapPresenter.Factory>().FromComponentInNewPrefab(m_FacebookMapItemPrefab).
//            UnderTransform(m_FacebookMapCanvas).AsSingle();
//        Container.BindFactory<TwitterMapPresenter, TwitterMapPresenter.Factory>().FromComponentInNewPrefab(m_TwitterMapItemPrefab).
//            UnderTransform(m_TwitterMapCanvas).AsSingle();
//        Container.BindFactory<VideoMapPresenter, VideoMapPresenter.Factory>().FromComponentInNewPrefab(m_VideoMapItemPrefab).
//            UnderTransform(m_VideoMapCanvas).AsSingle();

//        Container.Bind<AddEntityToMapPresenter>().ToSelf().AsSingle();
//        Container.Bind<IInitializable>().To<Mouse2DLock>().AsSingle();
//        Container.Bind<IInitializable>().To<TwitterWidgetPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IInitializable>().To<HUDPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IInitializable>().To<FacebookPhotoWidgetPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IInitializable>().To<FilesystemWidgetPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IInitializable>().To<EntitysystemWidgetPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IInitializable>().To<WebcamWidgetPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IInitializable>().To<AddEntityToMapPresenter>().FromResolve();
//        Container.Bind<IInitializable>().To<HandController>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IInitializable>().To<TaskManagerPresenter>().FromComponentInHierarchy().AsCached();

//        Container.Bind<ITickable>().To<MockNotifications>().AsSingle();         // debug/testing
//        Container.Bind<ITickable>().To<MockEntityHUD>().AsSingle();         // debug/testing
//        Container.Bind<ITickable>().To<AddEntityToMapPresenter>().FromResolve();         // debug/testing
//        Container.Bind<IDisposable>().To<VideoPlayerPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IDisposable>().To<VideoMapPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IDisposable>().To<WebcamWidgetPresenter>().FromComponentInHierarchy().AsCached();
//        Container.Bind<IDisposable>().To<HandController>().FromComponentInHierarchy().AsCached();
//        BindDatabaseData();
//    }

//    private void BindDatabaseData()
//    {
//        Container.Bind<IEnumerable<TweetInfo>>().FromInstance(TwitterDataset.CreateTwitterDataset()).AsSingle();
//        Container.Bind<IEnumerable<FbInfo>>().FromInstance(FBDataset.CreateFbDataset()).AsSingle();
//        Container.Bind<IEnumerable<FSInfo>>().FromInstance(FSDataset.CreateFSDataset()).AsSingle();
//        Container.Bind<IEnumerable<EntityInfo>>().FromInstance(EntityDataset.CreateEntityDataset()).AsSingle();
//    }
//}