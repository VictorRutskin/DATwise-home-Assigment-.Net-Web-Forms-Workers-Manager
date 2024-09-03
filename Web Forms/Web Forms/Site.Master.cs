using BL.Interfaces;
using BL.Services;
using Common.ConfigurationHandler;
using DAL.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Web.UI;

namespace Web_Forms
{
    public partial class SiteMaster : MasterPage
    {
        public myDbContext DbContext { get; private set; }
        public IServiceEmployee ServiceEmployee { get; private set; }
        public IServiceLogger ServiceLogger { get; private set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<myDbContext>();
            optionsBuilder.UseSqlServer(ConfigurationHandler.GetConnectionString());
            DbContext = new myDbContext(optionsBuilder.Options);

            ServiceEmployee = new ServiceEmployee(DbContext);
            ServiceLogger = new ServiceLogger(DbContext, Server.MapPath(ConfigurationHandler.GetLogFilePath()));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public void InitializeServices(ref IServiceEmployee serviceEmployee,ref IServiceLogger serviceLogger)
        {
            serviceEmployee = this.ServiceEmployee;
            serviceLogger = this.ServiceLogger;
        }
    }
}