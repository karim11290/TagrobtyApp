using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuizbeePlus.Data;
using QuizbeePlus.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Services
{
    public class UsersService
    {
        #region Define as Singleton
        private static UsersService _Instance;

        public static UsersService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UsersService();
                }

                return (_Instance);
            }
        }

        private UsersService()
        {
        }
        #endregion
        
        public UsersSearch GetUsersWithRoles(string searchTerm, int pageNo, int pageSize)
        {
            using (var context = new QuizbeeContext())
            {
                var search = new UsersSearch();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    search.Users = context.Users
                                        .Include(u => u.Roles)
                                        .OrderByDescending(x => x.RegisteredOn)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .Select(x => new UserWithRoleEntity() { User = x, Roles = x.Roles.Select(r => context.Roles.Where(role => role.Id == r.RoleId).FirstOrDefault())
                                        .ToList() }).ToList();

                    search.TotalCount = context.Users.Count();
                }
                else
                {
                    search.Users = context.Users
                                        .Where(u => u.UserName.ToLower().Contains(searchTerm.ToLower()))
                                        .Include(u => u.Roles)
                                        .OrderByDescending(x => x.RegisteredOn)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .Select(x => new UserWithRoleEntity() { User = x, Roles = x.Roles.Select(r => context.Roles.Where(role => role.Id == r.RoleId).FirstOrDefault())
                                        .ToList() }).ToList();

                    search.TotalCount = context.Users
                                        .Where(u => u.UserName.ToLower().Contains(searchTerm.ToLower())).Count();
                }

                return search;
            }
        }
        
        public UserWithRoleEntity GetUserWithRolesByID(string userID)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Users
                                    .Where(x => x.Id == userID)
                                    .Include(u => u.Roles)
                                    .Select(x => new UserWithRoleEntity()
                                    {
                                        User = x,
                                        Roles = x.Roles.Select(r => context.Roles.Where(role => role.Id == r.RoleId).FirstOrDefault()).ToList()
                                    }).FirstOrDefault();
            }
        }


        public string GetToken(string userName, string password)
        {
            var accessToken = string.Empty;

            var keyvalues = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")};

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44394/" + "Token");

            request.Content = new FormUrlEncodedContent(keyvalues);
            var client = new HttpClient();
            var response = client.SendAsync(request).Result;
            using (HttpContent content = response.Content)
            {
                var json = content.ReadAsStringAsync();
                JObject JwtDynamic = JsonConvert.DeserializeObject<dynamic>(json.Result);
                accessToken = JwtDynamic.Value<string>("access_token");
                var Username = JwtDynamic.Value<string>("userName");
                var accessTokenExpiration = JwtDynamic.Value<DateTime>(".expires");
                
            }

            return accessToken;
        }



    }
}
