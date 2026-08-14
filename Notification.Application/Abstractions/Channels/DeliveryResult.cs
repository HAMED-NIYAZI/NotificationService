namespace Notification.Application.Abstractions.Channels
{
    public sealed record DeliveryResult
    {
        public bool Success { get; init; }

        public bool RequiresAcknowledgement { get; init; }

        public string? ProviderMessageId { get; init; }

        public string? Error { get; init; }

        public static DeliveryResult Succeeded(
            bool requiresAcknowledgement = true,
            string? providerMessageId = null)
        {
            return new DeliveryResult
            {
                Success = true,
                RequiresAcknowledgement =
                    requiresAcknowledgement,
                ProviderMessageId =
                    providerMessageId
            };
        }

        public static DeliveryResult Failed(
            string error)
        {
            return new DeliveryResult
            {
                Success = false,
                Error = error
            };
        }
    }
}



 
