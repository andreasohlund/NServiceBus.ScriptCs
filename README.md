# [![NServiceBus](https://liveparticularwebstr.blob.core.windows.net/media/Default/img/NSB/logo.png)](http://particular.net/nservicebus) . [![ScriptCs](http://scriptcs.net/images/logo.png)](http://scriptcs.net/)

[scriptcs](http://scriptcs.net/) [script packs](https://github.com/scriptcs/scriptcs/wiki/Script-Packs) for [NServiceBus](http://particular.net/nservicebus).

## Building from source ##

### Pre-requisites

- .NET 4.5
- scriptcs (0.13.2 or later)

### Tasks

All build tasks are designed to run regardless of whether they have been run previously or whether the clone is fresh. They do not have to be run in the order shown here. When a given task is run, all pre-requisite tasks are run automatically.

The following batch files wrap the invocation of the most common build tasks.

- `build.cmd` : Run the default tasks which build the solution and create the NuGet package.
- `sample.cmd` : Run the sample. *Requires ServiceControl to be running locally on port 33333.*

Note that these files are just simple wrappers for the following [Bau](https://github.com/bau-build/bau) commands:

- `bau.cmd`
- `bau.cmd sample`

By using Bau directly, you can take advantage of further command line options. For more details, type:

`bau.cmd -help`

Note that `bau.cmd` is just a simple wrapper for running `baufile.csx` using scriptcs, passing along any arguments.
