# FleetGraphAPI
This repository contains the implementation of FleetGraphAPI, a GraphQL-based API for managing vehicles and geofencing in a fleet management system.

## Main Features
### 1. Queries
The API supports various queries to retrieve vehicle data. Some of the implemented queries include:

- Get all vehicles: Retrieve a list of all vehicles in the fleet.
- Get vehicle by ID: Retrieve a specific vehicle based on its ID.
- Filtering and pagination: Query vehicles based on specific criteria, such as make, model, or year, with support for pagination.

To test these queries, you can execute GraphQL requests at http://localhost:12000/graphql/. Here are some sample queries:

GetVehicleById
Retrieve a vehicle by its ID.

```
query GetVehicleById {
  vehicleById(id: "VmVoaWNsZQppMQ==") {
    make
    model
    year
    vIN
    currentLocation
    fuelLevel
  }
}
```

GetVehicles
Retrieve a list of vehicles based on specific filters.

```
query GetVehicles {
  vehicles(where: { model: { eq: "F150" } }) {
    edges {
      node {
        make
        model
      }
    }
  }
}
```

### 2. Mutations
The API provides mutation operations to perform CRUD operations on vehicles. Some of the implemented mutations include:

- AddVehicle: Create a new vehicle with the provided details.
- UpdateVehicleStatus: Update the location and fuel level of a vehicle.
- Here are sample mutation requests:

AddVehicle: Create a new vehicle.

```
mutation AddVehicle {
  addVehicle(
    input: {
      make: "Ford",
      model: "Mustang",
      year: 2018,
      vIN: "123456780"
    }
  ) {
    vehicle {
      id
    }
  }
}
```

UpdateVehicleStatus: Update the location and fuel level of a vehicle.

```
mutation UpdateVehicleStatus {
  updateVehicleStatus(
    input: {
      location: "Austin, TX",
      fuelLevel: 4,
      vehicleId: 1
    }
  ) {
    vehicle {
      id
    }
  }
}
```

### 3. Subscriptions
The API supports subscriptions via websockets, allowing clients to receive real-time updates. One of the implemented subscriptions is OnVehicleStatusUpdated, which notifies clients when a vehicle's status (location and fuel level) is updated.

To subscribe to vehicle status updates, you can use the following subscription:

```
subscription OnVehicleStatusUpdated {
  onVehicleStatusUpdated {
    currentLocation
    fuelLevel
  }
}
```

This subscription will provide real-time notifications whenever a vehicle's location or fuel level is updated.

## Technologies Used
- GraphQL
- HotChocolate
- Entity Framework Core
- ASP.NET Core
- WebSockets
