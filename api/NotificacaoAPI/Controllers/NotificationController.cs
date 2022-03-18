using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificacaoAPI.Bus;
using NotificacaoAPI.Context;
using NotificacaoAPI.Hub;
using NotificacaoAPI.Model;
using NotificacaoAPI.Requests;

namespace NotificacaoAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] IUow uow, [FromServices] IBus bus, [FromBody] SendNotification message)
        {
            Notification notification = new(message.Message, message.Destination, message.Sender, message.UrlRedirect);
            await uow.Notifications.Add(notification);
            await uow.Commit();

            await bus.Send(notification);

            return Ok(notification);
        }

        [HttpGet("{destiny}")]
        public async Task<IActionResult> Get([FromServices] IUow uow, string destiny, bool includeRead = false, int? top = null)
        {
            return Ok(await uow.Notifications.GetByDestiny(destiny, includeRead, top));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromServices] IUow uow, Guid id)
        {
            Notification? notification = await uow.Notifications.Get(id);
            if (notification == null)
                return NotFound();

            notification.SetAsRead();
            notification = await uow.Notifications.Update(notification);
            await uow.Commit();

            return Ok(notification);
        }

        [HttpPost("/hub")]
        public async Task<IActionResult> SendHub([FromServices] IHubContext<NotificationHub> hub)
        {
            await hub.Clients.Group("SignalR Users").SendAsync("broadcastMessage", "Guilherme", "Ola");
            return Ok();
        }
    }
}
