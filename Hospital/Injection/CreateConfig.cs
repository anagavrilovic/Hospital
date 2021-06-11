using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Injection
{
    public class CreateConfig
    {
        public CreateConfig()
        {
            var container = ContainerConfig.ContainterConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IServiceInjection>();
                app.GetHospitalTreatmentService();
            }
        }
    }
}
