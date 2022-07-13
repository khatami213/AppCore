using CoreService;
using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebPanel.SignalR
{
    public class RequestTripHub : Hub
    {
        private static IUnitOfWork _unitOfWork;
        public async override Task OnConnectedAsync()
        {
            try
            {
                var userId = Context.User.Claims.FirstOrDefault(r => r.Type == "UserId").Value.ToLong();
                var userType = Context.User.Claims.FirstOrDefault(r => r.Type == "UserType").Value.ToInt();
                _unitOfWork = (IUnitOfWork)Context.GetHttpContext().RequestServices.GetService(typeof(IUnitOfWork));

                var user = await _unitOfWork._user.GetByID(userId);

                if (user == null)
                    throw new Exception("User not Found");

                if (user.UserType != userType)
                    throw new Exception("User is invalid");

                switch (userType)
                {
                    case (int)Enums.UserType.Passenger:
                        await Groups.AddToGroupAsync(Context.ConnectionId, "Passengers");
                        break;
                    case (int)Enums.UserType.Driver:
                        await Groups.AddToGroupAsync(Context.ConnectionId, "Drivers");
                        break;
                }

            }
            catch (System.Exception ex)
            {
                await Clients.Caller.SendAsync("Notification", ex.Message);
            }

        }

        public async void SendRequest(long userId, double fee)
        {
            try
            {
                var user = await _unitOfWork._user.GetByID(userId);

                if (user == null)
                    throw new Exception("User not found");

                await Clients.Group("Drivers").SendAsync("RecieveRequest", user.Username, fee);

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Notification", ex.Message);
            }

        }

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{

        //}
    }
}
