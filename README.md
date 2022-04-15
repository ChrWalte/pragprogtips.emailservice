# The Pragmatic Programmer Tips Email Service

An email service that randomly selects a tip from the list of Pragmatic Programmer Tips and sends it to a mailing list.

Information on the _The Pragmatic Programmer_ book can be found [here](https://pragprog.com/titles/tpp20/the-pragmatic-programmer-20th-anniversary-edition/).
The full list of tips can be found [here](https://www.pragprog.com/tips/).
Buy your copy of the book and read the tips from [here](https://www.amazon.com/Pragmatic-Programmer-journey-mastery-Anniversary/dp/0135957052/ref=sr_1_1?crid=154XKH7A4LP4C&keywords=The+Pragmatic+Programmer&qid=1650057783&sprefix=the+pragmatic+programmer%2Caps%2C354&sr=8-1) and [here](https://www.informit.com/store/pragmatic-programmer-your-journey-to-mastery-20th-anniversary-9780135957059?ranMID=24808).

## Installing

Currently, the only way to install this service is to clone the repository and compile it using dotnet.

- [git install](https://git-scm.com/) ([git cheatsheet](https://education.github.com/git-cheat-sheet-education.pdf))
- [dotnet install](https://dotnet.microsoft.com/en-us/)

To clone the repository, run the following git command:

```shell
git clone https://github.com/chrwalte/pragprogtips.emailservice.git
```

To compile the service, run the following command:

```shell
dotnet build
```

To run the service, run the following command:

```shell
dotnet run --project pragmatic.programmer.tips.cli
```

The source code includes a Visual Studio solution file that can be used to compile and run the service from Visual Studio.

- [Visual Studio install](https://visualstudio.microsoft.com/)

## Configuration

The configuration files currently contain secrets that are used to connect to the email service.
Once the secrets are redacted, the configuration files will be safe to store in the repository.

Here is an example configuration file used by the service:

```jsonc
{
  // logger configuration
  "loggerConfiguration": {
    // minimum log to display. see logger code for more details.
    "logLevel": "Debug"
  },

  // email service configuration
  "emailServerConfiguration": {
    // host of the email server
    "host": "",
    // smtp port of the email server
    "port": 0,
    // username, if auth is required
    "username": "",

    // password, if auth is required
    "plaintextPassword": "",

    // some email service providers are not included in MailKit by standard. the email service this was implemented for is ProtonMail.
    // if a provider is not included in MailKit, the following configuration is required.
    "useCustomCertificateInformationInCertificateValidationCallback": false,
    // get from email service provider certificate. see email service in code for more details.
    "customCertificateInformation": {
      "name": "",
      "fingerPrint": "",
      "serial": "",
      "issuer": ""
    }
  },

  // email message configuration
  "emailMessageConfiguration": {
    // subject of the email
    "subjectTemplate": "",
    // what email is this sent from
    "fromEmails": [
      {
        "name": "",
        "email": ""
      }
    ],
    // what emails is this sent to
    "toEmails": [
      {
        "name": "",
        "email": ""
      }
    ]
  }
}
```

The email body can be configured as well just in a different way.
The email body is read in by the code as an HTML file.
This HTML file can be found in the core project under data/raw/email.template.html.

## Usage

Once the service is set up and configured, it can be left on its own.
The service was intended to be run every day at a certain time.

## License

This project is licensed under the GNU GENERAL PUBLIC license.
This can be viewed [here](https://www.gnu.org/licenses/gpl-3.0.en.html).

## Contributing

To contribute to this project, create a branch of the source code repository, make the changes, and create a pull request. the pull request will be merged into the main breach after review.
