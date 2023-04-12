using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Telerik.DataSource;
using DOOR.Shared.DTO;
using DOOR.EF.Models;
using DOOR.Shared;
using System.Collections.Generic;
using Telerik.Blazor.Components;
using DOOR.Shared.Utils;
using Newtonsoft.Json;
using DOOR.Client.eNums;
using DOOR.Shared.Exceptions;


namespace DOOR.Client.Services.Common
{
    public class BaseService<E>
    {
        protected HttpClient Http { get; set; }
        public string RestAPI;
        protected string BaseObject;

        protected JsonSerializerOptions options = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNameCaseInsensitive = true
        };

        public BaseService(HttpClient client, String ControllerName)
        {
            Http = client;
            this.RestAPI = $"api/{ControllerName}";
            this.BaseObject = ControllerName;
        }


        public async Task<List<E>> Get()
        {
            return await Http.GetFromJsonAsync<List<E>>($"{RestAPI}/Get{BaseObject}", options);

        }

        public async Task<E> Get(string ID)
        {
            return await Http.GetFromJsonAsync<E>($"{RestAPI}/Get{BaseObject}/{ID}", options);

        }

        public async Task<NotificationModel> Post(E _Item)
        {
            string _ItemJson = JsonConvert.SerializeObject(_Item);

            HttpResponseMessage response = await Http.PostAsJsonAsync($"{RestAPI}/Post{BaseObject}", _ItemJson);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var NotificationModel = MakeNotificationModel($"{BaseObject} Saved",
                1000,
                eNumTelerikThemeColor.success, "save");
                return NotificationModel;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.ExpectationFailed)
            {
                //  I'm using 'ExpectionFailed' to pass back Oracle Errors
                List<OraError> _ValidationResult = JsonConvert.DeserializeObject<List<OraError>>(response.Content.ReadAsStringAsync().Result);
                throw new CustomOraException(_ValidationResult, response.StatusCode);
            }

            throw new Exception($"The service returned with status {response.StatusCode}");
        }

        public async Task<NotificationModel> Put(E _Item)
        {
            string _ItemJson = JsonConvert.SerializeObject(_Item);

            HttpResponseMessage response = await Http.PutAsJsonAsync($"{RestAPI}/Put{BaseObject}", _ItemJson);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var NotificationModel = MakeNotificationModel($"{BaseObject} Saved",
                1000,
                eNumTelerikThemeColor.success, "save");
                return NotificationModel;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.ExpectationFailed)
            {
                //  I'm using 'ExpectionFailed' to pass back Oracle Errors
                List<OraError> _ValidationResult = JsonConvert.DeserializeObject<List<OraError>>(response.Content.ReadAsStringAsync().Result);
                throw new CustomOraException(_ValidationResult, response.StatusCode);
            }

            throw new Exception($"The service returned with status {response.StatusCode}");
        }

        public async Task<NotificationModel> Delete(string _ID)
        {
            HttpResponseMessage response = await Http.DeleteAsync($"{RestAPI}/Delete{BaseObject}/{_ID}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var NotificationModel = MakeNotificationModel($"{BaseObject} Deleted",
                1000,
                eNumTelerikThemeColor.success, "save");
                return NotificationModel;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.ExpectationFailed)
            {
                //  I'm using 'ExpectionFailed' to pass back Oracle Errors
                List<OraError> _ValidationResult = JsonConvert.DeserializeObject<List<OraError>>(response.Content.ReadAsStringAsync().Result);
                throw new CustomOraException(_ValidationResult, response.StatusCode);
            }

            throw new Exception($"The service returned with status {response.StatusCode}");
        }




        public NotificationModel MakeNotificationModel(string strNotificationText,
            int Length,
            eNumTelerikThemeColor eNumTelerikThemeColor,
            string TelerikIcon = "gear")
        {
            var notif = new NotificationModel()
            {
                Icon = TelerikIcon, 
                ShowIcon = true,
                ThemeColor = enumUtil.GetDescription(eNumTelerikThemeColor),
                Text = strNotificationText,
                CloseAfter = Length,
                Closable = Length == 0 ? true : false
            };
            return notif;
        }
    }
}
