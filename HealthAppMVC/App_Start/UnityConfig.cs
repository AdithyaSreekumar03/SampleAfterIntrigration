
using HealthAppMVC.Services.Implementation;
using HealthAppMVC.Services.Interface;
using System;
using System.Net.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace HealthAppMVC
{


    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44301/api/")
            };
            container.RegisterInstance<HttpClient>(httpClient);
            container.RegisterType <IPatientService, PatientService>();
            container.RegisterType<IDoctorService, DoctorService>();
            container.RegisterType<IAppointmentService,AppointmentService>();
            container.RegisterType <IHealthRecordService,HealthRecordService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}