using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class TaskTutorialConstantStrings
    {
        public readonly static string Welcome = "Welcome to INFINITY's gesture module tutorial level." +
                "You can traverse inside the room by teleporting. " +
                "If you point on the floor, an 'arrow' icon will apear. " +
                "By clicking you will be teleported on that location. " +
                "Please teleport to some location. To close this notification window, " +
                "click the 'X' icon on the top-right.";

        public readonly static string ArbitraryTeleportationComplete = "Great! You successfully teleported to a location in the room." +
                        "You can teleport to other standard locations by clicking on the standard" +
                        " 'arrow' icons which glow when you hover your pointer on them. " +
                        "Please teleport to the standard location in front of the map.";
        public readonly static string MapTeleportComplete = "Great! You successfully teleported in front of the map." +
                        "The map in front of you is interactive. You can click on widgets to " +
                        "see Facebook or Twitter posts, videos etc. Please click on a Facebook " +
                        "icon to see a Facebook post, and then click the 'X' icon or do the 'back' gesture to close the widget.";
        public readonly static string ViewFacebookPostComplete = "Great! You successfully viewed a Facebook post. " +
                        "Now two video icons appeared on the interactive map. " +
                        "You can click on one of them to open a video player. Please click on a " +
                        "video icon to open the video player.";

        public readonly static string OpenVideoComplete = "Great! You succesfully opened a video on the interactive map. " +
                        "You can watch the video just like any other video player by clicking on the " +
                        "'Play' button to start playback, and the 'Stop' Button to stop the video. Alternatively, you may do the 'OK' gesture" +
                        "to start/stop the video playback. Then you can " +
                        "click the 'X' button or do the 'back' gesture to close the video player.";
        public readonly static string ViewVideoComplete = "Great! You succesfully watched a video. " +
                        "The interactive map enables you to see information if you " +
                        "hover your pointer on some icons. A 'Metro Station' icon appeared " +
                        "near 'Rosny-sous-Bois'. If you hover the metro schedule will appear. " +
                        "Please hover you pointer on the 'Metro Station' icon.";
        public readonly static string ViewMetroScheduleComplete = "Great! You succesfully watched the metro schedule. " +
                        "Now teleport to your home location " +
                        "by clicking the 'home' keyboard button or doing the 'Home' gesture.";

        public readonly static string TeleportHomeComplete = "Great! You succesfully teleported to the home location. " +
                        "Now you should see a standard teleport location on your left, in front " +
                        "of a computer. Please teleport in front of the computer.";

        public readonly static string TeleportToFileImportLocationComplete = "Great! Now you should see a 'filesystem' icon on the virtual computer screen. " +
                        "You can import virtual files by accessing the computer. Please click on the 'filesystem' " +
                        "icon to see the files available for upload.";

        public readonly static string ClickFileSystemIconComplete = "Great! This is the file import user interface. You can see a list of files " +
                        "that are available for importing. You can upload a file by clicking on the " +
                        "checkbox on the left of the filename. Please select a file for upload and then " +
                        "click the 'import' button on the bottom right or do the 'OK' gesture to import it. ";

        public readonly static string FileImportComplete = "Great! You have successfully imported a file in the system. " + 
                                                           "Now teleport again to the home location " +
                                                           "by clicking the 'home' keyboard button or doing the 'home' gesture.";
        
        public readonly static string TeleportHomeComplete2 = "Great! You have sucessfully teleported to the home location. " +
                                                              "Now open the Entity HUD by clicking the 'E' button on the keyboard, doing the 'Inventory' "+
                                                              "gesture in VR or the 'win'/'open pallm' gestures with hands";

        public readonly static string EntityHUDOpened = "Great! You have successfully opened the Entity System HUD." +
                        "Select one imported item from the list in order to pin it on the map and click the 'Pin' button or do the 'OK' gesture";

        public readonly static string EntityHUDItemSelected = "Great! You have successfully selected one imported item from the list." +
                                                              "Now point on the map and 'click' in order to pin it in a specific location";

        public readonly static string EntityItemPinned =     "Great! You have successfully pinned the item on the map." +
                                                             "You can click on it in order to view it similar to other icons." +
                                                             "This concludes the Module's intro-tutorial, and now you can feel free to explore " +
                                                             "the environment.";
            

        public readonly static string[] All =
        {
            Welcome,ArbitraryTeleportationComplete,MapTeleportComplete,ViewFacebookPostComplete,OpenVideoComplete,ViewVideoComplete,
            ViewMetroScheduleComplete, TeleportHomeComplete, TeleportToFileImportLocationComplete, ClickFileSystemIconComplete, FileImportComplete,
            TeleportHomeComplete2,EntityHUDOpened,EntityHUDItemSelected,EntityItemPinned
        };
    }
}
