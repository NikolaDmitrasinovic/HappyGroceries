namespace Inventory.Products.Features.SetProductThreshold;

public record SetProductThresholdRequest(Guid Id, int Threshold);
public record SetProductThresholdResponse(Guid Id);
