# Push Campaign System

This is a project requested as part of the In Loco screening process.  The objective of this project is to manage push notifications according to ad campaigns and device geolocalization (visits) reports. The details and requirements are described in the specification document.

## Code Design

The system is composed of the following modules, in the order of the data flow:

### RESTful WebAPI

The campaigns and visits are loaded in JSON format through a simple RESTful API, made with ASP.NET Core 3.0 This module contains all the logic of routes, controllers, data models, and data managers. Data managers work as services modules, handling other tools at a high level of abstraction, connecting campaign indexers and retrievers, and data stores.

The configuration of managers and tools are made via Dependency Injection. For example, there are versions of simple managers, which operate over local mocks, as well as distributed versions of managers that delegate part of the processing to separate service workers. The distributed managers communicate with service workers through the message queue module, to operate asynchronously.

Swagger is available in the root URI, for manual ease of use.

### Index Campaign Service Worker

This is a standalone service that gets posted campaigns from his queue and indexes its places on a fast in-memory data store, handled by the caching module. The service is hooked to the queue in an asynchronous mode, allowing for more throughput. In case of need, the service can be scaled horizontally.

### Push Campaign Service Worker

When a visit is posted, this is the service worker responsible for fetching the matched push campaigns from the index data store and send to the push notification providers, according to what is specified in each campaign. The worker sends visits for a default provider in case of not finding targeted campaigns. The same way the other workers, this is a standalone service enabled for horizontal scaling.

### Caching Module

This module is responsible for the details of the initialization and handling of the index data store. The chosen tool for this project is REDIS, for being a good fit for this responsibility. Although it is an in-memory data store, it is possible to configure disk persistence, in case of not operating with a database.

### Message Queue Module

This module handles the details of reading and writing to queues, as well as serializing and deserializing objects. The tool used for this is RabbitMQ, as it serves well the module objective.

### Push Notification Providers Module

This module abstracts the implementation details of the push notification providers. In this project, all providers simulate their processing printing messages only.

### Domain Module

This is the reference module for all the other projects. It defines interfaces and models necessary for projects interoperability, and a base notification structure for better handling validations and some exceptions.

### Tests Project

This is a basic testing project, where the WebAPI could be tested with local managers and informed sample data. The testing of distributed versions was not implemented in time.

## Possible additional works

- Authentication/Authorization services integration;
- Application monitoring integration and better log support;
- Long-term database integration, for data analytics;
- Rich domain entities;
- Testing of distributed manager versions;
- Organization of literal strings in resources;


## Pre-requisites

1. You will need Visual Studio 2019 on Windows 10.

2. You will need the .NET Core SDK 3.0.101 installed.

3. You will need Git.


## Get the code

Clone the source code    

```
git clone https://github.com/denysfarias/push-campaign-system.git
```


## Build solution

1. Open the ```PushCampaignSystem\PushCampaignSystem.sln``` file.

2. In ```PushCampaignSystem\WebApi\Startup.cs```, configure the Dependency Injection of managers for local ou distributed versions:

    - Local

    ```
    services.AddSingleton<ICampaignManager, CampaignManager>();
                services.AddSingleton<IVisitManager, VisitManager>();
    ```

    - Distributed

    ```
    services.AddSingleton<ICampaignManager, DistributedCampaignManager>();
                services.AddSingleton<IVisitManager, DistributedVisitManager>();
    ```

3. Adjust verbosity of workers in ```PushCampaignSystem\IndexCampaignWorker\Program.cs``` and ```PushCampaignSystem\PushCampaignWorker\Program.cs```, setting up the const variable ```VERBOSE```.

4.  Set up the Redis server IP address in ```PushCampaignSystem\Caching\Configurations\GeneralData.cs```, variable ```SERVER```.

5. Set up the RabbitMQ configuration data in ```PushCampaignSystem\MessageQueue\Configurations\GeneralData.cs```.

6. Select the ```Build > Rebuild Solution``` option.

## Run solution (manual tests)

1. Right-click the solution inside Visual Studio, then ```Set StartUps Projects...```. Select option ```Release``` or ```Debug``` option (according to build profile) of the projects ```WebApi```, ```IndexCampaignWorker``` and ```PushCampaignWorker```.

2. Click ```Debug > Start Debugging``` or ```Debug > Start Without Debugging```


## Run automated tests

1. Load campaign samples for automated integration test in ```PushCampaignSystem\Tests\WebApi\CampaignSamples.cs```, variable ```Samples```.

2. Load visit samples for automated integration test in ```PushCampaignSystem\Tests\WebApi\VisitSamples.cs```, variable ```Samples```.

3. Right-click the ```Tests``` project and then ```Run Tests```.


## (Optional) Install and configure RabbitMQ

### Installation

1. Download and install Erlang:
http://www.erlang.org/downloads 

2. Download and install RabbitMQ:
https://www.rabbitmq.com/download.html 

3. Open RabbitMQ Command Prompt (```sbin dir```) in Windows Start Menu

4. Check if is up and running:
    ```
    rabbitmqctl status
    ```

### Activate management dashboard

1. Activate the management tool running the following command:
    ```
    rabbitmq-plugins enable rabbitmq_management
    ```

### Login into management dashboard

1. Open in the browser:
http://localhost:15672/ 

2. Log in with the default admin account:
    ```
    Username: guest
    Password: guest
    ```

### Add new user

1. Open tab ```Admin > Users```

2. Add User

    a. no tags

    b. name ```push-campaign-system```
    
    c. password ```in-loco```

### Add new virtual host

1. Open ```Admin > Virtual Hosts```

2. Add virtual host:

    a. name ```push-campaign-system-host```

### Configure user permission for virtual host

1. Open tab ```Admin > Virtual Hosts > push-campaign-system-host```

2. Set permission for:
    
    a. User ```push-campaign-system```
    
    b. Configure regexp .*
    
    c. Write regexp .*
    
    d. Read regexp .*
    
    (ref: https://stackoverflow.com/a/32380076)


## (Optional) Install and configure Redis

1. In a Linux terminal, run the following commands:

    ```
    sudo apt-get update
    sudo apt-get upgrade
    sudo apt-get -y install redis-server
    redis-cli -v
    ```

2. Restart the Redis server to make sure it is running:

    ```
    sudo service redis-server restart
    ```

3. Check if it is up and running:

    ```
    ps -f -u redis
    ```

4. In case of virtual machine environment, edit the config file for allowing remote connection:

    ```
    sudo nano /etc/redis/redis.conf
    ```

5. Replace ```127.0.0.1``` with ```0.0.0.0``` in:

    ```
    bind 0.0.0.0
    ```

6. Save the file and exit the editor.

7. Restart the Redis server:

    ```
    sudo service redis-server restart
    ```

8. Check for the new listening address:

    ```
    ps -f -u redis
    ```

    (ref. https://cloud.google.com/community/tutorials/setting-up-redis)


## About

This project was developed by Denys Farias under the MIT license.
