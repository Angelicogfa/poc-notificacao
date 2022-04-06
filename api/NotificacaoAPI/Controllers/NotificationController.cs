using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificacaoAPI.Authentication;
using NotificacaoAPI.Bus;
using NotificacaoAPI.Context;
using NotificacaoAPI.Model;
using NotificacaoAPI.Requests;

namespace NotificacaoAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize(AuthenticationSchemes = SingleSignOnSchemaConstants.SingleSignOnAuthSchema)]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IUow uow, [FromServices] IBus<Notification> bus, [FromBody] SendNotification message)
        {
            Notification notification = new(message.Message, message.Destination, message.Sender, message.UrlRedirect);
            await uow.Notifications.Add(notification);
            await uow.Commit();

            await bus.SendAsync(notification, CancellationToken.None);

            return Ok(notification);
        }
    }
}
