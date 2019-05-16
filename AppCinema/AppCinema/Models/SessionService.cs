using System;
using System.Collections.Generic;
using System.Text;

namespace AppCinema.Models
{
    public class SessionService
    {
        public List<Cinephile> Datos { get; set; }

        public SessionService()
        {
            this.Datos = new List<Cinephile>();
        }
    }
}
