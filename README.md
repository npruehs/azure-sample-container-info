# azure-sample-container-info

Example architecture of a logistics web application in the Microsoft Azure cloud.

## Goal

Building a REST API that provides information about the current status of containers, such as their ISO code, or whether
they've arrived or departed.

## Characteristics

`azure-sample-container-info` is deployed to Microsoft Azure, which is a _public cloud_: There's no upfront costs for
providing the infrastructure for deploying the application. The applications can be quickly provisioned and
deprovisioned if necessary, and the owning organization pays only for what they use.

It's entirely based on _Platform as a Service_: The cloud provider maintains the physical infrastructure,
physical security, and connection to the internet. They also maintain the operating systems, middleware,
development tools, and business intelligence services that make up the cloud solution. In turn, the owning organization
is responsible for the data stored in the cloud, and who can access it, and how (_shared responsibility_).

## Benefits

* **High availability**: The application is available when needed.
* **Scalability**: Compute and storage resources are automatically adjusted to meet demand.
* **Reliability**: The application recovers from failures and continues to function.

## Management Infrastructure

While you can develop the application locally (by running the Azure Functions locally and storing data in
[Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Ctable-storage)),
we recommend setting up two different subscriptions:

* Staging
* Production

Usually, Azure Functions allow you to use
[deployment slots](https://learn.microsoft.com/en-us/azure/azure-functions/functions-deployment-slots?tabs=azure-portal)
for staging your application. However, this forces you to put your staging environment in the same subscription as your
production environment, which violates recommended billing and access control boundaries. You can still use deployment
slots for prewarming before going live with a new version of your application, as well as easy fallbacks in case of 
live issues.

## Physical infrastructure

The application does not require any zone redundancy for the function app or storage:

* Accessing the data is not mission-critical for the organization - containers can still be accepted at and delivered
from a facility, even if the container info API is unavailable.
* The data can easily be restored from its source if necessary.

Storage is still relying on _locally redundant storage (LRS)_, which replicates the data three times within a single
data center, providing at least 11 nines of durability (99.999999999%) of objects over a given year. It protects the
data against unexpected server rack and drive failures, and is still the the lowest-cost redundancy option.

## Compute Services

The application compute service is based on _Azure Functions_, which allows us to not worry about the underlying
platform or infrastructure. They're automatically invoked when new data is available at the input queue, or an HTTP 
request is made to access the data. Azure Functions allows the application to scale automatically based on demand,
which can vary as global supply chains have proven to be unpredictable in the recent past. 

## Identity, Access & Security

The Function App itself is protected by an API Management service instance, and not publicly accessible. API Management
in turn provides simple, but weak protection in form of
[API keys](https://learn.microsoft.com/en-us/azure/api-management/api-management-subscriptions), as well as stronger
options such as [securing the API through Microsoft Entra ID](https://learn.microsoft.com/en-us/azure/api-management/api-management-howto-protect-backend-with-aad).


