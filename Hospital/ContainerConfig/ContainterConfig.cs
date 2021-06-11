using Autofac;
using Hospital.Injection;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using Hospital.Services;
using Hospital.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ContainerConfig
{
    public static class ContainterConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AppointmentFileRepository>().As<IAppointmentRepository>();
            builder.RegisterType<HospitalTreatmentFileRepository>().As<IHospitalTreatmentRepository>();
            builder.RegisterType<HospitalTreatmentService>().As<IHospitalTreatmentService>();
            builder.RegisterType<ServiceInjection>().As<IServiceInjection>();
            return builder.Build();
        }
    }
}
