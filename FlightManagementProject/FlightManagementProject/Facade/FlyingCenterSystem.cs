﻿using FlightManagementProject.DAO;
using FlightManagementProject.DAO.Interfaces;
using FlightManagementProject.Poco_And_User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace FlightManagementProject.Facade
{
    public class FlyingCenterSystem
    {
        static private FlyingCenterSystem _instance;
        static object key = new object();
        static private LoginService ls = new LoginService();
        static private IMaintenanceDAO _backgroundDAO = new MaintenanceDAOMSSQL();

        private FlyingCenterSystem()
        {
            System.Timers.Timer t = new System.Timers.Timer(FlyingCenterConfig.TIME_FOR_THREAD_HISTORY);
            t.AutoReset = true;
            t.Enabled = true;
            t.Elapsed += UpdateFlightsAndTickets;
        }

        // Function To Declare _instance Field, Only If It = null.
        static public FlyingCenterSystem GetInstance()
        {
            if (_instance == null)
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlyingCenterSystem();
                    }
                }
            }
            return _instance;
        }

        // Only Here Is Place To Get Any Token Or Facade(Because It's Singelton)
        static public void GetUserAndFacade(string userName, string password, out ILogin token, out FacadeBase facade)
        {
            ls.TryLogin(userName, password, out ILogin myToken, out FacadeBase myFacade);
            token = myToken;
            facade = myFacade;
        }

        void UpdateFlightsAndTickets(object sender, ElapsedEventArgs e)
        {
            _backgroundDAO.UpdateTicketsAndFlightsHistory();
        }
    }
}
