# The Pragmatic Programmer Tips Email Service Changelog

## VERSION 0.3.0

- upgraded dotnet back end to .NET 7.
- upgraded backend NuGet packages.
- upgraded angular front end to angular 15.
- upgraded front end npm packages.
- removed periods from tip data.

## VERSION 0.2.1

- more docker changes.
- minor deployment script changes.

## VERSION 0.2.0

- major docker changes.
- updates docker-compose and docker scripts.
- addressed some issues.

## VERSION 0.1.4

- updated scripts.
- checked mailing list logic.

## VERSION 0.1.3

- created client front end.
- changed email logic to send one email and BCC everyone on the mailing list.
- added docker files for each part of application (cli, api, and client).
- added docker helper scripts to help with deployment.
- added docker compose file for deployment.
- added crontab file for email service linux deployment.
- added mailing list file check to prevent crashing when reading a non-existent file.
- updated logging directory to use root directory.
- updated directories to use [/] instead of [\\].
- updated README.
- updated TODOs.

## VERSION 0.1.2

- started implementing API and Web Client.
- API has subscribe and unsubscribe methods.
- email service now uses mailing list with configuration emails.

## VERSION 0.1.1

- changed email logic to send individual message to each TO email list.
- added constants where possible.
- added missing documentation.

## VERSION 0.1.0

- Initial release.
- Service will only read tip data from static data file (plans to add method of getting data from web source).
- Service will email tips to users listed in configuration file.
- Service uses a smart-random method to remember what tips have already been sent out of total tip pool.
