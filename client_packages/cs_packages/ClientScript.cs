using System;
using RAGE;

namespace mygaymode
{
    using RAGE;
    public class Island_create : Events.Script
    {
        bool g_bIslandLoaded = false;
        public Island_create() {
            RAGE.Input.IsDown(RAGE.Ui.VirtualKeys.F3);
            RAGE.Input.Bind(RAGE.Ui.VirtualKeys.F3, true, new Action(() =>
            {
                g_bIslandLoaded = !g_bIslandLoaded;
                RAGE.Game.Invoker.Invoke(0x9A9D1BA639675CF1, "HeistIsland", g_bIslandLoaded);
            }));
        }
 
    }

    public class c_Waypoint : Events.Script
    {
        public c_Waypoint()
        {
            Events.OnPlayerCreateWaypoint += OnWaypointCreated1;
        }

        public void OnWaypointCreated1(Vector3 vec)
        {
            Chat.Output("Player successfully spawned"); //Этот метод выводит сообщение в дефолтный чат
            Events.CallRemote("teleportWaypoint", vec.X, vec.Y, vec.Z);
        }
    }
}
