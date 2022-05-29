using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService
{
    public static class PermisionManager
    {

        #region Security
        public const string Security_Roles_HttpGet = "5B34CAE7-3A4B-448A-980C-FBC41F7772F3";
        public const string Security_CreateRole_HttpGet = "D5B0BFCE-7BD4-46AF-9857-6ED2D9C34081";
        public const string Security_CreateRole_HttpPost = "6461A843-BF60-4A7F-ADC8-2140E167A0A3";
        public const string Security_EditRole_HttpGet = "B16F1F63-AB1D-42BE-98A2-8338E6C76986";
        public const string Security_EditRole_HttpPost = "ED3A2DD9-8B54-450E-9306-CE1E25042402";
        public const string Security_DeleteRole_HttpGet = "35929923-F2C3-430D-A524-EA438127BA44";
        #endregion

        public static List<KeyValuePair<string, string>> GetPrmisions()
        {
            var type = typeof(PermisionManager);
            var fileds = type.GetFields();
            var permisions = new List<KeyValuePair<string, string>>();

            foreach (var item in fileds)
            {
                var value = item.GetValue(type);
                var title = item.Name.Replace("_", " ");
                permisions.Add(new KeyValuePair<string, string>(title, value.ToString()));
            }

            return permisions;

        }

        public static async Task SetPermisions(IUnitOfWork context)
        {
            var databasePermisions = await context._permision.GetAll();
            var newPermisions = new List<PermisionDomain>();
            var res = PermisionManager.GetPrmisions();
            foreach (var item in res)
            {
                if (!databasePermisions.Any(r => r.Value == item.Value))
                    newPermisions.Add(new PermisionDomain()
                    {
                        Title = item.Key,
                        Value = item.Value,
                    });
            }

            if (await context._permision.AddRange(newPermisions))
            {
                context.Complete();
            }

        }


    }
}
