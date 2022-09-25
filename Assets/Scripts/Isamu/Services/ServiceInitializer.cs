using System.Collections.Generic;
using UnityEngine;

namespace Isamu.Services
{
    public static class ServiceInitializer
    {
        private static List<Service> services = new();

        [RuntimeInitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            Application.quitting += HandleQuit;
            CreateServices();
        }

        private static void CreateServices()
        {
            services.Add(new ActiveUnitHandler());
            services.Add(new TileSelectionHandler());
            services.Add(new UnitActionHandler());
            services.Add(new NavigationGrid());
        }

        private static void HandleQuit()
        {
            Application.quitting -= HandleQuit;

            for (int i = 0, count = services.Count; i < count; i++)
            {
                services[i].Disable();
            }

            services = null;
        }
    }
}
