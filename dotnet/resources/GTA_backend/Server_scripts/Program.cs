using GTANetworkAPI;
using GTANetworkInternals;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Diagnostics.Tracing;
using Npgsql;


namespace mygaymode.Server_scripts
{
    public class Program : Script
    {
        static void Main()
        {
            int a;
            Environment.SetEnvironmentVariable("COREHOST_TRACE", "1");
        }
        public Program()
        {

        }

        [ServerEvent(Event.ResourceStart)]
        public void myResourceStart()
        {
            NAPI.World.RemoveIpl("rc12b_destroyed");
            NAPI.World.RemoveIpl("rc12b_default");
            NAPI.World.RemoveIpl("RC12B_Fixed");
            NAPI.World.RemoveIpl("RC12B_HospitalInterior_lod");
            NAPI.World.RemoveIpl("bh1_13_strm_0");
            var db = SQL_process.CreateDB();
            NAPI.Util.ConsoleOutput(SQL_process.Connection_String);
            using (var a = new NpgsqlConnection(SQL_process.Connection_String))
            {
                a.Open();
            }

        }

        [Command("tp")]
        public void TP(Player sender, double x, double y, double z)
        {
            sender.Position = new Vector3(x, y, z);

        }
        [Command("veh")]
        public void Veh_spawn(Player sender, string name)
        {
            try
            {
                var v = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(name), sender.Position, 0, 1, 2);
                sender.SetIntoVehicle(v, 0);
            }
            catch (Exception e)
            {
                sender.SendChatMessage("Error create: " + e.Message);
            }
        }

        [RemoteEvent("teleportWaypoint")]
        public void teleport_waypoint(Player sender, double X, double Y, double Z)
        {
            Z = Height.GetHeightAtXY((float)X, (float)Y);
            sender.Position = new Vector3(X, Y, Z);
            sender.SendChatMessage("" + X + " " + Y + " " + Z);
        }

        [Command("test")]
        public void testJScommand(Player sender)
        {
            sender.TriggerEvent("test");
        }
        [Command("weapon")]
        public void GiveWeapon(Player sender, string w_name, int ammo)
        {
            sender.GiveWeapon(NAPI.Util.WeaponNameToModel(w_name), ammo);
        }
        #region OldHuinya
        //[Command("mine")]
        //public void PlaceMine(Player sender, float MineRange = 10f)
        //{
        //    var pos = NAPI.Entity.GetEntityPosition(sender);
        //    var playerDimension = NAPI.Entity.GetEntityDimension(sender);

        //    var prop = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_bomb_01"), pos - new Vector3(0, 0, 1f), new Vector3(), 255, playerDimension);

        //    var shape = NAPI.ColShape.CreateSphereColShape(pos, 10);
        //    shape.Dimension = playerDimension;

        //    bool mineArmed = false;
        //    sender.GiveWeapon(WeaponHash.Minigun, 1000);
        //    var p = NAPI.Ped.CreatePed(PedHash.Abigail, sender.Position + new Vector3(1, 0, 0), 100f);
        //    NAPI.Vehicle.CreateVehicle(0x810369E2, sender.Position + new Vector3(15, 0, 0), 0, 1, 2, "CUM");
        //    shape.OnEntityEnterColShape += (s, ent) =>
        //    {
        //        if (!mineArmed) return;
        //        NAPI.Explosion.CreateOwnedExplosion(sender, ExplosionType.ProxMine, pos, 1f, playerDimension);
        //        NAPI.Entity.DeleteEntity(prop);
        //        NAPI.ColShape.DeleteColShape(shape);
        //    };

        //    shape.OnEntityExitColShape += (s, ent) =>
        //    {
        //        if (ent == sender.Handle && !mineArmed)
        //        {
        //            mineArmed = true;
        //            NAPI.Notification.SendNotificationToPlayer(sender, "Mine has been ~r~armed~w~!", true);
        //            //NAPI.Marker.CreateMarker(1, new Vector3(-425.517, 1123.620, 325.8544), new Vector3(), new Vector3(), 30, new Color(0, 255, 255));
        //            NAPI.Explosion.CreateExplosion(ExplosionType.Train, new Vector3(-425.517, 1123.620, 325.8544));
        //        }
        //    };
        //}
        #endregion

        [ServerEvent(Event.PlayerConnected)]
        public void Connected_player(Player sender)
        {

        }
    }
}