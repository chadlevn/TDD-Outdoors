# TDD-Outdoors

This is a sample project meant to demonstrate a simple architecture that you might use for Test Driven Development.

## Basic Features

### Add Activities

This feature has a model and a command.

The model has only what is necessary to add a new activity.
It has validation rules and tests for each of them.

The command takes in the model, and adds a new activity to the database context. 
It has tests proving that it adds a new activity and returns the new primary key.

### View Upcoming Activities

This feature has a query and a dto.

The dto uses automapper and has tests for the mapping profile to prove that each property maps as expected.

The query searches the database context for activities with a date in the future and returns them as a list of these dtos.
It has tests proving that activities in the future are returned and activities in the past are not returned.
