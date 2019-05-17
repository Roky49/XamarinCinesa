using AppCinema.Base;
using AppCinema.IoC;
using AppCinema.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppCinema
{
    public partial class App : Application
    {
        // para guardar la sesion del token 
        private static IoConfiguration _locator;
        public static IoConfiguration Locator { get { return _locator = _locator ?? new IoConfiguration(); } }
       //para llamrlo desde otros lados en el comand
        // DoctorViewModel viewmodel =                     App.Locator.DoctorViewModel; 


        public App()
        {
            InitializeComponent();

            MainPage = new MasterPrincipal();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
