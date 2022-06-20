using CoreService;
using Domain.DTO.Charge;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebPanel.Controllers
{
    public class ChargeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChargeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult ShaparakPaymnet(string id)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(id);
            var plainText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var payObject = JsonSerializer.Deserialize<Dictionary<string, object>>(plainText);

            //ViewBag.Username = payObject["Username"];
            //ViewBag.Amount = payObject["Amount"];

            var res = new ShaparakPaymentDTO()
            {
                UserId = payObject["UserId"].ToString().ToLong(),
                Amount = payObject["Amount"].ToString().ToLong(),
                Username = payObject["Username"].ToString(),
                CVV2 = null
            };

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> ShaparakPaymnet(ShaparakPaymentDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await _unitOfWork._paymentDocument.InsertPayDoc(model))
                {
                    if (await _unitOfWork._user.IncreasePassengerWalletAmount(model.Amount, model.UserId))
                    {
                        _unitOfWork.Complete();
                        return RedirectToAction("index", "passenger");
                    }

                }
            }
            return View(model);
        }
    }
}
