﻿using System;
using System.Collections.Generic;

namespace Alpaca.Markets
{
    /// <summary>
    /// Encapsulates request parameters for <see cref="AlpacaTradingClient.PatchOrderAsync(ChangeOrderRequest,System.Threading.CancellationToken)"/> call.
    /// </summary>
    public sealed class ChangeOrderRequest : Validation.IRequest
    {
        /// <summary>
        /// Creates new instance of <see cref="ChangeOrderRequest"/> object.
        /// </summary>
        /// <param name="orderId">Server side order identifier.</param>
        public ChangeOrderRequest(Guid orderId)
        {
            OrderId = orderId;
        }

        /// <summary>
        /// Gets server side order identifier.
        /// </summary>
        public Guid OrderId { get; }

        /// <summary>
        /// Gets or sets updated order quantity or <c>null</c> if quantity is not changed.
        /// </summary>
        public Int64? Quantity { get; set; }

        /// <summary>
        /// Gets or sets updated order duration or <c>null</c> if duration is not changed.
        /// </summary>
        public TimeInForce? Duration { get; set; }

        /// <summary>
        /// Gets or sets updated order limit price or <c>null</c> if limit price is not changed.
        /// </summary>
        public Decimal? LimitPrice { get; set; }

        /// <summary>
        /// Gets or sets updated order stop price or <c>null</c> if stop price is not changed.
        /// </summary>
        public Decimal? StopPrice { get; set; }

        /// <summary>
        /// Gets or sets updated client order ID or <c>null</c> if client order ID is not changed.
        /// </summary>
        public String? ClientOrderId { get; set; }

        IEnumerable<RequestValidationException> Validation.IRequest.GetExceptions()
        {
            ClientOrderId = ClientOrderId?.ValidateClientOrderId();

            if (Quantity <= 0)
            {
                yield return new RequestValidationException(
                    "Order quantity should be positive value.", nameof(Quantity));
            }
        }
    }
}
