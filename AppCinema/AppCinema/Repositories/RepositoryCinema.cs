using AppCinema.Models;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppCinema.Repositories
{
     public class RepositoryCinema
    {

        String uriApi;
        MediaTypeWithQualityHeaderValue headerJson;
        public String token { get; set; }
        public RepositoryCinema()
        {
            uriApi = "https://cinemaparadisoapiasr.azurewebsites.net/";
            headerJson = new MediaTypeWithQualityHeaderValue("application/json");
        }
        /// <summary>
        /// Llamada genérica a la API de usuarios
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="peticion">EL string con la peticion</param>
        /// <returns>El contenido de la llamada</returns>
        private async Task<T> CallApi<T>(String peticion
            , String token)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(this.uriApi);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(headerJson);
                if (token != null)
                {
                    cliente.DefaultRequestHeaders.Add("Authorization", "bearer "
                        + token);
                }
                HttpResponseMessage response =
                    await cliente.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    T datos =
                        await response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(datos, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        /// <summary>
        /// Hace la peticion de login en la API.
        /// </summary>
        /// <param name="user">String. El email del usuario.</param>
        /// <param name="password">String. La password del usuario.</param>
        /// <returns></returns>
        public async Task<String> Login(String user, String password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerJson);
                Login log = new Login(user, password);
                String json = JsonConvert.SerializeObject(log);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/Auth/Login", content);
                if ((int)response.StatusCode != 200)
                    return null;
                else
                {
                    String data = await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(data);
                    return jObject.GetValue("response").ToString();

                }
            }
        }
        /// <summary>
        /// Comprueba si una pelicula está en la lista de un usuario
        /// </summary>
        /// <param name="idMovie">int. El id de la pelicula.</param>
        /// <param name="user">String. </param>
        /// <returns></returns>
        /// 
       
        public async Task<bool> CheckInList(int idMovie, String user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerJson);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                }
                HttpResponseMessage response = await client.GetAsync("api/List/CheckInList?idMovie=" + idMovie + "&user=" + user);
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                    return true;
                else if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                    return false;
                else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                    return false;
                else
                    return false;
            }
            //HttpStatusCode status = await CallApi<HttpStatusCode>("api/List/CheckInList?idMovie=" + idMovie + "&user=" + user);                            
        }
        /// <summary>
        /// Añade una pelicula a la lista del usuario
        /// </summary>
        /// <param name="idMovie">int. El id de la pelicula</param>
        /// <param name="user">String. El email del usuario</param>
        /// <returns></returns>
        public async Task AddMovieToList(int idMovie, String user)
        {
            Lists movie = new Lists();
            movie.IdMovie = idMovie;
            movie.UserEmail = user;
            //context.Lists.Add(movie);
            //context.SaveChanges();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerJson);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                }
                String json = JsonConvert.SerializeObject(movie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/List/AddMovieToList/", content);
            }
        }
        /// <summary>
        /// Elimina una pelicula de la lista de un usuario
        /// </summary>
        /// <param name="idMovie">int. El id de la pelicula</param>
        /// <param name="user">String. El email del usuario</param>
        /// <returns></returns>
        public async Task RemoveMovieFromList(int idMovie, String user)
        {
            Lists movie = new Lists();
            movie.IdMovie = idMovie;
            movie.UserEmail = user;
            //context.Lists.Remove(movie);
            //context.SaveChanges();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerJson);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                }
                String json = JsonConvert.SerializeObject(movie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/List/RemoveMovieFromList/", content);
            }
        }
        /// <summary>
        /// Obtiene la lista de peliculas de un usuario.
        /// </summary>
        /// <param name="user">String. El email del usuario</param>
        /// <returns></returns>
        public async Task<List<Lists>> GetUserList(String user,String token)
        {
            if (Barrel.Current.IsExpired(key: "GetUserList"+user) == false)
            {
                List<Lists> lista = Barrel.Current.Get<List<Lists>>("GetUserList"+user);

                return lista;

            }//SI NO EXISTEN DATOS
            else
            {
                List<Lists> lista = await CallApi<List<Lists>>("api/List/GetUserList?user=" + user, token);
                Barrel.Current.Add(key: "GetUserList" + user 
                        , data: lista, expireIn: TimeSpan.FromHours(23));
                return lista;


            }
        }
        public async Task<Cinephile> GetUser(String user , String token)
        {
            if (Barrel.Current.IsExpired(key: "GetUser") == false)
            {
                Cinephile usuario = Barrel.Current.Get<Cinephile>("GetUser"+user);
                return usuario;
            }//SI NO EXISTEN DATOS
            else
            {
                Cinephile usuario = await CallApi<Cinephile>("api/Cinephile/" + user, token);
                Barrel.Current.Add(key: "GetUser"+user
                        , data: usuario, expireIn: TimeSpan.FromHours(23));
                return usuario;


            }
        }






        public async Task RegisterUser(string email, string pass, string name, string lastName, int? age)
        {
            Cinephile newUser = new Cinephile();
            newUser.Email = email;
            newUser.Password = pass;
            newUser.Name = name;
            newUser.LastName = lastName;
            newUser.Age = age.GetValueOrDefault();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.uriApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerJson);
                String json = JsonConvert.SerializeObject(newUser);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/Cinephile/", content);
            }
        }
    }
}

