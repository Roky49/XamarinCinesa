using Autofac;
using System;
using System.Collections.Generic;

using System.Text;

namespace AppCinema.IoC
{
    public class IoConfiguration
    {

        private IContainer container;

        public IoConfiguration()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //registramos todos los tipos de inyeciones de dependencias o clases que necesitamos que nos devuelva el contenedor 
            //clases comunicadas entre otras
            //builder.RegisterType<ModelViewApuesta>();
          
            this.container = builder.Build();



        }

        //public ModelViewApuesta ModelViewApuest
        //{
        //    get
        //    {
        //        return this.container.Resolve<ModelViewApuesta>();
        //    }
        //}

    }

}

