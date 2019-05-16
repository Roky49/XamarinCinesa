using AppCinema.View;
using AppCinema.ViewModel;
using Autofac;
using System;
using System.Collections.Generic;

using System.Text;

namespace AppCinema.Base
{
     public class ViewModelLocator
    {
        readonly IContainer _container;
        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
           
            if (_container != null) { _container.Dispose(); }
            _container = builder.Build();
        }

        public ViewModelLogin ViewModelLogin { get { return _container.Resolve<ViewModelLogin>(); } }
        // para resolver la sesion donde keramos los  viewmodel

        //public DoctoresViewModel DoctoresViewModel { get { return _container.Resolve<DoctoresViewModel>(); } }
        //public DoctorViewModel DoctorViewModel { get { return _container.Resolve<DoctorViewModel>(); } }

    }
}
