# AgileCode.SimpleEventProcessor

AgileCode.SimpleEventProcessor is a framework (in a form of a library) written for Microsoft.NET framework which aim is to provide some fundamental services (interfaces) in order to enable an easy event-processing.

The library is particularly suited for the microservices applications which need to process some event data in an asyncronous fashion (i.e.: a queue listener, etc..)


This library is responsible for so called "Simple Events", which definition is*: 


*Simple Event Processing*: Simple events; that is, events which do not summarize, represent or denote a set of other events, are filtered and routed without modification. Thus, a notable event happens, initiating downstream action(s), and each event occurrence is processed independently. Although referred to as 'simple', such events can provide great value and provide considerable business information. Events are transformed, which entails translating and splitting events, and merging one or more events. Simple processing includes processing such as changing the events schema from one form to another, augmenting the event payload with additional data, redirecting the event from one channel or stream to another, and generating multiple events based on the payload of a single event. This type of event processing is not always distinguished as a separate type. 



* https://www.ibm.com/developerworks/library/ws-eventprocessing/index.html