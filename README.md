# NetMQ

### Request - Replay

 Implements a Broker according to Majordomo Protocol v0.1
 it implements this broker asynchronous and handles all administrative work
 if Run is called it automatically will Connect to the endpoint given
 it however allows to alter that endpoint via Bind
 it registers any worker with its service
 it routes requests from clients to waiting workers offering the service the client has requested
 as soon as they become available

 Services can/must be requested with a request, a.k.a. data to process

      CLIENT     CLIENT       CLIENT     CLIENT
     "Coffee"    "Water"     "Coffee"    "Tea"
        |           |            |         |
        +-----------+-----+------+---------+
                          |
                        BROKER
                          |
              +-----------+-----------+
              |           |           |
            "Tea"     "Coffee"     "Water"
            WORKER     WORKER       WORKER
