using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Scripts.Evaluation.Model.Tasks
{
    public enum TaskType
    {
        TELEPORTATION,
        STANDARD_TELEPORTATION,
        STANDARD_TELEPORTATION_MAP,
        STANDARD_TELEPORTATION_FILESYSTEM,
        STANDARD_TELEPORTATION_CHART,
        VIDEO_PLAYER_WIDGET_CLICKED,
        VIDEO_PLAYER_PLAYBACK_COMPLETE,
        METRO_STATION_HOVERED,
        SIMPLE_TELEPORTATION,
        TWITTER_BUTTON_CLICKED,
        FACEBOOK_BUTTON_CLICKED,
        WIDGET_CLOSED,
        FILESYSTEM_BUTTON_CLICKED,
        FILESYSTEM_FILE_UPLOADED,
        TELEPORT_HOME,
        ENTITY_HUD_OPENED,
        ENTITY_ITEM_SELECTED_FROM_HUD,
        ENTITY_ITEM_PINNED_ON_MAP
    }
    public class TaskComplete
    {
        public int ID { get; set; }
        public TaskType Type { get; set; }

        public TaskComplete(int ID, TaskType type)
        {
            this.ID = ID;
            this.Type = type;
        }

        public static void Notify(int ID, TaskType taskType)
        {
            MessageBroker.Default.Publish(new TaskComplete(ID, taskType));
        }
    }
}
