
using ErrorOr;
using MediatR;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Subscriptions.CreateSubscription;
public record CreateSubscriptionCommand(string SubscriptionType, Guid AdminId) : IRequest<ErrorOr<Subscription>>;