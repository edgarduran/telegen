# telegen

**telegen** is a project written for the third phase of the **Red Canary** interview process. It's purpose is to exercise the file system, the active processes, and the network interface and record those activities so that the recorded information may be compared with the information detected by the system monitors.

## Technical Documentation

[Read the technical documentation here.](telegen/docs/telegen.md)

## Overview

The assignment called for the implementation not just of an application, but a *framework*. I interpreted this to mean that the objects created should be usable in several different ways. This project demonstrates how to exercise the framework (and thus the desired endpoints) using a small, hand-parsed scripting language, and also how to use the framework from akka.net actors.

In either case, the desired activity is described in an **Operation** class, an immutable POCO that is suitable for use as Akka.Net messages. This operation is handled by the appropriate **Agent** class. The **Agent** interprets the operation's properties and takes the appropriate action.

>IMPLEMENTATION NOTE: Access to the agent is provided by a custom interface. One could easily adapt the engine to use a dependency injection (DI) container and provide multiple implementations for each agent, selectable at run-time. This was not a requirement of the assignment, and was not implemented, but the option was left open for future development. Additionally, the standardization makes the current implementation easier to follow.

## Implementation

### Agents

#### Telemetry-Generating Agents

To meet the requirements of the assignment, I implemented three different agents. This table lists each agent, along with the corresponding Operator and the requirement, copied directly from the assignment.

|Agent|Operation|Requirement|
|-----|:--------|:----------|
|FileAgent|OpCreateFile|Create a file of a specified type at a specified location|
||OpUpdateFile|Modify a file|
||OpDeleteFile|Delete a file|
|ProcessAgent|OpSpawn|Start a process, given a path to an executable file and the desired (optional) command-line arguments|
|NetworkAgent|OpNetGet|Establish a network connection and transmit data|

#### Reporting Agents

In addition to these, agents also provide reporting functionality. The assignment specifically called for the log to be generated "in a machine friendly format (e.g. CSV, TSV, JSON, YAML, etc)." The report actors accept a **Result** object/message that is returned from the telemetry-actors' activity. I have implemented a dedicated JSON reporting agent, but perhaps more interesting is the **CustomReportAgent**.

The **CustomReportAgent** allows the user to define a custom format by using placeholders to describe the log format. This makes it trivial to implement delimited format files.

>SAMPLE DELIMITED FORMAT FILE

    {Type},{Machine},{UTCStart},{ProcessId},{ProcessName},{FileEventType}{FileName},{ProcessName}

### Platforms

The code has been successfully tested on **macOS** and **Microsoft Windows**.

## Summary

The assignment left open many implementation details. I made the assumption that this was by design, as the direction that each candidate takes reveals much about their thought processes, and what they value as an architect.

For this project, I chose to emphasize the flexibility and stability of the framework, while attempting not to over-engineer. Additionally, I attempted to anticipate how the framework might be used in the future so that today's design decisions wouldn't impede tomorrow's needs.
