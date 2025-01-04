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
* **Scalability**: Resources are automatically adjusted to meet demand.
* **Reliability**: The application recovers from failures and continues to function.
