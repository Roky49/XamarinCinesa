using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AppCinema.Models;
using AppCinema.ViewModel;

namespace AppCinema.IoC
{
    public class IoCConfiguration
    {
        //DECLARAMOS LA VARIABLE DEL CONTENEDOR
        readonly IContainer container;
        public IoCConfiguration()
        {
            //CREAMOS EL CONSTRUCTOR DE CONTENEDORES
            var builder = new ContainerBuilder();
            //DEBEMOS RESOLVER LAS CLASES PARA NUESTRO
            //CONTENEDOR PARA LAS DEPENDENCIAS
            //AL REGISTRAR LOS TIPOS, PODEMOS INDICAR
            //EL NUMERO DE INSTANCIAS
            //POR DEFECTO, REALIZA UNA INSTANCIA POR
            //CADA PETICION
            builder.RegisterType<SessionService>().SingleInstance();
            builder.RegisterType<ViewModelMasterPrincipal>();

            //CONTRUIMOS EL CONTENEDOR
            this.container = builder.Build();
        }
        //CREAMOS UNA PROPIEDAD QUE NOS DEVUELVA
        //LA INSTANCIA DE SESSION SERVICE
        //DICHA INSTANCIA NOS LA RESOLVERA EL CONTENEDOR
        public SessionService SessionService
        {
            get { return this.container.Resolve<SessionService>(); }
        }

        public ViewModelMasterPrincipal ViewModelMasterPrincipal
        {
            get { return container.Resolve<ViewModelMasterPrincipal>();
            }
        }

    }
}

