# The Pragmatic Programmer Tips Email Service Changelog

## VERSION 0.1.1

- changed email logic to send individual message to each TO email list.
- added constants where possible.
- added missing documentation.

## VERSION 0.1.0

- Initial release.
- Service will only read tip data from static data file (plans to add method of getting data from web source).
- Service will email tips to users listed in configuration file.
- Service uses a smart-random method to remember what tips have already been sent out of total tip pool.
