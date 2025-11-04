using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using Prometheus;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private static readonly Counter OrderPlacedCounter = Metrics.CreateCounter("order_placed_total", "Total orders placed");
        private static readonly Histogram OrderLatency = Metrics.CreateHistogram("order_visibility_latency_seconds", "Order visibility latency", new HistogramConfiguration { Buckets = Histogram.LinearBuckets(0.1, 0.1, 10) });

        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            using var latency = OrderLatency.NewTimer();
            decimal fee = CalculatePartnerFee(order.TotalValue);

            OrderPlacedCounter.Inc();

            return Ok(new { OrderId = order.Id, Fee = fee });
        }

        private decimal CalculatePartnerFee(decimal value)
        {
            if (value <= 100) return value * 0.06m;
            if (value >= 1000) return value * 0.03m;
            decimal percentage = 0.06m - ((value - 100) * (0.03m / 900));
            return value * percentage;
        }
    }
}
