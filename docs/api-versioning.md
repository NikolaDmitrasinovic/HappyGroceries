# API Versioning

## Summary

The API uses ASP.NET API versioning to preserve backward compatibility as modules evolve.

## Current approach

- Package: `Asp.Versioning.Mvc`
- Versioning style: URL segment versioning
- Default API version: `1.0`
- Current versioned route format: `api/v{version}/...`

## Current status

- Inventory endpoints are exposed as API v1
- The current Products controller supports version `1.0`

Example route:

`/api/v1/inventory/products`

## Notes

The purpose of versioning is to allow future breaking API changes without modifying the existing v1 contract.

For example, if stock operations are redesigned in the future, the new contract should be introduced as a new API version rather than replacing v1 behavior.