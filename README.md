# SignalBooster sample by David Shinkle

## Project Notes:

This is a quick refactoring of the sample project.

IDE used: Visual Studio 2022.

AI used: None except for researching using appsettings with console applications.

## Imrprovements needed.

The logic of this application needs to be improved by:
- Managing the values that can be submitted via a non-hardcoded method such as a JSON file or database table.
- One hidden bug is data corruption.  The values for oxygen liters amd sleep and exertion should only be set if the device type is Oxygen, however the applications scheme does not enforce that.


