using Microsoft.AspNetCore.Mvc;
using MediatR;
using GymManagement.Contracts.Subscriptions;
using GymManagement.Application.Subscriptions.CreateSubscription;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;

namespace GymManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase

    {

        private readonly ISender _mediator;

        public SubscriptionsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {
            var command = new CreateSubscriptionCommand(
                request.SubscriptionType.ToString(), request.AdminId);
            var createSubscriptionResult = await _mediator.Send(command);

            // These code is notable of packages similar to ErrorOr
            // N.B -> Similar to the MatchFirst we have Match which we can list an array of items we want to return e.g errors
            return createSubscriptionResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
                error => Problem()
            );

            // Same as below
            // if (createSubscriptionResult.IsError)
            // {
            //     return Problem();
            // }
            // var response = new SubscriptionResponse(createSubscriptionResult.Value, request.SubscriptionType);
            // return Ok(response);

        }

        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);

            var getSubscriptionsResult = await _mediator.Send(query);

            return getSubscriptionsResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    Enum.Parse<SubscriptionType>(subscription.SubscriptionType))),
                error => Problem());
        }
    }
}