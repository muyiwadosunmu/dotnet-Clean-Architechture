using ErrorOr;
using MediatR;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Subscriptions.CreateSubscription;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription
{
    // Command we are handling and the Response Type
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        // private readonly IUnitOfWork _unitOfWork;
        public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository //  IUnitOfWork unitOfWork
         )
        {
            _subscriptionsRepository = subscriptionsRepository;
            // _unitOfWork = unitOfWork;

        }
        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            // Create a subscription
            var subscription = new Subscription { Id = Guid.NewGuid(), SubscriptionType = request.SubscriptionType };
            // Add it to the Database
            await _subscriptionsRepository.AddSubscriptionAsync(subscription);
            // await _unitOfWork.CommitChangesAsync();
            // Return subscription
            return subscription;
        }
    }
}