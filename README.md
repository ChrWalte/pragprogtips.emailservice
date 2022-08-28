# The Pragmatic Programmer Tips Email Service

A daily email containing one of the Pragmatic Programmer Tips from [_The Pragmatic Programmer_](https://pragprog.com/titles/tpp20/the-pragmatic-programmer-20th-anniversary-edition/) book.

The full list of tips can be found [here](https://www.pragprog.com/tips/).
Buy your copy of the book and read the tips from [here](https://www.amazon.com/Pragmatic-Programmer-journey-mastery-Anniversary/dp/0135957052/ref=sr_1_1?crid=154XKH7A4LP4C&keywords=The+Pragmatic+Programmer&qid=1650057783&sprefix=the+pragmatic+programmer%2Caps%2C354&sr=8-1) and [here](https://www.informit.com/store/pragmatic-programmer-your-journey-to-mastery-20th-anniversary-9780135957059?ranMID=24808).

## [https://pragmaticprogrammer.chrwalte.com](https://pragmaticprogrammer.chrwalte.com)

This email service is being hosted at <https://pragmaticprogrammer.chrwalte.com/> where you can submit your email to be added to the daily email mailing list.

The email gets sent out at 4:30 AM Mountain Time so that it is waiting in the inboxes as people start their day. Since there are only 100 tips, the tip service will select a random tip from the pool of unselected tips making sure that no tip is sent out twice before going through the entire list of tips.

## self-hosting

You can self-host the email service if wanted, but it may require some additional configuration or coding changes.

A docker-compose.yml deployment file is included and is the recommended way of hosting this application.

There are three main parts of this application:

- pragmatic.programmer.tips.service
- pragmatic.programmer.tips.api
- pragmatic.programmer.tips.client

More details can be found below.

### The Projects

#### pragmatic.programmer.tips.service

The pragmatic.programmer.tips.service project acts as the main email service. it handles all the random generation for the tips and the sending of the emails. This project has a lot of configurations to connect to the email service and set up the certificate. This project was created to be self-contained where all the data and log files are created at the root directory of the compiled binaries. There are plans to have this more configurable.

The pragmatic.programmer.tips.service project is a .Net 6.0 project.

#### pragmatic.programmer.tips.api

The pragmatic.programmer.tips.api project acts as the back end to the user front end. It handles all the subscribing and unsubscribing from the email service mailing list. This project was also created to be self-contained and access the same data as the pragmatic.programmer.tips.service project.

The pragmatic.programmer.tips.api project is a .Net 6.0 project.

#### pragmatic.programmer.tips.client

The pragmatic.programmer.tips.client project acts as the front end for the users. It handles all the website views and allows people to easily subscribe and unsubscribe from the email service mailing list.

The pragmatic.programmer.tips.client project is an Angular 13.3 Project.

### Installing

#### [GitHub Source Code](https://github.com/chrwalte/pragprogtips.emailservice)

Prerequisites:

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
dotnet run --project pragmatic.programmer.tips.service
```

The source code includes a Visual Studio solution file that can be used to compile and run the service from Visual Studio.

- [Visual Studio install](https://visualstudio.microsoft.com/)

#### [DockerHub Container Images](https://hub.docker.com/)

Prerequisites:

- [git install](https://git-scm.com/) ([git cheatsheet](https://education.github.com/git-cheat-sheet-education.pdf))
- [docker install](https://docs.docker.com/get-started/)
- [dockerhub](https://hub.docker.com)

Docker and Docker-Compose scripts are included in the source code. Use [these](https://github.com/ChrWalte/pragprogtips.emailservice/tree/main/scripts) for references for running in Docker and Docker-Compose.

To clone the repository, run the following git command:

```shell
git clone https://github.com/chrwalte/pragprogtips.emailservice.git
```

Here are the docker images hosted on [DockerHub](https://hub.docker.com/):

- [pragmatic.programmer.tips.service](https://hub.docker.com/repository/docker/chrwalte/pragmatic.programmer.tips.service)
- [pragmatic.programmer.tips.api](https://hub.docker.com/repository/docker/chrwalte/pragmatic.programmer.tips.api)
- [pragmatic.programmer.tips.client](https://hub.docker.com/repository/docker/chrwalte/pragmatic.programmer.tips.client)

These are the supported architecture: arm64 and aarch64/v8

### Configuration

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

## License

This project is licensed under the GNU GENERAL PUBLIC license.
This can be viewed [here](https://www.gnu.org/licenses/gpl-3.0.en.html).

## Contributing

To contribute to this project, create a branch of the source code repository, make the changes, and create a pull request. The pull request will be merged into the main breach after review.
